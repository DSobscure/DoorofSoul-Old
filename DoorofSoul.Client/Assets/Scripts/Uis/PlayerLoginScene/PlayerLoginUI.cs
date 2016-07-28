using DoorofSoul.Library.General;
using UnityEngine;
using UnityEngine.UI;
using DoorofSoul.Client.Library.General;
using DoorofSoul.Client.Global;
using DoorofSoul.Client.Interfaces;

public class PlayerLoginUI : MonoBehaviour, IEventProvider
{
    private ClientPlayer player;

    [SerializeField]
    private InputField accountInputField;
    [SerializeField]
    private InputField passwordInputField;
    [SerializeField]
    private Button playerLoginButton;

    public string Account { get { return accountInputField.text; } }
    public string Password { get { return passwordInputField.text; } }

    void Awake()
    {
        player = Global.Player;
        RegisterEvents();
    }
    void OnDestroy()
    {
        EraseEvents();
    }

    public void RegisterEvents()
    {
        player.OnLogin += OnLoginResponse;
    }

    public void EraseEvents()
    {
        player.OnLogin -= OnLoginResponse;
    }
    public void PlayerLogin()
    {
        accountInputField.enabled = false;
        passwordInputField.enabled = false;
        playerLoginButton.enabled = false;
    }
    public void OnLoginResponse(Player player)
    {
        if(!player.IsOnline)
        {
            accountInputField.enabled = true;
            passwordInputField.enabled = true;
            playerLoginButton.enabled = true;
        }
    }
}
