
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using HoloToolkit.Unity;
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

        public AudioClip buttonClick;

        private GameObject currentUI;
        private AudioSource audioSource;

        private KeywordRecognizer keywordRecognizer;
        private TextToSpeech textToSpeech;
        private Stack<DialogType> dialogStack = new Stack<DialogType>();

        void Start () {
            currentUI = Instantiate(welcomeDialog, gameObject.transform); 
            currentUI.GetComponentInChildren<IDialog>().DialogResult = Result;

            keywordRecognizer = new KeywordRecognizer(new []{"main menu"});
            keywordRecognizer.OnPhraseRecognized += KeywordRecognizerOnOnPhraseRecognized;
            keywordRecognizer.Start();

            textToSpeech = gameObject.GetComponentInChildren<TextToSpeech>();
            audioSource = gameObject.GetComponentInChildren<AudioSource>();

            SpeakTheMenuText(currentUI);
        }

        private void KeywordRecognizerOnOnPhraseRecognized(PhraseRecognizedEventArgs args) {
            if (args.text == "main menu") {
                if (currentUI == null)
                    return;

                Destroy(currentUI);

                currentUI = Instantiate(packingMain, gameObject.transform);
                currentUI.GetComponentInChildren<IDialog>().DialogResult = Result;

                textToSpeech.StartSpeaking(TitleText(currentUI) + MessageText(currentUI));
            }
        }

        public void Result(object value) {
            audioSource.PlayOneShot(buttonClick);
    
            var dialog = currentUI.GetComponentInChildren<IDialog>();
            dialogStack.Push(dialog.DialogType);

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

            SpeakTheMenuText(currentUI);
        }

        private void SpeakTheMenuText(GameObject currentUI) {
            var titleText = TitleText(currentUI).Replace("\n", "").Replace("\r", "");
            var message = MessageText(currentUI).Replace("\n", "").Replace("\r", "");

            string ssml = String.Format(@"<speak>{0} <break time=""1s""/> {1}</speak>", titleText, message);
            string ssmlx = String.Format(@"<?xml version='1.0' encoding='utf-8'?>
                <speak
                  version=""1.0""
                  xmlns=""http://www.w3.org/2001/10/synthesis""
                  xml:lang=""en-US"">
                  {0} <break time=""1s""/> {1}
                </speak>", titleText, message);

            textToSpeech.SpeakSsml(ssmlx);
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
    }

}
