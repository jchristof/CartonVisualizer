
using System;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class SpatialMapState : Singleton<SpatialMapState>, IInputClickHandler, ISourceStateHandler {

    private Action updateAction = () => { };

    private SpatialUnderstanding spatialUnderstanding;
    private SpatialUnderstanding.ScanStates lastScanState;

    public SpatialUnderstandingDll.Imports.PlayspaceStats SpatialStats { get; private set; }
    public SpatialUnderstanding.ScanStates SpatialScanState { get { return spatialUnderstanding.ScanState; } }

    void Start () {
        InputManager.Instance.PushFallbackInputHandler(gameObject);
        SpatialStats = new SpatialUnderstandingDll.Imports.PlayspaceStats();
        spatialUnderstanding = SpatialUnderstanding.Instance;
        lastScanState = spatialUnderstanding.ScanState;
    }
	
	void Update () {

        if (updateAction != null)
            updateAction();

        if (lastScanState == spatialUnderstanding.ScanState)
	        return;

	    switch (spatialUnderstanding.ScanState) {
            case SpatialUnderstanding.ScanStates.None:
                updateAction = SpatialScanStateNone;
                break;
            case SpatialUnderstanding.ScanStates.Done:
                updateAction = SpatialScanStateDone;
                break;
            case SpatialUnderstanding.ScanStates.ReadyToScan:
                updateAction = SpacialScanStateReadyToScan;
                break;
            case SpatialUnderstanding.ScanStates.Scanning:
                updateAction = SpatialScanStateScanning;
                break;
            case SpatialUnderstanding.ScanStates.Finishing:
                updateAction = SpatialScanStateFinishing;
                break;
        }

	    lastScanState = spatialUnderstanding.ScanState;


	}

    private void SpatialScanStateNone() {
        
    }

    private void SpatialScanStateDone() {

    }

    private void SpatialScanStateFinishing() {

    }

    private void SpacialScanStateReadyToScan() {
        
    }

    private void SpatialScanStateScanning() {

        IntPtr statsPtr = spatialUnderstanding.UnderstandingDLL.GetStaticPlayspaceStatsPtr();
        if (SpatialUnderstandingDll.Imports.QueryPlayspaceStats(statsPtr) == 0) {
            Debug.Log("Load space stats query failed");
            return;
        }

        SpatialStats = spatialUnderstanding.UnderstandingDLL.GetStaticPlayspaceStats();
    }

    public void OnInputClicked(InputClickedEventData eventData) {
        
    }

    public void OnSourceDetected(SourceStateEventData eventData) {
        
    }

    public void OnSourceLost(SourceStateEventData eventData) {
        
    }

}
