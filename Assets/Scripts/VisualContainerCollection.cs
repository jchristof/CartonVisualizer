
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

        public VisualContainerCollection(CubeiqContainer.Cubeiq cubeIqData, GameObject cubePrefab, Material materialPrefab) {
            if(cubeIqData == null)
                throw new ArgumentNullException("cubeIqData");
            if(cubePrefab == null)
                throw new ArgumentNullException("cubePrefab");
            if(materialPrefab == null)
                throw new ArgumentNullException("materialPrefab");

            this.cubeIqData = cubeIqData;
            this.cubePrefab = cubePrefab;
            this.materialPrefab = materialPrefab;

            CubeObjects = new List<GameObject>();

            BuildVisualVolumes();
        }

        private readonly CubeiqContainer.Cubeiq cubeIqData;
        private readonly GameObject cubePrefab;
        private readonly Material materialPrefab;

        private Dictionary<string, int> products;
        public List<GameObject> CubeObjects { get; private set; }

        private Bounds containerBounds = new Bounds(Vector3.zero, Vector3.zero);

        void BuildVisualVolumes() {
            foreach (var block in cubeIqData.Blocks.Block) {
                GameObject cube = Object.Instantiate(cubePrefab, VisualizationServices.ToVolume(block.Widthcoord, block.Heightcoord, block.Depthcoord), Quaternion.identity);
                Renderer renderer = cube.GetComponentInChildren<Renderer>();

                var product = cubeIqData.Products.Product.FirstOrDefault(x => x.Productid == block.Productid);

                renderer.material = GetMaterialForContainer(product.Color.ToColor(0.5f));
                cube.transform.localScale = new Vector3(float.Parse(block.Width), float.Parse(block.Height), float.Parse(block.Length));
                cube.transform.GetChild(0).gameObject.name = block.Productid;

                CubeObjects.Add(cube);
                containerBounds.Encapsulate(renderer.bounds);
            }

            var pallet = Object.Instantiate(cubePrefab, new Vector3(containerBounds.center.x - containerBounds.extents.x, -1, containerBounds.center.z - containerBounds.extents.z), Quaternion.identity);
            pallet.name = "pallet";
            pallet.transform.localScale = new Vector3(containerBounds.size.x, 1f, containerBounds.size.z);
            pallet.GetComponentInChildren<Renderer>().material = GetMaterialForContainer(Color.magenta);
            pallet.transform.GetChild(0).gameObject.name = "Pallet";
            CubeObjects.Add(pallet);
        }

        public Vector3 VolumeCenter { get { return containerBounds.center; } }

        private Material GetMaterialForContainer(Color color) {
            var material = new Material(Shader.Find("Standard"));
            material.CopyPropertiesFromMaterial(materialPrefab);
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
