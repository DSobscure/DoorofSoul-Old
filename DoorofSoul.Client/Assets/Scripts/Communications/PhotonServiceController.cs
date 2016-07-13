using UnityEngine;
using DoorofSoul.Client.Interfaces;
using System;

public class PhotonServiceController : MonoBehaviour, IEventProvider
{
    void Awake()
    {
        RegisterEvents();
    }
    void Start()
    {
        if(Global.PhotonService.ServerConnected)
        {
            Global.SystemManagers.DebugInformManager.DebugInform("Already Connected");
        }
        else
        {
            Global.PhotonService.Connect();
            Global.SystemManagers.DebugInformManager.DebugInform("Start Connecting");
        }
    }

    void OnGUI()
    {
        if(Global.PhotonService.ServerConnected)
        {
            GUI.Label(new Rect(20, 10, 100, 20), "connected");
        }
        else
        {
            GUI.Label(new Rect(20, 10, 100, 20), "connect failed");
        }
    }

    void OnDestroy()
    {
        EraseEvents();
    }
    void FixedUpdate()
    {
        Global.PhotonService.Service();
    }

    void OnApplicationQuit()
    {
        Global.PhotonService.Disconnect();
    }

    public void EraseEvents()
    {
        Global.SystemManagers.SystemInformManager.EraseConnectChangeFunction(OnConnectChange);
    }

    public void RegisterEvents()
    {
        Global.SystemManagers.SystemInformManager.RegisterConnectChangeFunction(OnConnectChange);
    }

    private void OnConnectChange(bool connected)
    {
        if(connected)
        {
            Global.SystemManagers.DebugInformManager.DebugInform("Connected");
            Global.OperationManagers.OperationManager.FetchDataOperationManager.FetchSystemVersion();
        }
        else
        {
            Global.SystemManagers.DebugInformManager.DebugInform("Disconnected");
        }
    }
}
