
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts {

    /// <summary>
    /// Produce and manage all of the visual volume elements for a Cubeiq dataset
    /// </summary>
    public class VisualContainerCollection : IDisposable {

        /// <summary>
        /// Create a visual collection of block containers from a cubeiq xml load declation
        /// </summary>
        /// <param name="cubeIqData">load declaration</param>
        /// <param name="cubePrefab">prefab used to create the block volumes</param>
        /// <param name="materialCollection">materials used to visualize the volumes</param>
        /// <param name="originOffset">offset from the origin to place the container</param>
        public VisualContainerCollection(CubeiqContainer.Cubeiq cubeIqData, GameObject cubePrefab,  Material[] materialCollection, Vector3 originOffset) {
            if(cubeIqData == null)
                throw new ArgumentNullException("cubeIqData");
            if(cubePrefab == null)
                throw new ArgumentNullException("cubePrefab");
            if(materialCollection == null)
                throw new ArgumentNullException("materialCollection");
            if(originOffset == null)
                throw new ArgumentNullException("originOffset");

            this.cubeIqData = cubeIqData;
            this.cubePrefab = cubePrefab;
            this.materialCollection = materialCollection;
            this.originOffset = originOffset;

            CubeObjects = new List<GameObject>();

            BuildVisualVolumes();
        }

        private readonly CubeiqContainer.Cubeiq cubeIqData;
        private readonly GameObject cubePrefab;
        private readonly Material[] materialCollection;
        private readonly Vector3 originOffset = new Vector3(0,0,75);

        private Dictionary<string, int> products;
        public List<GameObject> CubeObjects { get; private set; }

        private Bounds containerBounds = new Bounds(Vector3.zero, Vector3.zero);

        void BuildVisualVolumes() {
            foreach (var block in cubeIqData.Blocks.Block) {
                GameObject cube = Object.Instantiate(cubePrefab, VisualizationServices.ToVolume(block.Widthcoord, block.Heightcoord, block.Depthcoord) + originOffset, Quaternion.identity);
                Renderer renderer = cube.GetComponentInChildren<Renderer>();

                var product = cubeIqData.Products.Product.FirstOrDefault(x => x.Productid == block.Productid);

                renderer.material = GetMaterialForContainer(product == null ? Color.magenta : product.Color.ToColor(0.5f));
                cube.transform.localScale = new Vector3(float.Parse(block.Width), float.Parse(block.Height), float.Parse(block.Length));
                cube.transform.GetChild(0).gameObject.name = block.Productid;

                CubeObjects.Add(cube);
                containerBounds.Encapsulate(renderer.bounds);
            }

            var pallet = Object.Instantiate(cubePrefab, new Vector3(containerBounds.center.x - containerBounds.extents.x, -1, containerBounds.center.z - containerBounds.extents.z) + originOffset, Quaternion.identity);
            pallet.name = "pallet";
            pallet.transform.localScale = new Vector3(containerBounds.size.x, 1f, containerBounds.size.z);
            pallet.GetComponentInChildren<Renderer>().material = GetMaterialForContainer(Color.magenta);
            pallet.transform.GetChild(0).gameObject.name = "Pallet";
            CubeObjects.Add(pallet);
        }

        public Vector3 VolumeCenter { get { return containerBounds.center; } }

        private Material GetMaterialForContainer(Color color) {
            var material = new Material(materialCollection[1]);
            material.color = color;
            return material;
        }

        public void Update() {

        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                GameObject[] containers = GameObject.FindGameObjectsWithTag("Container");
                foreach (var container in containers) {
                    Object.Destroy(container);
                }
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~VisualContainerCollection() {
            Dispose(false);
        }
    }
    
}
