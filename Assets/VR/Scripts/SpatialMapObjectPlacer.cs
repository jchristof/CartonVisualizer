using HoloToolkit.Unity;
using UnityEngine;

public class SpatialMapObjectPlacer : MonoBehaviour {

    public void InitializePlacement() {
        SpatialUnderstandingDllObjectPlacement.Solver_Init();

        var placementQuery = MakePlacementQuery();

        var container = GameObject.Find("Container");
        container.transform.position = placementQuery.Query().Position;
    }

    private ObjectPlacementQuery MakePlacementQuery() {
        var placementQuery = new ObjectPlacementQuery();

        var container = GameObject.Find("Container");
        var bounds = container.GetComponentInChildren<ContainerVisualizer>().ContainerBounds;
        placementQuery.PlacementDefinition = SpatialUnderstandingDllObjectPlacement.ObjectPlacementDefinition.Create_OnFloor(bounds.extents * .5f);

        var rule = SpatialUnderstandingDllObjectPlacement.ObjectPlacementRule.Create_AwayFromWalls(.1f);

        placementQuery.PlacementRules.Add(rule);

        var constraint = SpatialUnderstandingDllObjectPlacement.ObjectPlacementConstraint.Create_NearCenter();

        placementQuery.PlacementConstraints.Add(constraint);

        return placementQuery;
    }
}
