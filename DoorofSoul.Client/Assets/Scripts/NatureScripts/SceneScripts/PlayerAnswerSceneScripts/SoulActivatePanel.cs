using DoorofSoul.Library.General.MindComponents;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DoorofSoul.Client.Scripts.NatureScripts.SceneScripts.PlayerAnswerSceneScripts
{
    public class SoulActivatePanel : MonoBehaviour
    {
        private Text soulIDText;
        private Text soulNameText;
        public Button activeButton;

        public void Show(Soul soul)
        {
            soulIDText = transform.FindChild("SoulIDText").GetComponent<Text>();
            soulNameText = transform.FindChild("SoulNameText").GetComponent<Text>();
            activeButton = transform.FindChild("ActiveButton").GetComponent<Button>();
            soulIDText.text = soul.SoulID.ToString();
            soulNameText.text = soul.SoulName;
        }

        public void SetButton(string text, UnityAction onClickButtonEvent)
        {
            activeButton.GetComponentInChildren<Text>().text = text;
            activeButton.onClick.AddListener(onClickButtonEvent);
        }
    }
}
