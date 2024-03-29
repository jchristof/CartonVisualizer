﻿using UnityEngine;
using System.IO;
using Assets.Scripts;

public class ContainerVisualizer : MonoBehaviour {
    // Requred Unity elements set from the interface
    [Tooltip("Materials used to visualize block volumes")]
    public Material[] materialCollection;
    public GameObject cubePrefab;
    [Tooltip("Mouse camera movement orbits around this object")]
    public GameObject cameraTarget;
    public GameObject buttonPrefab;
    [Tooltip("Starting point for the container")]
    public Vector3 originOffset;
    
    // Data and visualization elements
    private CubeiqContainer.Cubeiq cubeIq;
    private VisualContainerCollection visualContainerCollection;

    public ContainerCollectionAnimator VisualCommands { get; private set; }

    private FileInfo[] files;

    void Start() {
        files = new DirectoryInfo(Application.dataPath + "//StreamingAssets//Xml").GetFiles("*.xml");
        var menu = GameObject.Find("Content");

        if (menu == null)
            return;

        foreach (var file in files) {
            UIServices.NewButton(buttonPrefab, menu, "Load " + file.Name, () => {
                var f = file;
                cubeIq = Create(f.FullName);

                if (cubeIq == null)
                    return;

                Visualize(cubeIq);
            });
        }

        UIServices.NewButton(buttonPrefab, menu, "Explode", () => VisualCommands?.Explode());
        UIServices.NewButton(buttonPrefab, menu, "Compact", () => VisualCommands?.Compact());
        UIServices.NewButton(buttonPrefab, menu, "Show First", () => VisualCommands?.ShowFirst());
        UIServices.NewButton(buttonPrefab, menu, "Show Next", () => VisualCommands?.ShowNext());
        UIServices.NewButton(buttonPrefab, menu, "Show Previous", () => VisualCommands?.ShowPrevious());
        UIServices.NewButton(buttonPrefab, menu, "Show All", () => VisualCommands?.ShowAll());
        UIServices.NewButton(buttonPrefab, menu, "Exit", Application.Quit);
    }

    public void LoadOne() {
        cubeIq = Create(files[0].FullName);

        if (cubeIq == null)
            return;

        Visualize(cubeIq);
    }

    public void LoadTwo() {
        cubeIq = Create(files[1].FullName);

        if (cubeIq == null)
            return;

        Visualize(cubeIq);
    }

    public Bounds ContainerBounds { get { return visualContainerCollection != null ? visualContainerCollection.ContainerBounds : new Bounds(Vector3.zero, Vector3.one); } }

    private CubeiqContainer.Cubeiq Create(string fullname) {
        using (var r = File.OpenText(fullname)) {
            string xml = r.ReadToEnd();

            if (string.IsNullOrEmpty(xml))
                return null;

            return XmlSerialization.DeserializeObject(xml) as CubeiqContainer.Cubeiq;
        }
    }

    private void Visualize(CubeiqContainer.Cubeiq cubeIq) {
        if(visualContainerCollection != null)
            visualContainerCollection.Dispose();

        visualContainerCollection = new VisualContainerCollection(cubeIq, gameObject, cubePrefab, materialCollection, originOffset);
        cameraTarget.transform.position = visualContainerCollection.VolumeCenter;

        VisualCommands = new ContainerCollectionAnimator(gameObject, visualContainerCollection.CubeObjects, .1f, 3f);
    }
}
