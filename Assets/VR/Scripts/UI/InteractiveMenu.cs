using System;
using System.Linq;
using HoloToolkit.Unity.Buttons;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.Receivers;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace Assets.VR.Scripts.UI {

    public abstract class InteractiveMenu : InteractionReceiver, IDialog {

        private KeywordRecognizer keywordRecognizer;

        // Register this menu's compound button objects
        public virtual void Start () {
            iTween.FadeFrom(gameObject, 0, 3f);
            iTween.ScaleFrom(gameObject, Vector3.zero, 1f);
            var compoundButtonsText = gameObject.GetComponentsInChildren<CompoundButtonText>();
            interactables = compoundButtonsText.Select(x => x.gameObject).ToList();

            var buttonText = compoundButtonsText.Select(x => x.Text).ToArray();

            if (buttonText.Any()) {
                keywordRecognizer = new KeywordRecognizer(buttonText.ToArray());
                keywordRecognizer.OnPhraseRecognized += args => {
                    ButtonInputDown(args.text);
                };

                keywordRecognizer.Start();
            }

        }

        void OnDestroy() {
        }

        protected override void InputDown(GameObject obj, InputEventData eventData) {
            base.InputDown(obj, eventData);

            if (!eventData.used) {
       
                ButtonInputDown(obj.GetComponent<CompoundButtonText>().Text);
                eventData.Use();
            }
        }

        protected virtual void ButtonInputDown(string buttonId) { }

        // Call to return some value to the menu manger and close the dialog
        public Action<object> DialogResult { get; set; }

        // Provide this menu's type
        public abstract DialogType DialogType { get; }
    }

}
