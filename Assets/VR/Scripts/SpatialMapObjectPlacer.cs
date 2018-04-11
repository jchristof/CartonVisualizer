using System;
using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity;
using UnityEngine;

public class SpatialMapObjectPlacer : MonoBehaviour {

    

    public void InitializePlacement() {
        var container = GameObject.Find("Container");
        var bounds = container.GetComponentInChildren<ContainerVisualizer>().ContainerBounds;
        var understandingDLL = SpatialUnderstanding.Instance.UnderstandingDLL;

        SpatialUnderstandingDllObjectPlacement.ObjectPlacementDefinition placementDefinition = SpatialUnderstandingDllObjectPlacement.ObjectPlacementDefinition.Create_OnFloor(bounds.extents * .5f);
        var pinnedId = understandingDLL.PinObject(placementDefinition);

        SpatialUnderstandingDllObjectPlacement.Solver_PlaceObject("container")
        SpatialUnderstandingDllObjectPlacement.Solver_Init();
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
                                                                      (placementRules != null) ? placementRules.Count : 0,
                                                                      ((placementRules != null) && (placementRules.Count > 0)) ? SpatialUnderstanding.Instance.UnderstandingDLL.PinObject(placementRules.ToArray()) : IntPtr.Zero,
                                                                      (placementConstraints != null) ? placementConstraints.Count : 0,
                                                                      ((placementConstraints != null) && (placementConstraints.Count > 0)) ? SpatialUnderstanding.Instance.UnderstandingDLL.PinObject(placementConstraints.ToArray()) : IntPtr.Zero,
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
