
namespace Assets.VR.Scripts.UI {

    public class WelcomeDialog : InteractiveMenu {

        protected override void ButtonInputDown(string buttonId) {

            if (DialogResult != null)
                DialogResult("Continue");
        }

        public override DialogType DialogType {get { return DialogType.Welcome; }}

    }

}
