
using UnityEngine;

public class ContainerItem : MonoBehaviour {

    private Material originalMateral;

    void Start() {
        originalMateral = GetComponentInChildren<Renderer>().material;
    }

    void OnMouseOver() {
        var renderer = GetComponentInChildren<Renderer>();
        renderer.material = new Material(Shader.Find("VertexLit")) {color = Color.red};
    }

    void OnMouseExit() {
        GetComponentInChildren<Renderer>().material = originalMateral;
    }
}
