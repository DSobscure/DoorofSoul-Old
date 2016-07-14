using UnityEngine;
using UnityEngine.UI;
using DoorofSoul.Client.Interfaces;
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
            Global.SystemManagers.DebugInformManager.DebugInform("ErrorInformPanel doesn't have text");
        }
        errorMessageContent = GameObject.Find("ErrorMessageContent").GetComponent<RectTransform>();
        if (errorMessageContent == null)
        {
            Global.SystemManagers.DebugInformManager.DebugInform("ErrorMessageContent doesn't have content");
        }
        GetComponent<RectTransform>().localScale = Vector3.one;
        GetComponent<RectTransform>().localPosition = Vector3.zero;

        errorMessageText.text = message;
        errorMessageContent.sizeDelta = new Vector2(errorMessageText.preferredWidth, errorMessageText.preferredHeight);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
