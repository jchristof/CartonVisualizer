
using System.Collections.Generic;
using HoloToolkit.Unity;
using UnityEngine;

public class ObjectPlacementQuery {
    public ObjectPlacementQuery() {
        PlacementRules = new List<SpatialUnderstandingDllObjectPlacement.ObjectPlacementRule>();
        PlacementConstraints = new List<SpatialUnderstandingDllObjectPlacement.ObjectPlacementConstraint>();
    }

    public SpatialUnderstandingDllObjectPlacement.ObjectPlacementDefinition PlacementDefinition { get; set; }
    public List<SpatialUnderstandingDllObjectPlacement.ObjectPlacementRule> PlacementRules { get; private set; }
    public List<SpatialUnderstandingDllObjectPlacement.ObjectPlacementConstraint> PlacementConstraints { get; private set;}

    public SpatialUnderstandingDllObjectPlacement.ObjectPlacementResult Query () {
        var understandingDll = SpatialUnderstanding.Instance.UnderstandingDLL;

        var pinnedPlacementDefinition = understandingDll.PinObject(PlacementDefinition);
        var pinnedPlacementRules = understandingDll.PinObject(PlacementRules);
        var pinnedPlacementConstraints = understandingDll.PinObject(PlacementConstraints);
        var staticPlacementResultPtr = understandingDll.GetStaticObjectPlacementResultPtr();

        var result = SpatialUnderstandingDllObjectPlacement.Solver_PlaceObject("container", pinnedPlacementDefinition,
            PlacementRules.Count, pinnedPlacementRules, PlacementConstraints.Count, pinnedPlacementConstraints,
            staticPlacementResultPtr);

        SpatialUnderstandingDllObjectPlacement.ObjectPlacementResult placementResult = understandingDll.GetStaticObjectPlacementResult();

        understandingDll.UnpinAllObjects();

        if (result == 0 || placementResult == null) {
            Debug.Log("Object placement query failed");
            return null;
        }

        return placementResult.Clone() as SpatialUnderstandingDllObjectPlacement.ObjectPlacementResult;
    }
}

