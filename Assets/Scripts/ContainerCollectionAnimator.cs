﻿
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
    public class ContainerCollectionAnimator : IVisualCommands {

        /// <summary>
        /// Take a set of container objects to animate in their original positions
        /// </summary>
        /// <param name="containers"></param>
        /// <param name="explodeMaxDistance"></param>
        /// <param name="speed"></param>
        public ContainerCollectionAnimator(GameObject parentContainer, IList<GameObject> containers, float explodeMaxDistance, float time) {
            if(parentContainer == null)
                throw new ArgumentNullException("parentContainer");

            if (containers == null)
                throw new ArgumentNullException("containers");

            this.parentContainer = parentContainer;
            this.containers = containers;

            this.explodeMaxDistance = explodeMaxDistance;
            this.time = time;
        }

        private readonly GameObject parentContainer;
        private readonly IList<GameObject> containers;
        private  IDictionary<GameObject,Vector3> originalPositions;
        private readonly float explodeMaxDistance;
        private readonly float time;

        private Bounds bounds;

        private float distance;

        public void PlaceBottomCenterAt(Vector3 point) {
            bounds = parentContainer.GetComponent<Renderer>().bounds;

            var halfExtents = bounds.extents/2;

            parentContainer.transform.position = new Vector3(point.x - halfExtents.x, point.y + bounds.extents.y, point.z - halfExtents.z);
        }

        public void RotationOn() {
            ShowBoundingVolume();
        }

        public void RotationOff() {
            HideBoundingVolume();
        }

        public void ShowBoundingVolume() {
            parentContainer.GetComponent<MeshRenderer>().enabled = true;
        }

        public void HideBoundingVolume() {
            parentContainer.GetComponent<MeshRenderer>().enabled = false;
        }

        private int showIndex = 0;
        public void ShowFirst() {
            showIndex = 0;
            foreach (var cube in containers) {
                if (cube.name == "Pallet")
                    continue;

                cube.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            }

            containers[0].transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        }

        public void ShowNext() {
            showIndex = Math.Min(containers.Count, ++showIndex);
            foreach (var cube in containers) {
                if (cube.name == "Pallet")
                    continue;

                cube.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            }

            containers[showIndex].transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        }

        public void ShowPrevious() {
            showIndex = Math.Max(0, --showIndex);
            foreach (var cube in containers) {
                if (cube.name == "Pallet")
                    continue;

                cube.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            }

            containers[showIndex].transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        }

        public void ShowAll() {
            showIndex = 0;
            foreach (var cube in containers) {
                if (cube.name == "Pallet")
                    continue;

                cube.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            }
        }

        // Animate each volume from the bottom center of the entire collection along
        // a ray that penetrates the center of that volume. Add an offset from the center to 
        // account for a game object's position
        public void Explode() {
            bounds = parentContainer.GetComponent<Renderer>().bounds;
            originalPositions = new Dictionary<GameObject, Vector3>( );
            foreach (var cube in containers) {
                if (cube.name == "Pallet")
                    continue;          

                Vector3 fromPosition = bounds.center - new Vector3(0, bounds.extents.y, 0);
                var cubeBounds = cube.GetComponentInChildren<Renderer>().bounds;
                Vector3 toPosition = cubeBounds.center - cubeBounds.extents;
                Vector3 direction = toPosition - fromPosition;

                var rayNormal = direction.normalized;

                if(!originalPositions.ContainsKey(cube))
                    originalPositions.Add(cube, cube.transform.position);

                iTween.MoveTo(cube, toPosition + rayNormal * explodeMaxDistance, time);
            }
        }

        public void Compact() {
            foreach (var kvp in originalPositions) 
                iTween.MoveTo(kvp.Key, kvp.Value, time);

            originalPositions.Clear();
        }

        void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f) {
            GameObject myLine = new GameObject();
            myLine.transform.position = start;
            myLine.AddComponent<LineRenderer>();
            LineRenderer lr = myLine.GetComponent<LineRenderer>();
            lr.material = new Material(Shader.Find("Legacy Shaders/Transparent/Diffuse"));
            lr.SetColors(color, color);
            lr.SetWidth(0.01f, 0.01f);
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
            //GameObject.Destroy(myLine, duration);
        }
    }  
}
