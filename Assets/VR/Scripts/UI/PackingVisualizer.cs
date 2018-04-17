
using UnityEngine;

namespace Assets.VR.Scripts.UI {

    public class PackingVisualizer : InteractiveMenu {

        protected override void ButtonInputDown(string buttonId) {

            if(buttonId.ToLower() == "explode")
                GameObject.Find("Container").GetComponentInChildren<ContainerVisualizer>().Explode();

            if (DialogResult != null)
                DialogResult("");
        }

        public override DialogType DialogType {
            get { return DialogType.PackingVisualizer; }
        }
    }

}
