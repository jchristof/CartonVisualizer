using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine.Networking.NetworkSystem;

public class ContainerVisualizer : MonoBehaviour {
    private readonly List<string> containerFileNames = new List<string>();
    private readonly List<GameObject> cubeObjects = new List<GameObject>();
    private Bounds containerBounds;
    public Material[] productMaterial;
    public GameObject cubeIqBlock;
    public GameObject target;
    Dictionary<string, int> products; 
    int initButtonPosition = 60;
    int buttonSpacing = 40;
    Rect loadMsg = new Rect(10, 140, 400, 40);
    string fileLocation;
    CubeiqContainer.Cubeiq cubeIq;
    string xmlData;

    void Start() {
        fileLocation = Application.dataPath + "//StreamingAssets//Xml";
        FindContainerFiles();
        cubeIq = new CubeiqContainer.Cubeiq();
        products = new Dictionary<string, int>();
    }

    public void LoadCubeModel() {
        ClearContainers();
        LoadXML(containerFileNames[0]);
        if (xmlData.ToString() != "") {
            cubeIq = (CubeiqContainer.Cubeiq)DeserializeObject(xmlData);
        }
        Visualize(cubeIq);
    }

    private void FindContainerFiles() {
        DirectoryInfo d = new DirectoryInfo(fileLocation);
        FileInfo[] Files = d.GetFiles("*.xml");
        foreach (FileInfo file in Files) {
            containerFileNames.Add(file.Name);
        }
    }

    void OnGUI() {
        int buttonPosition = initButtonPosition;
        foreach (var fileName in containerFileNames) {
            Create(fileName, new Rect(30, buttonPosition, 200, 30));
            buttonPosition += buttonSpacing;
        }
    }

    private void Create(string fileName, Rect rect) {
        if (GUI.Button(rect, "Load " + fileName)) {
            GUI.Label(loadMsg, "Loading from: " + fileLocation);
            ClearContainers();
            LoadXML(fileName);
            if (xmlData.ToString() != "") {
                cubeIq = (CubeiqContainer.Cubeiq)DeserializeObject(xmlData);
            }
            Visualize(cubeIq);
        }
    }

    private void Visualize(CubeiqContainer.Cubeiq cubeIq) {
        int index = 0;
        products.Clear();
        cubeObjects.Clear();

        foreach (var product in cubeIq.Products.Product) {
            products.Add(product.Productid, index % productMaterial.Length);
            index++;
        }
        index = 0;

        containerBounds = new Bounds(Vector3.zero, Vector3.zero);

        foreach (var block in cubeIq.Blocks.Block) {
            GameObject cube = Instantiate(cubeIqBlock, new Vector3(float.Parse(block.Widthcoord), float.Parse(block.Heightcoord), float.Parse(block.Depthcoord)), Quaternion.identity);
            Renderer renderer = cube.GetComponentInChildren<Renderer>();
            renderer.material = GetMaterialForContainer(block.Productid);
            cube.transform.localScale = new Vector3(float.Parse(block.Width), float.Parse(block.Height), float.Parse(block.Length));
            cubeObjects.Add(cube);
            containerBounds.Encapsulate(renderer.bounds);

            index++;
        }
        target.transform.position = containerBounds.center;

        explode = true;
        amount = 0f;
    }

    private bool explode;
    private float amount;

    void Update() {
        if (!explode || amount > 10f)
            return;

        foreach (var cube in cubeObjects) {
            Vector3 fromPosition = containerBounds.center;
            Vector3 toPosition = cube.transform.position;
            Vector3 direction = toPosition - fromPosition;

            var rayNormal = direction.normalized;

            cube.transform.Translate(rayNormal * Mathf.Sin(amount/10f) * Time.deltaTime * 10f);
        }

        amount += .1f;
    }

    private Material GetMaterialForContainer(string productid) {
        var product = cubeIq.Products.Product.FirstOrDefault(x => x.Productid == productid);

        if (product != null) {
            var color = product.Color.Split('#');

            var material = new Material(Shader.Find("Standard"));
            material.CopyPropertiesFromMaterial(productMaterial[0]);
            material.color = new Color(int.Parse(color[1])/255f, int.Parse(color[2])/255f, int.Parse(color[3])/255f, 0.5f);
            return material;
        }
        
        System.Random r = new System.Random();
        return productMaterial[r.Next(0, productMaterial.Length)];
        
    }

    private void ClearContainers() {
        GameObject[] containers = GameObject.FindGameObjectsWithTag("Container");
        foreach (var container in containers) {
            GameObject.Destroy(container);
        }
    }

    string UTF8ByteArrayToString(byte[] characters) {
        UTF8Encoding encoding = new UTF8Encoding();
        string constructedString = encoding.GetString(characters);
        return (constructedString);
    }

    byte[] StringToUTF8ByteArray(string pXmlString) {
        UTF8Encoding encoding = new UTF8Encoding();
        byte[] byteArray = encoding.GetBytes(pXmlString);
        return byteArray;
    }

    string SerializeObject(object pObject) {
        string XmlizedString = null;
        MemoryStream memoryStream = new MemoryStream();
        XmlSerializer xs = new XmlSerializer(typeof(CubeiqContainer.Cubeiq));
        XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        xs.Serialize(xmlTextWriter, pObject);
        memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
        XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
        return XmlizedString;
    }

    object DeserializeObject(string pXmlizedString) {
        XmlSerializer xs = new XmlSerializer(typeof(CubeiqContainer.Cubeiq));
        MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
        XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        return xs.Deserialize(memoryStream);
    }

    void LoadXML(string fileName) {
        StreamReader r = File.OpenText(fileLocation + "//" + fileName);
        string xml = r.ReadToEnd();
        r.Close();
        xmlData = xml;
    }
}
