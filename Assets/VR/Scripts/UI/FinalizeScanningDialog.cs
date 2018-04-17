
using UnityEngine;

namespace Assets.VR.Scripts.UI {

    public class FinalizeScanningDialog : InteractiveMenu {

        private SpatialMapState spatialMapState;

        void Update() {
            if (spatialMapState == null) {

                var spatialMapperGameObject = GameObject.Find("SpatialMapper");

                if (spatialMapperGameObject == null)
                    return;

                spatialMapState = spatialMapperGameObject.GetComponentInChildren<SpatialMapState>();
            }
        }

        protected override void ButtonInputDown(string buttonId) {

            spatialMapState.FinishScanning();

            if (DialogResult != null)
                DialogResult("Finalize");
        }

        public override DialogType DialogType { get { return DialogType.FinalizeScan; } }

    }

}
