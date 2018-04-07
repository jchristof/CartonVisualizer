
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace Assets.Scripts.Input {
    public class VoiceCommander : MonoBehaviour {

        public void Start() {
            keywords.Add("show room", () =>
            {
                GameObject.Find("SpatialUnderstanding").SetActive(true);
            });

            keywords.Add("hide rooom", () => {
                GameObject.Find("SpatialUnderstanding").SetActive(false);
            });

            keywords.Add("load one", () => {
                GameObject.Find("Container").GetComponentInChildren<ContainerVisualizer>().LoadOne();
            });

            keywords.Add("load two", () => {
                GameObject.Find("Container").GetComponentInChildren<ContainerVisualizer>().LoadTwo();
            });

            keywords.Add("explode", () => {
                GameObject.Find("Container").GetComponentInChildren<ContainerVisualizer>().Explode();
            });

            keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

            keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;

            keywordRecognizer.Start();
        }

        private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args) {
            System.Action keywordAction;
            // if the keyword recognized is in our dictionary, call that Action.
            if (keywords.TryGetValue(args.text, out keywordAction)) {
                keywordAction.Invoke();
            }
        }

        KeywordRecognizer keywordRecognizer;
        Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    }
}
