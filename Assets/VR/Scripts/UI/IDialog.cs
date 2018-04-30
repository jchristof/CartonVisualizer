using System;

namespace Assets.VR.Scripts.UI {

    public interface IDialog {

        Action<object> DialogResult { get; set; }
        Action GoBack { get; set; }
        DialogType DialogType { get; }
        object Parameters { get; set; }
    }

}
