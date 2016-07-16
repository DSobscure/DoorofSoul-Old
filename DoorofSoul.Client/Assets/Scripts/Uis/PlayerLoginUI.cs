using UnityEngine;
using UnityEngine.UI;
using DoorofSoul.Client.Interfaces;
using System;

public class PlayerLoginUI : MonoBehaviour, IEventProvider
{
    [SerializeField]
    private InputField accountInputField;
    [SerializeField]
    private InputField passwordInputField;
    [SerializeField]
    private Button playerLoginButton;

    void Awake()
    {
        RegisterEvents();
    }
    void OnDestroy()
    {
        EraseEvents();
    }

    public void RegisterEvents()
    {
        Global.ResponseManagers.UIResponseManager.OnPlayerLoginResult += OnPlayerLoginResult;
    }

    public void EraseEvents()
    {
        Global.ResponseManagers.UIResponseManager.OnPlayerLoginResult -= OnPlayerLoginResult;
    }

    public void PlayerLogin()
    {
        Global.OperationManagers.OperationManager.PlayerLogin(accountInputField.text, passwordInputField.text);
        accountInputField.enabled = false;
        passwordInputField.enabled = false;
        playerLoginButton.enabled = false;
    }
    public void OnPlayerLoginResult()
    {
        accountInputField.enabled = true;
        passwordInputField.enabled = true;
        playerLoginButton.enabled = true;
    }
}
