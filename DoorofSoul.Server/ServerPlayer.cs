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
        }
        public void RelifeWithNewPlayer(ServerPlayer newPlayer)
        {
            peer = newPlayer.peer;
            peer.RelifeWithOldPlayer(this);
            LastConnectedIPAddress = peer.RemoteIPAddress;
        }

        public override void SendEvent(PlayerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            EventData eventData = new EventData
            {
                Code = (byte)eventCode,
                Parameters = parameters
            };
            peer.SendEvent(eventData, new SendParameters());
        }
        public override void SendResponse(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationResponse response = new OperationResponse((byte)operationCode, parameters)
            {
                ReturnCode = (short)ErrorCode.NoError
            };
            peer.SendOperationResponse(response, new SendParameters());
        }
        public override void SendError(PlayerOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            OperationResponse response = new OperationResponse((byte)operationCode, parameters)
            {
                ReturnCode = (short)errorCode,
                DebugMessage = debugMessage
            };
            peer.SendOperationResponse(response, new SendParameters());
        }

        public override void SendWorldEvent(WorldEventCode eventCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendWorldResponse(WorldOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendWorldError(WorldOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override bool Login(string account, string password, out string debugMessage, out string errorMessage)
        {
            throw new NotImplementedException();
        }

        public override void Logout()
        {
            throw new NotImplementedException();
        }

        public override void FetchSystemVersion(out string serverVersion, out string clientVersion)
        {
            throw new NotImplementedException();
        }

        public override void FetchAnswer(out Answer answer)
        {
            throw new NotImplementedException();
        }

        public override void FetchScene(int sceneID, out Scene scene)
        {
            throw new NotImplementedException();
        }
    }
}
