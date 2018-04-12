
using UnityEngine;

public class TagalongMenuManager : MonoBehaviour {

    public GameObject spatialMappingUI;
    public GameObject blockDetailsUI;

    private GameObject currentUI;

	void Start () {
	    currentUI = Instantiate(spatialMappingUI, gameObject.transform);
	}

    public void NextMenu() {
        Destroy(currentUI);

        currentUI = Instantiate(blockDetailsUI, gameObject.transform);
    }
	void Update () {
		
	}
}
