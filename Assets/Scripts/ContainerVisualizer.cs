using UnityEngine;
using System.IO;
using Assets.Scripts;

public class ContainerVisualizer : MonoBehaviour {
    // Requred Unity elements set from the interface
    public Material[] productMaterial;
    public GameObject cubePrefab;
    public GameObject cameraTarget;
    public GameObject buttonPrefab;
    
    // Data and visualization elements
    private CubeiqContainer.Cubeiq cubeIq;
    private VisualContainerCollection visualContainerCollection;
    private ContainerCollectionAnimator containerCollectionAnimator;

    void Start() {
        FileInfo[] files = new DirectoryInfo(Application.dataPath + "//StreamingAssets//Xml").GetFiles("*.xml");

        foreach (var file in files) {
            UIServices.NewButton(buttonPrefab, "Load " + file.Name, () => {
                var f = file;
                cubeIq = Create(f.FullName);

                if (cubeIq == null)
                    return;

                Visualize(cubeIq);
            });
        }

        UIServices.NewButton(buttonPrefab, "Exit", Application.Quit);
    }

    private CubeiqContainer.Cubeiq Create(string fullname) {
        using (var r = File.OpenText(fullname)) {
            string xml = r.ReadToEnd();
            r.Close();

            if (string.IsNullOrEmpty(xml))
                return null;

            return XmlSerialization.DeserializeObject(xml) as CubeiqContainer.Cubeiq;
        }
    }

    private void Visualize(CubeiqContainer.Cubeiq cubeIq) {
        if(visualContainerCollection != null)
            visualContainerCollection.Dispose();

        visualContainerCollection = new VisualContainerCollection(cubeIq, cubePrefab, productMaterial[0]);
        cameraTarget.transform.position = visualContainerCollection.VolumeCenter;

        containerCollectionAnimator = new ContainerCollectionAnimator(visualContainerCollection.CubeObjects, 10f, .1f);
        containerCollectionAnimator.Run();
    }

    void Update() {
        if(containerCollectionAnimator != null)
            containerCollectionAnimator.Update();
    }
}
