
using System;
using System.Linq;
using HoloToolkit.Unity.Buttons;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.Receivers;
using UnityEngine;

public class PackingOperationsToolbar : InteractionReceiver, IDialog {

    void Start() {
        var compoundButtons = gameObject.GetComponentsInChildren<CompoundButton>();
        interactables = compoundButtons.Select(x => x.gameObject).ToList();
    }

    protected override void InputDown(GameObject obj, InputEventData eventData) {
        base.InputDown(obj, eventData);

        if (DialogResult != null)
            DialogResult("Finalize");
    }

    public Action<string> DialogResult { get; set; }
    public DialogType DialogType {
        get { return DialogType.PackingOperationsToolbar; }
    }
}
