
using UnityEngine;

public class TagalongMenuManager : MonoBehaviour {

    public GameObject welcomeDialog;
    public GameObject scanOrLoadDialog;
    public GameObject placeLoadDialog;

    private GameObject currentUI;

	void Start () {
	    currentUI = Instantiate(welcomeDialog, gameObject.transform);
	    currentUI.GetComponentInChildren<IDialog>().DialogResult = Result;
	}

    public void Result(string value) {
        var dialog = currentUI.GetComponentInChildren<IDialog>();
        Destroy(currentUI);

        if (dialog.DialogType == DialogType.Welcome) {
            currentUI = Instantiate(scanOrLoadDialog, gameObject.transform);
            currentUI.GetComponentInChildren<IDialog>().DialogResult = Result;
        }
        else if (dialog.DialogType == DialogType.FinalizeScan) {
            currentUI = Instantiate(placeLoadDialog, gameObject.transform);
            currentUI.GetComponentInChildren<IDialog>().DialogResult = Result;
        }
        
    }
}
