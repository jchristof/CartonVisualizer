using System;

public interface IDialog {

    Action<string> DialogResult { get; set; }
    DialogType DialogType { get; }

}
