

using Assets.Scripts;

namespace Assets.VR.Scripts.UI {

    public class PackingVisualizer : InteractiveMenu {

        protected override void ButtonInputDown(string buttonId) {
            var commands = Parameters as IVisualCommands;

            if (commands == null)
                return;

            if(buttonId.ToLower() == "explode")
                commands.Explode();
            else if(buttonId.ToLower() == "collapse")
                commands.Compact();

//            if (DialogResult != null)
//                DialogResult("");
        }

        public override DialogType DialogType {
            get { return DialogType.PackingVisualizer; }
        }
    }

}
