
using System;
using Assets.Scripts;
using UnityEngine;

public class ContainerItem : MonoBehaviour {

    private Material originalMateral;
    private Material[] materialCollection;
    private TextMesh hoverTextMesh;
    private Renderer renderer;

    void Awake() {
        hoverTextMesh = GameObject.Find("HoverText").GetComponent<TextMesh>();
        if(hoverTextMesh == null)
            throw new InvalidOperationException("Hover text requires a TextMesh in the scene graph.");

        renderer = GetComponent<Renderer>();
    }

    public void SetMaterials(Material[] materialCollection, Color color) {
        this.materialCollection = materialCollection;

        originalMateral = new Material(materialCollection[(int)ContainerMaterials.Transparent]);
        originalMateral.color = color;
        renderer.material = originalMateral;
    }

    void OnMouseOver() {
        renderer.material = materialCollection[(int) ContainerMaterials.Highlight];

        hoverTextMesh.text = name;
        hoverTextMesh.transform.LookAt(Camera.main.transform);
        hoverTextMesh.transform.Rotate(Vector3.up - new Vector3(0, 180, 0));
        hoverTextMesh.transform.position = renderer.bounds.center;
    }

    void OnMouseExit() {
        renderer.material = originalMateral;
        hoverTextMesh.text = "";
    }
}
