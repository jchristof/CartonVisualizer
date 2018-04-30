

namespace Assets.VR.Scripts.UI {
    class SelectLoadDialog : InteractiveMenu {

        protected override void ButtonInputDown(string buttonId) {

            if (DialogResult != null)
                DialogResult(buttonId.ToLower());
        }

        public override DialogType DialogType {
            get { return DialogType.SelectLoad; }
        }
    }
}
