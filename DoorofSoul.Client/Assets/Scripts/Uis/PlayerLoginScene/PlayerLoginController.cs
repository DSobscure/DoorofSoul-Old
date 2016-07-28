using DoorofSoul.Client.Global;
using DoorofSoul.Client.Interfaces;
using DoorofSoul.Client.Library.General;
using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLoginController : MonoBehaviour, IEventProvider
{
    [SerializeField]
    private PlayerLoginUI playerLoginUI;

    private ClientPlayer player;

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
        player.OnActiveAnswer += OnActiveAnswer;
    }

    public void EraseEvents()
    {
        player.OnActiveAnswer -= OnActiveAnswer;
    }
    public void PlayerLogin()
    {
        string debugMessage;
        ErrorCode errorCode;
        player.Login(playerLoginUI.Account, playerLoginUI.Password, out debugMessage, out errorCode);
        playerLoginUI.PlayerLogin();
    }
    public void OnActiveAnswer(Answer answer)
    {
        SceneManager.LoadScene("PlayerAnswerScene");
    }
}
