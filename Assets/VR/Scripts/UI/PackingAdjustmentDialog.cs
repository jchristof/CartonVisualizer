
using Assets.Scripts;
using Assets.VR.Scripts.UI;

public class PackingAdjustmentDialog : InteractiveMenu {

    public override void Initialize(object commands) {
        base.Initialize(commands);

        this.commands = commands as IVisualCommands;

        this.commands.RotationOn();
    }

    private IVisualCommands commands;

    protected override void OnGoBack() {
        base.OnGoBack();
        commands.RotationOff();
    }

    protected override void ButtonInputDown(string buttonId) {

        if (DialogResult != null)
            DialogResult("Finalize");
    }

    public override DialogType DialogType {
        get { return DialogType.PackingAdjustment; }
    }
}
