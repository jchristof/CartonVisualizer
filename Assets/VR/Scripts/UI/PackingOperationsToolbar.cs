
using Assets.VR.Scripts.UI;

public class PackingOperationsToolbar : InteractiveMenu {

    protected override void ButtonInputDown(string buttonId) {

        if (DialogResult != null)
            DialogResult("Finalize");
    }

    public override DialogType DialogType {
        get { return DialogType.PackingOperationsToolbar; }
    }
}
