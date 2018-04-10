

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts {
    public class UIServices {

        public static GameObject NewButton(GameObject buttonPrefab, GameObject menu, string text, UnityAction onClickAction) {
            var button = Object.Instantiate(buttonPrefab);
            button.GetComponentInChildren<Text>().text = text;
            button.transform.SetParent(menu.transform);
            var buttonScript = button.GetComponentInChildren<Button>();
            buttonScript.onClick.AddListener(onClickAction);

            return button;
        } 
    }
}
