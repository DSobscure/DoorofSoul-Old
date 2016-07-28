using DoorofSoul.Client.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using DoorofSoul.Client.Global;
using DoorofSoul.Client.Library.General;

public class PlayerLogoutController : MonoBehaviour, IEventProvider
{
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

    public void EraseEvents()
    {
        player.OnLogout -= Logout;
    }

    public void RegisterEvents()
    {
        player.OnLogout += Logout;
    }

    private void Logout()
    {
        SceneManager.LoadScene("PlayerLoginScene");
    }
}
