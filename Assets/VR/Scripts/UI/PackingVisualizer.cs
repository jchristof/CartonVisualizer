
using UnityEngine;

namespace Assets.VR.Scripts.UI {

    public class PackingVisualizer : InteractiveMenu {

        protected override void ButtonInputDown(string buttonId) {

            if(buttonId.ToLower() == "explode")
                GameObject.Find("Container").GetComponentInChildren<ContainerVisualizer>().Explode();
            else if(buttonId.ToLower() == "collapse")
                GameObject.Find("Container").GetComponentInChildren<ContainerVisualizer>().Compact();

            if (DialogResult != null)
                DialogResult("");
        }

        public override DialogType DialogType {
            get { return DialogType.PackingVisualizer; }
        }
    }

}
