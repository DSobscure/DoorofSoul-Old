using DoorofSoul.Library.General;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SoulPanel : MonoBehaviour
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
