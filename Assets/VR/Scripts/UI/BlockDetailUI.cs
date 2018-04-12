using System.Linq;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class BlockDetailUI : MonoBehaviour {

    private TextMesh title;
    private TextMesh detail;

    // Use this for initialization
    void Start () {
        var uiComponents = GetComponentsInChildren<TextMesh>();

        title = uiComponents.FirstOrDefault(x => x.name == "Title");
        detail = uiComponents.FirstOrDefault(x => x.name == "Detail");
    }

    // Update is called once per frame
    void Update() {
        var focusedObject = GameObject.Find("InputManager").GetComponent<FocusManager>().GetFocusedObject(GazeManager.Instance);

        if (focusedObject != null) {
            detail.name = focusedObject.name;
        }
    }
}
