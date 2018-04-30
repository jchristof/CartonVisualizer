

using System.Collections.Generic;
using HoloToolkit.Unity.SpatialMapping;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts {
    public class UIServices {

        public static GameObject NewButton(GameObject buttonPrefab, GameObject menu, string text, UnityAction onClickAction) {
            var button = Object.Instantiate(buttonPrefab);
            button.GetComponentInChildren<Text>().text = text;
            button.transform.SetParent(menu.transform);
            var buttonScript = button.GetComponentInChildren<Button>();
            buttonScript.onClick.AddListener(onClickAction);

            return button;
        }


        public void CreatePlanesFromSpatialMap(int minimumFloors) {
            SurfaceMeshesToPlanes.Instance.MakePlanesComplete += (source, args) => {
                List<GameObject> floors = new List<GameObject>();
                floors = SurfaceMeshesToPlanes.Instance.GetActivePlanes(PlaneTypes.Floor);

                // Check to see if we have enough floors (minimumFloors) to start processing.
                if (floors.Count >= minimumFloors) {
                    // Reduce our triangle count by removing any triangles
                    // from SpatialMapping meshes that intersect with active planes.
                    RemoveVertices(SurfaceMeshesToPlanes.Instance.ActivePlanes);

                    // After scanning is over, switch to the secondary (occlusion) material.
                    // SpatialMappingManager.Instance.SetSurfaceMaterial(secondaryMaterial);
                }
                else {
                    // Re-enter scanning mode so the user can find more surfaces before processing.
                    SpatialMappingManager.Instance.StartObserver();

                    // Re-process spatial data after scanning completes.
                    //meshesProcessed = false;
                }
            };
            // Generate planes based on the spatial map.
            SurfaceMeshesToPlanes surfaceToPlanes = SurfaceMeshesToPlanes.Instance;
            if (surfaceToPlanes != null && surfaceToPlanes.enabled) {
                surfaceToPlanes.MakePlanes();
            }
        }

        private void RemoveVertices(IEnumerable<GameObject> boundingObjects) {
            RemoveSurfaceVertices removeVerts = RemoveSurfaceVertices.Instance;
            if (removeVerts != null && removeVerts.enabled) {
                removeVerts.RemoveSurfaceVerticesWithinBounds(boundingObjects);
            }
        }
    }
}
