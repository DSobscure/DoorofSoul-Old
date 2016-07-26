using DoorofSoul.Database.Library;
using DoorofSoul.Library;
using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.ResponseParameters;
using Photon.SocketServer;
using System;
using System.Collections.Generic;

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
        public void LoadPlayer(DatabasePlayer player)
        {
            PlayerID = player.PlayerID;
            Account = player.Account;
            Nickname = player.Nickname;
            UsingLanguage = player.UsingLanguage;
            LastConnectedIPAddress = player.LastConnectedIPAddress;
            AnswerID = player.AnswerID;
        }
        public void RelifeWithNewPlayer(ServerPlayer newPlayer)
        {
            peer = newPlayer.peer;
            peer.RelifeWithOldPlayer(this);
            LastConnectedIPAddress = peer.RemoteIPAddress;
        }

        public override void SendEvent(PlayerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> eventParameters = new Dictionary<byte, object>
            {
                { (byte)EventParameterCode.EventCode, (byte)eventCode },
                { (byte)EventParameterCode.ID, PlayerID },
                { (byte)EventParameterCode.Parameters, parameters }
            };
            EventData eventData = new EventData
            {
                Code = (byte)EventCode.PlayerEvent,
                Parameters = eventParameters
            };
            peer.SendEvent(eventData, new SendParameters());
        }
        public override void SendResponse(PlayerOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> responseParameters = new Dictionary<byte, object>
            {
                { (byte)ResponseParameterCode.OperationCode, (byte)operationCode },
                { (byte)ResponseParameterCode.ID, PlayerID },
                { (byte)ResponseParameterCode.ReturnCode, (short)errorCode },
                { (byte)ResponseParameterCode.DebugMessage, debugMessage },
                { (byte)ResponseParameterCode.Parameters, parameters }
            };
            OperationResponse response = new OperationResponse((byte)OperationCode.PlayerOperation, responseParameters)
            {
                ReturnCode = (short)ErrorCode.NoError
            };
            peer.SendOperationResponse(response, new SendParameters());
        }

        public override void SendWorldEvent(int worldID, WorldEventCode eventCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> eventParameters = new Dictionary<byte, object>
            {
                { (byte)EventParameterCode.EventCode, (byte)eventCode },
                { (byte)EventParameterCode.ID, worldID },
                { (byte)EventParameterCode.Parameters, parameters }
            };
            EventData eventData = new EventData
            {
                Code = (byte)EventCode.WorldEvent,
                Parameters = eventParameters
            };
            peer.SendEvent(eventData, new SendParameters());
        }

        public override void SendWorldResponse(int worldID, WorldOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> responseParameters = new Dictionary<byte, object>
            {
                { (byte)ResponseParameterCode.OperationCode, (byte)operationCode },
                { (byte)ResponseParameterCode.ID, worldID },
                { (byte)ResponseParameterCode.ReturnCode, (short)errorCode },
                { (byte)ResponseParameterCode.DebugMessage, debugMessage },
                { (byte)ResponseParameterCode.Parameters, parameters }
            };
            OperationResponse response = new OperationResponse((byte)OperationCode.WorldOperation, responseParameters)
            {
                ReturnCode = (short)ErrorCode.NoError
            };
            peer.SendOperationResponse(response, new SendParameters());
        }

        public override bool Login(string account, string password, out string debugMessage, out string errorMessage)
        {
            return Application.ServerInstance.PlayerFactory.PlayerLogin(this, account, password, out debugMessage, out errorMessage);
        }

        public override void Logout()
        {
            Application.ServerInstance.PlayerFactory.PlayerLogout(this);
        }

        public override void FetchSystemVersion(out string serverVersion, out string clientVersion)
        {
            serverVersion = Application.ServerInstance.SystemConfiguration.ServerVersion;
            clientVersion = Application.ServerInstance.SystemConfiguration.ClientVersion;
        }

        public override void FetchAnswer(out Answer answer)
        {
            answer = Answer;
        }

        public override void FetchScene(int sceneID, out Scene scene)
        {
            scene = Hexagram.Instance.Nature.FindScene(sceneID);
        }

        public override void SendOperation(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }


        public override void SendWorldOperation(int worldID, WorldOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
