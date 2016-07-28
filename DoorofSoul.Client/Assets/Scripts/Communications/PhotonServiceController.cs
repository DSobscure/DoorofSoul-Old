using UnityEngine;
using DoorofSoul.Client.Interfaces;
using DoorofSoul.Client.Global;
using DoorofSoul.Client.Communication;
using ExitGames.Client.Photon;
using DoorofSoul.Client.Library.General;

public class PhotonServiceController : MonoBehaviour, IEventProvider
{
    private PhotonService photonService;
    private ClientPlayer player;

    void Awake()
    {
        photonService = Global.PhotonService;
        player = Global.Player;
        RegisterEvents();
    }
    void Start()
    {
        if(photonService.ServerConnected)
        {
            photonService.DebugReturn(DebugLevel.WARNING, "Already Connected");
        }
        else
        {
            photonService.Connect();
            photonService.DebugReturn(DebugLevel.INFO, "Start Connecting");
        }
    }

    void OnGUI()
    {
        if(photonService.ServerConnected)
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
        photonService.Service();
    }

    void OnApplicationQuit()
    {
        photonService.Disconnect();
    }

    public void EraseEvents()
    {
        photonService.OnConnectChange -= OnConnectChange;
    }

    public void RegisterEvents()
    {
        photonService.OnConnectChange += OnConnectChange;
    }

    private void OnConnectChange(bool connected)
    {
        if(connected)
        {
            SystemManager.Error("Connected");
            string systemVersion, clientVersion;
            player.FetchSystemVersion(out systemVersion, out clientVersion);
        }
        else
        {
            SystemManager.Error("Disconnected");
        }
    }
}
