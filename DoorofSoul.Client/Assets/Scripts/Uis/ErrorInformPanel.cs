using UnityEngine;
using UnityEngine.UI;
using DoorofSoul.Client.Global;
using System;

public class ErrorInformPanel : MonoBehaviour
{
    private Text errorMessageText;
    private RectTransform errorMessageContent;


    public void ShowMessage(string message)
    {
        errorMessageText = GameObject.Find("ErrorMessageText").GetComponent<Text>();
        if (errorMessageText == null)
        {
            SystemManager.Error("ErrorInformPanel errorMessageText is null");
        }
        errorMessageContent = GameObject.Find("ErrorMessageContent").GetComponent<RectTransform>();
        if (errorMessageContent == null)
        {
            SystemManager.Error("ErrorMessageContent errorMessageContent is null");
        }
        GetComponent<RectTransform>().localScale = Vector3.one;
        GetComponent<RectTransform>().localPosition = Vector3.zero;

        errorMessageText.text = message;
        errorMessageContent.sizeDelta = new Vector2(errorMessageContent.rect.width, errorMessageText.preferredHeight);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
