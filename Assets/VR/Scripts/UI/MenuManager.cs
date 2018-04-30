
using System;
using System.Collections.Generic;
using System.Linq;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace Assets.VR.Scripts.UI {

    public class MenuManager : MonoBehaviour, IInputClickHandler {

        [Serializable]
        public struct DialogMenus {
            public DialogType type;
            public GameObject dialog;
        }
        public DialogMenus[] dialogs;

        public AudioClip buttonClick;

        private GameObject currentUI;
        private AudioSource audioSource;

        private KeywordRecognizer keywordRecognizer;
        private TextToSpeech textToSpeech;
        private Stack<DialogType> dialogStack = new Stack<DialogType>();

        void Start () {

            keywordRecognizer = new KeywordRecognizer(new []{"main menu", "back"});
            keywordRecognizer.OnPhraseRecognized += KeywordRecognizerOnOnPhraseRecognized;
            keywordRecognizer.Start();

            textToSpeech = gameObject.GetComponentInChildren<TextToSpeech>();
            audioSource = gameObject.GetComponentInChildren<AudioSource>();

            CreateNewDialog(DialogType.Welcome);

            SurfaceMeshesToPlanes.Instance.MakePlanesComplete += InstanceOnMakePlanesComplete;
            InputManager.Instance.PushFallbackInputHandler(gameObject);
        }

        private int minimumFloors;

        private void InstanceOnMakePlanesComplete(object source, EventArgs args) {
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
        }

        private void CreatePlanes() {
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

        private void KeywordRecognizerOnOnPhraseRecognized(PhraseRecognizedEventArgs args) {
            if (args.text == "main menu") {
                if (currentUI == null)
                    return;

                Destroy(currentUI);

                var packingMainMenu = dialogs.FirstOrDefault(x=>x.type == DialogType.PackingMain);
                currentUI = Instantiate(packingMainMenu.dialog, gameObject.transform);
                currentUI.GetComponentInChildren<IDialog>().DialogResult = Result;

                textToSpeech.StartSpeaking(TitleText(currentUI) + MessageText(currentUI));
            }
            else if (args.text == "back") {
                var dialog = dialogStack.Count > 0 ? dialogStack.Pop() : DialogType.None;
                if (dialog == DialogType.None) {
                    currentUI = null;
                    return;
                }

                CreateNewDialog(dialog);
            }
        }

        private void CreateNewDialog(DialogType dialogType) {

            var dialog = dialogs.FirstOrDefault(x => x.type == dialogType);

            dialogStack.Push(dialogType);
            currentUI = Instantiate(dialog.dialog, gameObject.transform);
            currentUI.GetComponentInChildren<IDialog>().DialogResult = Result;

            SpeakTheMenuText(currentUI);
        }

        private void GoBack() {
            if (dialogStack.Count == 0)
                return;

            audioSource.PlayOneShot(buttonClick);
            Destroy(currentUI);

            var dialogType = dialogStack.Pop();
            CreateNewDialog(dialogType);
        }

        private string selectedLoad;

        public void Result(object value) {
            audioSource.PlayOneShot(buttonClick);
    
            var dialog = currentUI.GetComponentInChildren<IDialog>();

            Destroy(currentUI);

            switch (dialog.DialogType) {
                case DialogType.Welcome:
                    CreateNewDialog(DialogType.FinalizeScan);
                    break;

                case DialogType.FinalizeScan:
                    CreateNewDialog(DialogType.SelectLoad);
                    CreatePlanes();
                    break;

                case DialogType.PlaceLoad:
                    CreateNewDialog(DialogType.PackingMain);
                    break;

                case DialogType.PackingMain:
                    if (value as string == "Load Sequence") 
                        CreateNewDialog(DialogType.PackingSequence);
                    
                    else if (value as string == "Visualization") 
                        CreateNewDialog(DialogType.PackingVisualizer);
                    
                    break;
                case DialogType.SelectLoad:
                    CreateNewDialog(DialogType.PlaceLoad);
                    selectedLoad = value.ToString();
//                    if (value == "load 1")
//                        GameObject.Find("Container").GetComponentInChildren<ContainerVisualizer>().LoadOne();
//                    else if (value == "load 2")
//                        GameObject.Find("Container").GetComponentInChildren<ContainerVisualizer>().LoadTwo();
                    break;

                case DialogType.PackingSequence:
                case DialogType.PackingVisualizer:
                    //CreateNewDialog(DialogType.PackingMain);
                    break;
            } 
        }

        private void SpeakTheMenuText(GameObject currentUI) {
            var titleText = TitleText(currentUI).Replace("\n", "").Replace("\r", "");
            var message = MessageText(currentUI).Replace("\n", "").Replace("\r", "");

            string ssml = String.Format(@"<?xml version='1.0' encoding='utf-8'?>
                <speak
                  version=""1.0""
                  xmlns=""http://www.w3.org/2001/10/synthesis""
                  xml:lang=""en-US"">
                  {0} <break time=""1s""/> {1}
                </speak>", titleText, message);

            textToSpeech.SpeakSsml(ssml);
        }

        private static string TitleText(GameObject currentUI) {
            if (currentUI == null)
                return "";

            var titleTextObject = currentUI.transform.Find("TitleText");

            if (titleTextObject == null)
                return "";

            var textMesh = titleTextObject.GetComponentInChildren<TextMesh>();

            if (textMesh == null)
                return "";

            return textMesh.text;
        }

        private static string MessageText(GameObject currentUI) {
            if (currentUI == null)
                return "";

            var titleTextObject = currentUI.transform.Find("TitleMessage");

            if (titleTextObject == null)
                return "";

            var textMesh = titleTextObject.GetComponentInChildren<TextMesh>();

            if (textMesh == null)
                return "";

            return textMesh.text;
        }

        public void OnInputClicked(InputClickedEventData eventData) {
            if (eventData.used)
                return;

            eventData.Use();
            GoBack();
        }

        void OnDestroy() {
            InputManager.Instance.PopFallbackInputHandler();
        }
    }

}
