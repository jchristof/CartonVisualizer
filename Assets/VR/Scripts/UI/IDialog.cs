using System;

namespace Assets.VR.Scripts.UI {

    public interface IDialog {

        Action<object> DialogResult { get; set; }
        DialogType DialogType { get; }

    }

}
