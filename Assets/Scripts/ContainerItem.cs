
using UnityEngine;

public class ContainerItem : MonoBehaviour {

    private Material originalMateral;

    void Start() {
        originalMateral = GetComponentInChildren<Renderer>().material;
    }

    void OnMouseOver() {
        var renderer = GetComponentInChildren<Renderer>();
        renderer.material = new Material(Shader.Find("VertexLit")) {color = Color.red};

        var text = GameObject.Find("HoverText").GetComponent<TextMesh>();

        text.text = name;
        text.transform.LookAt(Camera.main.transform);
        text.transform.Rotate(Vector3.up - new Vector3(0, 180, 0));
        text.transform.position = renderer.bounds.center;
    }

    void OnMouseExit() {
        GetComponentInChildren<Renderer>().material = originalMateral;
        //hoverText.SetActive(false);
    }
}
