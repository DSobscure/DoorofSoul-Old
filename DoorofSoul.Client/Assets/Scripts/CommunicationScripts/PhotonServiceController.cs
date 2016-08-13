using UnityEngine;
using DoorofSoul.Client.Interfaces;
using DoorofSoul.Client.Global;
using DoorofSoul.Client.Communication;
using DoorofSoul.Library.General;

namespace DoorofSoul.Client.Scripts.CommunicationScripts
{
    public class PhotonServiceController : MonoBehaviour, IEventProvider
    {
        private PhotonService photonService;
        private Player player;

        void Awake()
        {
            photonService = Global.Global.PhotonService;
            player = Global.Global.Player;
            RegisterEvents();
        }
        void Start()
        {
            if (photonService.ServerConnected)
            {

            }
            else
            {
                photonService.Connect();
            }
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
