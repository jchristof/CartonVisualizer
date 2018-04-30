
using Assets.Scripts;
using Assets.VR.Scripts.UI;

public class PackingSequence : InteractiveMenu {

    protected override void ButtonInputDown(string buttonId) {
        var commands = Parameters as IVisualCommands;

        if (commands == null)
            return;

        if (buttonId.ToLower() == "first")
            commands.ShowFirst();
        else if (buttonId.ToLower() == "next")
            commands.ShowNext();
        else if (buttonId.ToLower() == "previous")
            commands.ShowPrevious();
        else if (buttonId.ToLower() == "all")
            commands.ShowAll();

//        if (DialogResult != null)
//            DialogResult("");
    }

    public override DialogType DialogType {
        get { return DialogType.PackingSequence; }
    }
}
