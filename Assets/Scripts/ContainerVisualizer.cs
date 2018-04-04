using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System.Text;
using Assets.Scripts;

public class ContainerVisualizer : MonoBehaviour {
    private readonly List<string> containerFileNames = new List<string>();
    private readonly List<GameObject> cubeObjects = new List<GameObject>();

    private Bounds containerBounds;
    public Material[] productMaterial;
    public GameObject cubeIqBlock;
    public GameObject target;
    public GameObject buttonPrefab;
    
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

        foreach (var fileName in containerFileNames) {
            UIServices.NewButton(buttonPrefab, "Load " + fileName, () => {
                var f = fileName;
                Create(f);
            });
        }

        UIServices.NewButton(buttonPrefab, "Exit", Application.Quit);
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

    private void Create(string fileName) {
        ClearContainers();
        LoadXML(fileName);
        if (xmlData.ToString() != "") {
            cubeIq = (CubeiqContainer.Cubeiq)DeserializeObject(xmlData);
        }
        Visualize(cubeIq);
    }

    private void Visualize(CubeiqContainer.Cubeiq cubeIq) {
        int index = 0;
        products.Clear();
        cubeObjects.Clear();

        GameObject.Find("HoverText").GetComponent<TextMesh>().text = "";

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
            cube.transform.GetChild(0).gameObject.name = block.Productid;

            cubeObjects.Add(cube);
            containerBounds.Encapsulate(renderer.bounds);
            index++;
        }
        target.transform.position = containerBounds.center;

        var pallet = Instantiate(cubeIqBlock, new Vector3(containerBounds.center.x - containerBounds.extents.x, -1, containerBounds.center.z - containerBounds.extents.z), Quaternion.identity);
        pallet.name = "pallet";
        pallet.transform.localScale = new Vector3(containerBounds.size.x, 1f, containerBounds.size.z);
        pallet.GetComponentInChildren<Renderer>().material = productMaterial[3];
        pallet.transform.GetChild(0).gameObject.name = "Pallet";
        cubeObjects.Add(pallet);

        containerCollectionAnimator = new ContainerCollectionAnimator(cubeObjects, 10f, .1f);
        containerCollectionAnimator.Run();
    }

    private ContainerCollectionAnimator containerCollectionAnimator;

    void Update() {
        if(containerCollectionAnimator != null)
            containerCollectionAnimator.Update();
    }

    private Material GetMaterialForContainer(string productid) {
        var product = cubeIq.Products.Product.FirstOrDefault(x => x.Productid == productid);

        if (product != null) {
            var material = new Material(Shader.Find("Standard"));
            material.CopyPropertiesFromMaterial(productMaterial[0]);
            material.color = product.Color.ToColor(0.5f);
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
