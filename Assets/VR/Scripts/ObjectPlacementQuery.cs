

using System.Collections.Generic;
using HoloToolkit.Unity;

public class ObjectPlacementQuery {
    public ObjectPlacementQuery() {
        PlacementRules = new List<SpatialUnderstandingDllObjectPlacement.ObjectPlacementRule>();
        PlacementConstraints = new List<SpatialUnderstandingDllObjectPlacement.ObjectPlacementConstraint>();
    }

    public SpatialUnderstandingDllObjectPlacement.ObjectPlacementDefinition PlacementDefinition { get; set; }
    public List<SpatialUnderstandingDllObjectPlacement.ObjectPlacementRule> PlacementRules { get; private set; }
    public List<SpatialUnderstandingDllObjectPlacement.ObjectPlacementConstraint> PlacementConstraints { get; private set;}

//    public void AddRule(SpatialUnderstandingDllObjectPlacement.ObjectPlacementRule rule) {
//        PlacementRules.Add(rule);
//    }
//
//    public void AddConstraint(SpatialUnderstandingDllObjectPlacement.ObjectPlacementConstraint constraint) {
//        PlacementConstraints.Add(constraint);
//    }
}
