
using Assets.VR.Scripts.UI;
using UnityEngine;

public class PackingSequence : InteractiveMenu {

    protected override void ButtonInputDown(string buttonId) {

        if (buttonId.ToLower() == "first")
            GameObject.Find("Container").GetComponentInChildren<ContainerVisualizer>().ShowFirst();
        else if (buttonId.ToLower() == "next")
            GameObject.Find("Container").GetComponentInChildren<ContainerVisualizer>().ShowNext();
        else if (buttonId.ToLower() == "previous")
            GameObject.Find("Container").GetComponentInChildren<ContainerVisualizer>().ShowPrevious();
        else if (buttonId.ToLower() == "all")
            GameObject.Find("Container").GetComponentInChildren<ContainerVisualizer>().ShowAll();

//        if (DialogResult != null)
//            DialogResult("");
    }

    public override DialogType DialogType {
        get { return DialogType.PackingSequence; }
    }
}
