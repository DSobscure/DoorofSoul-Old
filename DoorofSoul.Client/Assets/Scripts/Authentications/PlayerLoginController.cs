using DoorofSoul.Client.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using DoorofSoul.Library.General;

public class PlayerLoginController : MonoBehaviour, IEventProvider
{
    void Awake()
    {
        RegisterEvents();
    }
    void OnDestroy()
    {
        EraseEvents();
    }

    public void EraseEvents()
    {
        Global.ResponseManagers.ResponseManager.OnPlayerLogin -= PlayerLogin;
    }

    public void RegisterEvents()
    {
        Global.ResponseManagers.ResponseManager.OnPlayerLogin += PlayerLogin;
    }

    private void PlayerLogin(Player player)
    {
        Global.Player = player;
        SceneManager.LoadScene("PlayerAnswerScene");
    }
}
