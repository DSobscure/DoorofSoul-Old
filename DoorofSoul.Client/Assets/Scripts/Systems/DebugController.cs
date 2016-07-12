using UnityEngine;
using DoorofSoul.Client.Interfaces;

public class DebugController : MonoBehaviour, IEventProvider
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
        Global.SystemManagers.DebugInformManager.EraseDebugInformFunction(OnDebugInform);
    }

    public void RegisterEvents()
    {
        Global.SystemManagers.DebugInformManager.RegisterDebugInformFunction(OnDebugInform);
    }

    private void OnDebugInform(string message)
    {
        Debug.Log("Debug: " + message);
    }
}
