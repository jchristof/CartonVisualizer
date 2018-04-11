
using System;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class SpatialMapState : Singleton<SpatialMapState> {

    private Action updateAction = () => { };

    private SpatialUnderstanding spatialUnderstanding;
    private SpatialUnderstanding.ScanStates lastScanState;

    public SpatialUnderstandingDll.Imports.PlayspaceStats SpatialStats { get; private set; }
    public SpatialUnderstanding.ScanStates SpatialScanState { get { return spatialUnderstanding.ScanState; } }

    void Start () {
        
        SpatialStats = new SpatialUnderstandingDll.Imports.PlayspaceStats();
        spatialUnderstanding = SpatialUnderstanding.Instance;
        lastScanState = spatialUnderstanding.ScanState;
    }

    public void FinishScanning() {
        spatialUnderstanding.RequestFinishScan();
    }
	
	void Update () {

        if (updateAction != null)
            updateAction();

        if (lastScanState == spatialUnderstanding.ScanState)
	        return;

	    switch (spatialUnderstanding.ScanState) {
            case SpatialUnderstanding.ScanStates.None:
                updateAction = () => { };
                break;
            case SpatialUnderstanding.ScanStates.Done:
                updateAction = () => { };
                break;
            case SpatialUnderstanding.ScanStates.ReadyToScan:
                updateAction = () => { };
                break;
            case SpatialUnderstanding.ScanStates.Scanning:
                updateAction = () => {
                    IntPtr statsPtr = spatialUnderstanding.UnderstandingDLL.GetStaticPlayspaceStatsPtr();
                    if (SpatialUnderstandingDll.Imports.QueryPlayspaceStats(statsPtr) == 0) {
                        Debug.Log("Load space stats query failed");
                        return;
                    }

                    SpatialStats = spatialUnderstanding.UnderstandingDLL.GetStaticPlayspaceStats();
                };
                break;
            case SpatialUnderstanding.ScanStates.Finishing:
                updateAction = () => { };
                break;
        }

	    lastScanState = spatialUnderstanding.ScanState;
	}
}
