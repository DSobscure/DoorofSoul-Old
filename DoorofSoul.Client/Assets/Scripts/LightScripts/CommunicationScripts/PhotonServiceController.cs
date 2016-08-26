using UnityEngine;
using DoorofSoul.Client.Interfaces;
using DoorofSoul.Client.Global;
using DoorofSoul.Client.Communication;
using DoorofSoul.Library.General;

namespace DoorofSoul.Client.Scripts.LightScripts.CommunicationScripts
{
    public class PhotonServiceController : MonoBehaviour, IEventProvider
    {
        private PhotonService photonService;
        private DoorofSoul.Library.General.Player player;

        void Awake()
        {
            photonService = Global.Global.PhotonService;
            player = Global.Global.Player;
            RegisterEvents();
        }
        void OnGUI()
        {
            if (photonService.ServerConnected)
            {
                GUI.Label(new Rect(20, 10, 100, 20), "connected");
            }
            else
            {
                GUI.Label(new Rect(20, 10, 100, 20), "connect failed");
                if (GUI.Button(new Rect(Screen.width/2-200, Screen.height/2-150, 400, 100), "連接至開發伺服器"))
                {
                    photonService.Connect(Global.Global.ServerName, Global.Global.ServerAddress, Global.Global.ServerPort);
                }
                if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 , 400, 100), "連接至Alpha伺服器"))
                {
                    photonService.Connect(Global.Global.AlphaServerName, Global.Global.AlphaServerAddress, Global.Global.AlphaServerPort);
                }
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
            if (connected)
            {
                SystemManager.Error("Connected");
                player.PlayerOperationManager.FetchSystemVersion();
            }
            else
            {
                SystemManager.Error("Disconnected");
            }
        }
    }
}
