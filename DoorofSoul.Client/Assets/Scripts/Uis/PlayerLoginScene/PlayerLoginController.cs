using DoorofSoul.Client.Global;
using DoorofSoul.Client.Interfaces;
using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLoginController : MonoBehaviour, IEventProvider
{
    [SerializeField]
    private PlayerLoginUI playerLoginUI;

    private Player player;

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
        player.PlayerOperationManager.Login(playerLoginUI.Account, playerLoginUI.Password);
        playerLoginUI.PlayerLogin();
    }
    public void OnActiveAnswer(Answer answer)
    {
        SceneManager.LoadScene("PlayerAnswerScene");
    }
}
