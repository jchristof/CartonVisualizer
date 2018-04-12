
using UnityEngine;

public class TagalongMenuManager : MonoBehaviour {

    public GameObject spatialMappingUI;
    public GameObject blockDetailsUI;

    private GameObject currentUI;

	void Start () {
	    currentUI = Instantiate(spatialMappingUI, gameObject.transform);
	}

	void Update () {
		
	}
}
