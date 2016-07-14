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
        Global.SystemManagers.DebugInformManager.OnDebugInform -= OnDebugInform;
    }

    public void RegisterEvents()
    {
        Global.SystemManagers.DebugInformManager.OnDebugInform += OnDebugInform;
    }

    private void OnDebugInform(string message)
    {
        Debug.Log("Debug: " + message);
    }
}
