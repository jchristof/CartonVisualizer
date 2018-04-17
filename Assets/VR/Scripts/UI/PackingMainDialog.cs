

namespace Assets.VR.Scripts.UI {

    public class PackingMainDialog : InteractiveMenu {

        protected override void ButtonInputDown(string buttonId) {

            if (DialogResult != null)
                DialogResult(buttonId);
        }

        public override DialogType DialogType {
            get { return DialogType.PackingMain; }
        }
    }

}
