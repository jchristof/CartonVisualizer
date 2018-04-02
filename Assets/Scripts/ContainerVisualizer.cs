using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;

public class ContainerVisualizer : MonoBehaviour {
    List<string> containerFileNames = new List<string>();
    public Material[] productMaterial;
    public GameObject cubeIqBlock;
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
        foreach (var product in cubeIq.Products.Product) {
            products.Add(product.Productid, index % productMaterial.Length);
            index++;
        }
        index = 0;
        foreach (var block in cubeIq.Blocks.Block) {
            GameObject cube = Instantiate(cubeIqBlock, new Vector3(float.Parse(block.Widthcoord), float.Parse(block.Heightcoord), float.Parse(block.Depthcoord)), Quaternion.identity);
            Renderer renderer = cube.GetComponentInChildren<Renderer>();
            renderer.material = GetMaterialForContainer(block.Productid);
            cube.transform.localScale = new Vector3(float.Parse(block.Width), float.Parse(block.Height), float.Parse(block.Length));
            index++;
        }
    }

    private Material GetMaterialForContainer(string productid) {
        int materialIndex = 0;
        if (products.TryGetValue(productid, out materialIndex)) {
            return productMaterial[materialIndex];
        } else {
            System.Random r = new System.Random();
            return productMaterial[r.Next(0,productMaterial.Length)];
        }
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
