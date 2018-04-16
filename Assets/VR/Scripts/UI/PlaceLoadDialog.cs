using System;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;
using UnityEngine;

public class PlaceLoadDialog : MonoBehaviour, IInputClickHandler, IDialog {

    void Start() {
        InputManager.Instance.PushFallbackInputHandler(gameObject);
    }

    public Action<string> DialogResult { get; set; }
    public DialogType DialogType { get { return DialogType.PlaceLoad; } }

    public void OnInputClicked(InputClickedEventData eventData) {
        InputManager.Instance.PopFallbackInputHandler();


        var containerGameObject = GameObject.Find("Container");
        if (containerGameObject == null)
            return;

       containerGameObject.GetComponentInChildren<ContainerVisualizer>().LoadOne();

        if (SpatialMappingManager.Instance == null)
            return;

        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 30.0f, SpatialMappingManager.Instance.LayerMask))
            containerGameObject.GetComponentInChildren<ContainerVisualizer>().PlaceBottomCenterAt(hitInfo.point);

        if (DialogResult != null)
            DialogResult("");
    }

}
