using System;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace Assets.VR.Scripts.UI {

    public class MenuManager : MonoBehaviour {

        public GameObject welcomeDialog;
        public GameObject scanOrLoadDialog;
        public GameObject placeLoadDialog;
        public GameObject packingMain;
        public GameObject packingSequence;
        public GameObject packingPlacement;
        public GameObject packingVisualization;

        private GameObject currentUI;

        private KeywordRecognizer keywordRecognizer;

        void Start () {
            currentUI = Instantiate(welcomeDialog, gameObject.transform);
            currentUI.GetComponentInChildren<IDialog>().DialogResult = Result;

            keywordRecognizer = new KeywordRecognizer(new []{"main menu"});
            keywordRecognizer.OnPhraseRecognized += KeywordRecognizerOnOnPhraseRecognized;
            keywordRecognizer.Start();
        }

        private void KeywordRecognizerOnOnPhraseRecognized(PhraseRecognizedEventArgs args) {
            if (args.text == "main menu") {
                if (currentUI == null)
                    return;

                Destroy(currentUI);

                currentUI = Instantiate(packingMain, gameObject.transform);
                currentUI.GetComponentInChildren<IDialog>().DialogResult = Result;
            }
        }

        public void Result(object value) {
            var dialog = currentUI.GetComponentInChildren<IDialog>();
            Destroy(currentUI);

            if (dialog.DialogType == DialogType.Welcome) {
                currentUI = Instantiate(scanOrLoadDialog, gameObject.transform);
                currentUI.GetComponentInChildren<IDialog>().DialogResult = Result;
            }
            else if (dialog.DialogType == DialogType.FinalizeScan) {
                currentUI = Instantiate(placeLoadDialog, gameObject.transform);
                currentUI.GetComponentInChildren<IDialog>().DialogResult = Result;
            }
            else if (dialog.DialogType == DialogType.PlaceLoad) {
                currentUI = Instantiate(packingMain, gameObject.transform);
                currentUI.GetComponentInChildren<IDialog>().DialogResult = Result;
            }
            else if (dialog.DialogType == DialogType.PackingMain) {
                if (value as string == "Load Sequence") {
                    currentUI = Instantiate(packingSequence, gameObject.transform);
                    currentUI.GetComponentInChildren<IDialog>().DialogResult = Result;
                }
                else if (value as string == "Visualization") {
                    currentUI = Instantiate(packingVisualization, gameObject.transform);
                    currentUI.GetComponentInChildren<IDialog>().DialogResult = Result;
                }
            }
            else if (dialog.DialogType == DialogType.PackingVisualizer) {
                currentUI = Instantiate(packingMain, gameObject.transform);
                currentUI.GetComponentInChildren<IDialog>().DialogResult = Result;
            }

        }
    }

}
