using DoorofSoul.Client.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLogoutController : MonoBehaviour, IEventProvider
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
        Global.ResponseManagers.ResponseManager.OnPlayerLogout -= PlayerLogout;
    }

    public void RegisterEvents()
    {
        Global.ResponseManagers.ResponseManager.OnPlayerLogout += PlayerLogout;
    }

    private void PlayerLogout()
    {
        SceneManager.LoadScene("PlayerLoginScene");
    }
}
