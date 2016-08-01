using UnityEngine;
using DoorofSoul.Client.Interfaces;
using DoorofSoul.Protocol.Language;
using UnityEngine.SceneManagement;
using DoorofSoul.Client.Global;
using DoorofSoul.Library.General;

public class SystemVersionController : MonoBehaviour, IEventProvider
{
    private SystemManager systemManager;
    private Player player;

    void Awake()
    {
        systemManager = Global.SystemManager;
        player = Global.Player;
        RegisterEvents();
    }
    void OnDestroy()
    {
        EraseEvents();
    }
    void OnGUI()
    {
        GUI.Label(new Rect(20, 30, 300, 20), string.Format("CurrentServer Version: {0}", systemManager.CurrentServerVersion));
        GUI.Label(new Rect(20, 50, 300, 20), string.Format("CurrentClient Version: {0}", systemManager.CurrentClientVersion));
        GUI.Label(new Rect(20, 70, 300, 20), string.Format("LocalClient Version: {0}", systemManager.LocalClientVersion));
    }

    public void EraseEvents()
    {
        systemManager.OnCurrentServerVersionChange -= OnCurrentServerVersionChange;
        systemManager.OnCurrentClientVersionChange -= OnCurrentClientVersionChange;
    }

    public void RegisterEvents()
    {
        systemManager.OnCurrentServerVersionChange += OnCurrentServerVersionChange;
        systemManager.OnCurrentClientVersionChange += OnCurrentClientVersionChange;
    }

    private void OnCurrentServerVersionChange(string version)
    {

    }
    private void OnCurrentClientVersionChange(string version)
    {
        if(!systemManager.ClientVersionCheck())
        {
            player.PlayerEventManager.ErrorInform(LauguageDictionarySelector.Instance[player.UsingLanguage]["Fail"], LauguageDictionarySelector.Instance[player.UsingLanguage]["Client Version Inconsistent"]);
        }
        else
        {
            SceneManager.LoadScene("PlayerLoginScene");
        }
    }
}
