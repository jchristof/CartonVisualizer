
using System;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.Receivers;
using UnityEngine;

public class FinalizeScanningDialog : InteractionReceiver, IDialog {

    private SpatialMapState spatialMapState;

    void Update() {
        if (spatialMapState == null) {

            var spatialMapperGameObject = GameObject.Find("SpatialMapper");

            if (spatialMapperGameObject == null)
                return;

            spatialMapState = spatialMapperGameObject.GetComponentInChildren<SpatialMapState>();
        }
    }
    protected override void InputDown(GameObject obj, InputEventData eventData) {
        base.InputDown(obj, eventData);

        spatialMapState.FinishScanning();

        if (DialogResult != null)
            DialogResult("Finalize");
    }

    public Action<string> DialogResult { get; set; }
    public DialogType DialogType { get { return DialogType.FinalizeScan; } }

}
