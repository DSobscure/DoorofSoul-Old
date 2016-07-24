using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication;
using Photon.SocketServer;
using System.Net;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.OperationCodes;

namespace DoorofSoul.Server
{
    public class ServerPlayer : Player
    {

        public Guid Guid
        {
            get { return peer.Guid; }
        }

        protected Peer peer;

        public ServerPlayer(Peer peer) : base()
        {
            this.peer = peer;
            LastConnectedIPAddress = peer.RemoteIPAddress;
            SendEvent = SendServerEvent;
            SendResponse = SendServerResponse;
            SendError = SendServerError;
        }
        public void RelifeWithNewPlayer(ServerPlayer newPlayer)
        {
            peer = newPlayer.peer;
            peer.RelifeWithOldPlayer(this);
            LastConnectedIPAddress = peer.RemoteIPAddress;
        }

        private void SendServerEvent(PlayerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            EventData eventData = new EventData
            {
                Code = (byte)eventCode,
                Parameters = parameters
            };
            peer.SendEvent(eventData, new SendParameters());
        }
        private void SendServerResponse(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationResponse response = new OperationResponse((byte)operationCode, parameters)
            {
                ReturnCode = (short)ErrorCode.NoError
            };
            peer.SendOperationResponse(response, new SendParameters());
        }
        private void SendServerError(PlayerOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            OperationResponse response = new OperationResponse((byte)operationCode, parameters)
            {
                ReturnCode = (short)errorCode,
                DebugMessage = debugMessage
            };
            peer.SendOperationResponse(response, new SendParameters());
        }
    }
}
