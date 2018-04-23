
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using HoloToolkit.Unity;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace Assets.VR.Scripts.UI {

    public class MenuManager : MonoBehaviour {

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

        public void Result(object value) {
            audioSource.PlayOneShot(buttonClick);
    
            var dialog = currentUI.GetComponentInChildren<IDialog>();
            dialogStack.Push(dialog.DialogType);

            Destroy(currentUI);

            switch (dialog.DialogType) {
                case DialogType.Welcome:
                    CreateNewDialog(DialogType.FinalizeScan);
                    break;

                case DialogType.FinalizeScan:
                    CreateNewDialog(DialogType.PlaceLoad);
                    break;

                case DialogType.PlaceLoad:
                    CreateNewDialog(DialogType.PackingMain);
                    break;

                case DialogType.PackingMain:
                    if (value as string == "Load Sequence") {
                        CreateNewDialog(DialogType.PackingSequence);
                    }
                    else if (value as string == "Visualization") {
                        CreateNewDialog(DialogType.PackingVisualizer);
                    }
                    break;
                case DialogType.PackingSequence:
                case DialogType.PackingVisualizer:
                    CreateNewDialog(DialogType.PackingMain);
                    break;

            } 
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
