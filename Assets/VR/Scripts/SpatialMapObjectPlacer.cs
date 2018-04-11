using System.Collections.Generic;
using HoloToolkit.Unity;
using UnityEngine;

public class SpatialMapObjectPlacer : MonoBehaviour {

    public void InitializePlacement() {
        SpatialUnderstandingDllObjectPlacement.Solver_Init();

        var container = GameObject.Find("Container");
        var bounds = container.GetComponentInChildren<ContainerVisualizer>().ContainerBounds;
        var understandingDll = SpatialUnderstanding.Instance.UnderstandingDLL;

        var placementDefinition = SpatialUnderstandingDllObjectPlacement.ObjectPlacementDefinition.Create_OnFloor(bounds.extents * .5f);
        var pinnedPlacementDefinition = understandingDll.PinObject(placementDefinition);

        var placementRules = new List<SpatialUnderstandingDllObjectPlacement.ObjectPlacementRule>();
        var rule = SpatialUnderstandingDllObjectPlacement.ObjectPlacementRule.Create_AwayFromWalls(.1f);

        placementRules.Add(rule);
        var pinnedPlacementRules = understandingDll.PinObject(placementRules);

        var placementConstraints = new List<SpatialUnderstandingDllObjectPlacement.ObjectPlacementConstraint>();
        var constraint = SpatialUnderstandingDllObjectPlacement.ObjectPlacementConstraint.Create_NearCenter();
        placementConstraints.Add(constraint);

        var pinnedPlacementConstraints = understandingDll.PinObject(placementConstraints);

        var staticPlacementResultPtr = understandingDll.GetStaticObjectPlacementResultPtr();

        var result = SpatialUnderstandingDllObjectPlacement.Solver_PlaceObject("container", pinnedPlacementDefinition,
            placementRules.Count, pinnedPlacementRules, placementConstraints.Count, pinnedPlacementConstraints,
            staticPlacementResultPtr);

        SpatialUnderstandingDllObjectPlacement.ObjectPlacementResult placementResult = understandingDll.GetStaticObjectPlacementResult();
        container.transform.position = (placementResult.Clone() as SpatialUnderstandingDllObjectPlacement.ObjectPlacementResult).Position ;
    }

    private ObjectPlacementQuery MakePlacementQuery() {
        var placementQuery = new ObjectPlacementQuery();

        var container = GameObject.Find("Container");
        var bounds = container.GetComponentInChildren<ContainerVisualizer>().ContainerBounds;
        placementQuery.PlacementDefinition = SpatialUnderstandingDllObjectPlacement.ObjectPlacementDefinition.Create_OnFloor(bounds.extents * .5f);

        var placementRules = new List<SpatialUnderstandingDllObjectPlacement.ObjectPlacementRule>();
        var rule = SpatialUnderstandingDllObjectPlacement.ObjectPlacementRule.Create_AwayFromWalls(.1f);

        placementQuery.PlacementRules.Add(rule);

        var placementConstraints = new List<SpatialUnderstandingDllObjectPlacement.ObjectPlacementConstraint>();
        var constraint = SpatialUnderstandingDllObjectPlacement.ObjectPlacementConstraint.Create_NearCenter();

        placementConstraints.Add(constraint);

        return placementQuery;
    }

    private PlacementResult PlaceObject(string placementName,
                                        SpatialUnderstandingDllObjectPlacement.ObjectPlacementDefinition placementDefinition,
                                        Vector3 boxFullDims,
                                        ObjectType objType,
                                        List<SpatialUnderstandingDllObjectPlacement.ObjectPlacementRule> placementRules = null,
                                        List<SpatialUnderstandingDllObjectPlacement.ObjectPlacementConstraint> placementConstraints = null) {

        // New query
        if (SpatialUnderstandingDllObjectPlacement.Solver_PlaceObject(
                                                                      placementName,
                                                                      SpatialUnderstanding.Instance.UnderstandingDLL.PinObject(placementDefinition),
                                                                      placementRules.Count,
                                                                      SpatialUnderstanding.Instance.UnderstandingDLL.PinObject(placementRules.ToArray()),
                                                                       placementConstraints.Count,
                                                                      SpatialUnderstanding.Instance.UnderstandingDLL.PinObject(placementConstraints.ToArray()),
                                                                      SpatialUnderstanding.Instance.UnderstandingDLL.GetStaticObjectPlacementResultPtr()) > 0) {
            SpatialUnderstandingDllObjectPlacement.ObjectPlacementResult placementResult = SpatialUnderstanding.Instance.UnderstandingDLL.GetStaticObjectPlacementResult();

            return new PlacementResult(placementResult.Clone() as SpatialUnderstandingDllObjectPlacement.ObjectPlacementResult, boxFullDims, objType);
        }

        return null;
    }

    private List<PlacementQuery> CreateLocationQueriesForSolver(int desiredLocationCount, Vector3 boxFullDims, ObjectType objType) {
        List<PlacementQuery> placementQueries = new List<PlacementQuery>();

        var halfBoxDims = boxFullDims * .5f;

        var disctanceFromOtherObjects = halfBoxDims.x > halfBoxDims.z ? halfBoxDims.x * 3f : halfBoxDims.z * 3f;

        for (int i = 0; i < desiredLocationCount; ++i) {
            var placementRules = new List<SpatialUnderstandingDllObjectPlacement.ObjectPlacementRule>
            {
                SpatialUnderstandingDllObjectPlacement.ObjectPlacementRule.Create_AwayFromOtherObjects(disctanceFromOtherObjects)
            };

            var placementConstraints = new List<SpatialUnderstandingDllObjectPlacement.ObjectPlacementConstraint>();

            SpatialUnderstandingDllObjectPlacement.ObjectPlacementDefinition placementDefinition = SpatialUnderstandingDllObjectPlacement.ObjectPlacementDefinition.Create_OnFloor(halfBoxDims);

            placementQueries.Add(
                                 new PlacementQuery(placementDefinition,
                                                    boxFullDims,
                                                    objType,
                                                    placementRules,
                                                    placementConstraints
                                                   ));
        }

        return placementQueries;
    }
}
