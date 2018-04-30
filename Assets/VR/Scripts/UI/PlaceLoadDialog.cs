using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;
using UnityEngine;

namespace Assets.VR.Scripts.UI {

    public class PlaceLoadDialog : InteractiveMenu , IInputClickHandler {

        public override void Start() {
            base.Start();
            InputManager.Instance.PushFallbackInputHandler(gameObject);
        }

        public override DialogType DialogType { get { return DialogType.PlaceLoad; } }

        public new void OnInputClicked(InputClickedEventData eventData) {
            if (eventData.used)
                return;

            eventData.Use();

            InputManager.Instance.PopFallbackInputHandler();

            var containerGameObject = GameObject.Find("Container");
            if (containerGameObject == null)
                return;

            var loadName = (Parameters as string).Trim().ToLower();

            if (loadName == "load 1")
                containerGameObject.GetComponentInChildren<ContainerVisualizer>().LoadOne();
            else if(loadName == "load 2")
                containerGameObject.GetComponentInChildren<ContainerVisualizer>().LoadTwo();

            if (SpatialMappingManager.Instance == null)
                return;

            RaycastHit hitInfo;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 30.0f, SpatialMappingManager.Instance.LayerMask))
                containerGameObject.GetComponentInChildren<ContainerVisualizer>().PlaceBottomCenterAt(hitInfo.point);

            if (DialogResult != null)
                DialogResult("");
        }

    }

}
