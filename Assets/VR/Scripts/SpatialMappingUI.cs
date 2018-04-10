using System.Linq;
using UnityEngine;

public class SpatialMappingUI : MonoBehaviour {

    private TextMesh title;
    private TextMesh detail;
    private SpatialMapState spatialMapState;

	// Use this for initialization
	void Start () {
	    var uiComponents = GetComponentsInChildren<TextMesh>();

        title = uiComponents.FirstOrDefault(x => x.name == "Title");
	    detail = uiComponents.FirstOrDefault(x => x.name == "Detail");
	}
	
	// Update is called once per frame
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
}
