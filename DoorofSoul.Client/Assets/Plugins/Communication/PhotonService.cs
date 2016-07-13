using DoorofSoul.Protocol.Communication;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Client.Communication
{
    public class PhotonService : IPhotonPeerListener
    {
        private PhotonPeer peer;
        private bool serverConnected;
        public bool ServerConnected
        {
            get { return serverConnected; }
            private set
            {
                serverConnected = value;
                Global.SystemManagers.SystemInformManager.ConnectChange(serverConnected);
            }
        }
        private string serverName;
        private string serverAddress;
        private int udpPort;

        public PhotonService(string serverName, string serverAddress, int udpPort)
        {
            this.serverName = serverName;
            this.serverAddress = serverAddress;
            this.udpPort = udpPort;
        }

        public void DebugReturn(DebugLevel level, string message)
        {
            Global.SystemManagers.DebugInformManager.DebugInform(level.ToString() + " : " + message);
        }

        public void OnEvent(EventData eventData)
        {
            Global.EventManagers.EventManager.Operate(eventData);
        }

        public void OnOperationResponse(OperationResponse operationResponse)
        {
            Global.ResponseManagers.ResponseManager.Operate(operationResponse);
        }

        public void OnStatusChanged(StatusCode statusCode)
        {
            switch (statusCode)
            {
                case StatusCode.Connect:
                    DebugReturn(DebugLevel.INFO, "Establishing Encryption");
                    peer.EstablishEncryption();
                    break;
                case StatusCode.Disconnect:
                    ServerConnected = false;
                    break;
                case StatusCode.EncryptionEstablished:
                    ServerConnected = true;
                    break;
            }
        }

        public void Connect()
        {
            try
            {
                peer = new PhotonPeer(this, ConnectionProtocol.Udp);
                if (!peer.Connect(serverAddress + ":" + udpPort.ToString(), serverName))
                {
                    DebugReturn(DebugLevel.ERROR, "Connect Fail");
                    ServerConnected = false;
                }
                else
                {
                    DebugReturn(DebugLevel.INFO, peer.PeerState.ToString());
                }
            }
            catch (Exception ex)
            {
                ServerConnected = false;
                DebugReturn(DebugLevel.ERROR, ex.Message);
                DebugReturn(DebugLevel.ERROR, ex.StackTrace);
            }
        }

        public void Disconnect()
        {
            try
            {
                peer.Disconnect();
            }
            catch (Exception ex)
            {
                DebugReturn(DebugLevel.ERROR, ex.Message);
                DebugReturn(DebugLevel.ERROR, ex.StackTrace);
            }
        }

        public void Service()
        {
            try
            {
                peer.Service();
            }
            catch (Exception ex)
            {
                DebugReturn(DebugLevel.ERROR, ex.Message);
                DebugReturn(DebugLevel.ERROR, ex.StackTrace);
            }
        }

        public void SendOperation(OperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (peer.IsEncryptionAvailable)
            {
                peer.OpCustom((byte)operationCode, parameters, true, 0, true);
            }
            else
            {
                DebugReturn(DebugLevel.WARNING, "Communication Still Not Establish Encryption");
            }
        }
    }
}

