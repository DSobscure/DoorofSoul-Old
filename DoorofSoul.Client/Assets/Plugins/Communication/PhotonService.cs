using DoorofSoul.Client.Communication.Events;
using DoorofSoul.Client.Communication.Responses;
using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DoorofSoul.Client.Communication
{
    public class PhotonService : IPhotonPeerListener
    {
        private PhotonPeer peer;
        private bool serverConnected;
        protected EventResolver eventResolver;
        protected ResponseResolver responseResolver;

        #region Connect Change
        private event Action<bool> onConnectChange;
        public event Action<bool> OnConnectChange
        {
            add { onConnectChange += value; }
            remove { onConnectChange -= value; }
        }
        #endregion

        public bool ServerConnected
        {
            get { return serverConnected; }
            private set
            {
                serverConnected = value;
                if(onConnectChange != null)
                {
                    onConnectChange(serverConnected);
                }
                else
                {
                    DebugReturn(DebugLevel.ERROR, "onConnectChange event is null");
                }
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

            eventResolver = new EventResolver(this);
            responseResolver = new ResponseResolver(this);

            RegisterTypes();
        }

        public void DebugReturn(DebugLevel level, string message)
        {
            Debug.Log(level.ToString() + " : " + message);
        }

        public void OnEvent(EventData eventData)
        {
            eventResolver.Operate(eventData);
        }

        public void OnOperationResponse(OperationResponse operationResponse)
        {
            responseResolver.Operate(operationResponse);
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

        public void SendPlayerOperation(int playerID, PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (peer.IsEncryptionAvailable)
            {
                Dictionary<byte, object> operationParameter = new Dictionary<byte, object>
                {
                    { (byte)OperationParameterCode.ID, playerID },
                    { (byte)OperationParameterCode.OperationCode, (byte)operationCode },
                    { (byte)OperationParameterCode.Parameters, parameters }
                };
                peer.OpCustom((byte)OperationCode.PlayerOperation, operationParameter, true, 0, true);
            }
            else
            {
                DebugReturn(DebugLevel.WARNING, "Communication Still Not Establish Encryption");
            }
        }

        public void SendWorldOperation(int worldID, WorldOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (peer.IsEncryptionAvailable)
            {
                Dictionary<byte, object> operationParameter = new Dictionary<byte, object>
                {
                    { (byte)OperationParameterCode.ID, worldID },
                    { (byte)OperationParameterCode.OperationCode, (byte)operationCode },
                    { (byte)OperationParameterCode.Parameters, parameters }
                };
                peer.OpCustom((byte)OperationCode.WorldOperation, operationParameter, true, 0, true);
            }
            else
            {
                DebugReturn(DebugLevel.WARNING, "Communication Still Not Establish Encryption");
            }
        }

        private void RegisterTypes()
        {
            PhotonPeer.RegisterType(typeof(Item), (byte)SerializationClassTypeCode.Item, Item.Serialize, Item.Deserialize);
            PhotonPeer.RegisterType(typeof(EntitySpaceProperties), (byte)SerializationClassTypeCode.EntitySpaceProperties, EntitySpaceProperties.Serialize, EntitySpaceProperties.Deserialize);
        }
    }
}

