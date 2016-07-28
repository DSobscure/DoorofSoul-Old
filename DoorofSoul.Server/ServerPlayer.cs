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
using DoorofSoul.Protocol.Language;

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
        public void LoadPlayer(PlayerData player)
        {
            PlayerID = player.playerID;
            Account = player.account;
            Nickname = player.nickname;
            UsingLanguage = player.usingLanguage;
            LastConnectedIPAddress = player.lastConnectedIPAddress;
            AnswerID = player.answerID;
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
                { (byte)EventParameterCode.ID, PlayerID },
                { (byte)EventParameterCode.EventCode, (byte)eventCode },
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
                { (byte)ResponseParameterCode.ID, PlayerID },
                { (byte)ResponseParameterCode.OperationCode, (byte)operationCode },
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
                { (byte)EventParameterCode.ID, worldID },
                { (byte)EventParameterCode.EventCode, (byte)eventCode },
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
                { (byte)ResponseParameterCode.ID, worldID },
                { (byte)ResponseParameterCode.OperationCode, (byte)operationCode },
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

        public override bool Login(string account, string password, out string debugMessage, out ErrorCode errorCode)
        {
            return Application.ServerInstance.PlayerFactory.PlayerLogin(this, account, password, out debugMessage, out errorCode);
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

        public override void SendOperation(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }


        public override void SendWorldOperation(int worldID, WorldOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void ErrorInform(string title, string message)
        {
            throw new NotImplementedException();
        }

        public override void LoginResponse(int playerID, string account, string nickname, SupportLauguages usingLanguage, int answerID)
        {
            throw new NotImplementedException();
        }

        public override void LoginFailed()
        {
            PlayerID = 0;
            Account = null;
            IsOnline = false;
            IsActivated = false;
            AnswerID = 0;
        }

        public override void LogoutResponse()
        {
            throw new NotImplementedException();
        }

        public override bool DeleteSoul(Answer answer, int soulID)
        {
            return Hexagram.Instance.Throne.DeleteSoul(answer.AnswerID, soulID);
        }

        public override bool CreateSoul(Answer answer, string soulName)
        {
            return Hexagram.Instance.Throne.CreateSoul(answer.AnswerID, soulName);
        }

        public override bool ActivateSoul(Answer answer, int soulID)
        {
            return Hexagram.Instance.Throne.ActiveSoul(soulID);
        }

        public override void FetchSystemVersionResponse(string serverVersion, string clientVersion)
        {
            throw new NotImplementedException();
        }

        public override void FetchWorlds(out List<World> worlds)
        {
            worlds = Hexagram.Instance.Nature.ListWorlds();
        }

        public override void FetchWorldsResponse(int worldID, string worldName)
        {
            throw new NotImplementedException();
        }
    }
}
