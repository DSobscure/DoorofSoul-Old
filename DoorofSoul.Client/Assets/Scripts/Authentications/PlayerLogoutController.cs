using DoorofSoul.Client.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using DoorofSoul.Client.Global;
using DoorofSoul.Library.General;

public class PlayerLogoutController : MonoBehaviour, IEventProvider
{
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

    public void EraseEvents()
    {
        player.OnOnline -= Logout;
    }

    public void RegisterEvents()
    {
        player.OnOnline += Logout;
    }

    private void Logout(Player player)
    {
        if(!player.IsOnline && SceneManager.GetActiveScene().name != "PlayerLoginScene")
        {
            SceneManager.LoadScene("PlayerLoginScene");
        }
    }
}
