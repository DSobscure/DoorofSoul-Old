using UnityEngine;
using DoorofSoul.Client.Interfaces;
using DoorofSoul.Client.Communication;

public class SystemVersionController : MonoBehaviour, IEventProvider
{
    void Awake()
    {
        RegisterEvents();
    }
    void OnDestroy()
    {
        EraseEvents();
    }
    void OnGUI()
    {
        GUI.Label(new Rect(20, 30, 300, 20), string.Format("CurrentServer Version: {0}", Global.VersionManager.CurrentServerVersion));
        GUI.Label(new Rect(20, 50, 300, 20), string.Format("CurrentClient Version: {0}", Global.VersionManager.CurrentClientVersion));
        GUI.Label(new Rect(20, 70, 300, 20), string.Format("LocalClient Version: {0}", Global.VersionManager.LocalClientVersion));
    }

    public void EraseEvents()
    {
        Global.VersionManager.EraseCurrentServerVersionChangeFunction(CurrentServerVersionChange);
        Global.VersionManager.EraseCurrentClientVersionChangeFunction(CurrentClientVersionChange);
    }

    public void RegisterEvents()
    {
        Global.VersionManager.RegisterCurrentServerVersionChangeFunction(CurrentServerVersionChange);
        Global.VersionManager.RegisterCurrentClientVersionChangeFunction(CurrentClientVersionChange);
    }

    private void CurrentServerVersionChange(string version)
    {

    }
    private void CurrentClientVersionChange(string version)
    {
        if(!Global.VersionManager.ClientVersionCheck())
        {
            Global.SystemManagers.SystemInformManager.ErrorInform(LauguageDictionarySelector.Instance[Global.SystemManagers.UsingLauguage]["Client Version Inconsistent"]);
        }
    }
}
