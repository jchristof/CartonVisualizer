
using System;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.Receivers;
using UnityEngine;

public class WelcomeDialog : InteractionReceiver, IDialog {

    protected override void InputDown(GameObject obj, InputEventData eventData) {
        base.InputDown(obj, eventData);

        if(DialogResult != null)
            DialogResult("Continue");
    }

    public Action<string> DialogResult { get; set; }
    public DialogType DialogType {get { return DialogType.Welcome; }}

}
