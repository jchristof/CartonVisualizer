

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts {
    public class UIServices {

        public static GameObject NewButton(GameObject buttonPrefab, string text, UnityAction onClickAction) {
            var button = Object.Instantiate(buttonPrefab);
            button.GetComponentInChildren<Text>().text = text;
            button.transform.SetParent(GameObject.Find("Content").transform);
            var buttonScript = button.GetComponentInChildren<Button>();
            buttonScript.onClick.AddListener(onClickAction);

            return button;
        } 
    }
}
