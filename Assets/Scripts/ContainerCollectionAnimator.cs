

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts {
    public class ContainerCollectionAnimator {

        /// <summary>
        /// Take a set of container objects to animate in their original positions
        /// </summary>
        /// <param name="containers"></param>
        /// <param name="explodeMaxDistance"></param>
        /// <param name="speed"></param>
        public ContainerCollectionAnimator(IList<GameObject> containers, float explodeMaxDistance, float speed) {
            if(containers == null)
                throw new ArgumentNullException("containers");


            this.containers = containers;
            this.explodeMaxDistance = explodeMaxDistance;
            this.speed = speed;

            bounds = new Bounds(Vector3.zero, Vector3.zero);
            foreach (var cube in containers) {
                Renderer renderer = cube.GetComponentInChildren<Renderer>();

                if (renderer == null) {
                    Debug.Log("Cube renderer null. Can't evaluate container volume");
                    return;
                }

                bounds.Encapsulate(renderer.bounds);
            }
        }

        private readonly IList<GameObject> containers;
        private readonly float explodeMaxDistance;
        private readonly float speed;

        private Bounds bounds;

        private float distance;
        private UnityAction updateAction;

        public void StepForward() {

        }

        public void Run() {
            updateAction = () => {

                if (distance > explodeMaxDistance)
                    updateAction = null;

                foreach (var cube in containers) {
                    if(cube.name == "Pallet")
                        continue;

                    Vector3 fromPosition = bounds.center - new Vector3(0, bounds.extents.y/2, 0);
                    Vector3 toPosition = cube.GetComponentInChildren<Renderer>().bounds.center + new Vector3(0, .5f, 0);
                    Vector3 direction = toPosition - fromPosition;

                    var rayNormal = direction.normalized;

                    cube.transform.Translate(rayNormal * Mathf.Sin(distance / 10f) * Time.deltaTime * 10f);
                }

                distance += speed;
            };
        }

        public void StepBackward() {

        }

        public void Update() {
            if (updateAction != null)
                updateAction();
        }
    }  
}
