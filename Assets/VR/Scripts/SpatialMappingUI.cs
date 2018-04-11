using System.Linq;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;
using UnityEngine;

public class SpatialMappingUI : MonoBehaviour, IInputClickHandler, ISourceStateHandler {

    private TextMesh title;
    private TextMesh detail;
    private SpatialMapState spatialMapState;

	void Start () {
	    var uiComponents = GetComponentsInChildren<TextMesh>();

        title = uiComponents.FirstOrDefault(x => x.name == "Title");
	    detail = uiComponents.FirstOrDefault(x => x.name == "Detail");

	    InputManager.Instance.PushFallbackInputHandler(gameObject);
    }
	
	void Update () {
	    if (spatialMapState == null) {

	        var spatialMapperGameObject = GameObject.Find("SpatialMapper");

	        if (spatialMapperGameObject == null)
	            return;

	        spatialMapState = spatialMapperGameObject.GetComponentInChildren<SpatialMapState>();
	    }

	    var stats = spatialMapState.SpatialStats;

	    var detailsString = string.Format("totalArea={0:0.0}, horiz={1:0.0}, wall={2:0.0}", stats.TotalSurfaceArea, stats.HorizSurfaceArea, stats.WallSurfaceArea);
	    detailsString += string.Format("\nnumFloorCells={0}, numCeilingCells={1}, numPlatformCells={2}", stats.NumFloor, stats.NumCeiling, stats.NumPlatform);
	    detailsString += string.Format("\npaintMode={0}, seenCells={1}, notSeen={2}", stats.CellCount_IsPaintMode, stats.CellCount_IsSeenQualtiy_Seen + stats.CellCount_IsSeenQualtiy_Good, stats.CellCount_IsSeenQualtiy_None);

	    detail.text = detailsString;
	    title.text = "Scan state: " + spatialMapState.SpatialScanState;
	}

    public void OnInputClicked(InputClickedEventData eventData) {
        spatialMapState.FinishScanning();

        var containerGameObject = GameObject.Find("Container");
        if(containerGameObject == null)
            return;

        containerGameObject.GetComponentInChildren<ContainerVisualizer>().LoadOne();

        if (SpatialMappingManager.Instance == null)
            return;

        var colChildren = containerGameObject.GetComponentsInChildren<Collider>();
        foreach (var colChild in colChildren) {
            colChild.enabled = false;
        }

        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 30.0f, SpatialMappingManager.Instance.LayerMask)) 
            GameObject.Find("Container").transform.position = hitInfo.point;
            
    }

    public void OnSourceDetected(SourceStateEventData eventData) {
        
    }

    public void OnSourceLost(SourceStateEventData eventData) {
        
    }
}
