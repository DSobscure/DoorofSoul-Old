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
    public class ServerPlayerCommunicationInterface : PlayerCommunicationInterface
    {
        protected Peer peer;

        public ServerPlayerCommunicationInterface(Peer peer)
        {
            this.peer = peer;
        }

        public override bool ActiveSoul(Answer answer, int soulID)
        {
            return Hexagram.Instance.Throne.ActiveSoul(soulID);
        }

        public override bool CreateSoul(Answer answer, string soulName)
        {
            return Hexagram.Instance.Throne.CreateSoul(answer.AnswerID, soulName);
        }

        public override bool DeleteSoul(Answer answer, int soulID)
        {
            return Hexagram.Instance.Throne.DeleteSoul(answer.AnswerID, soulID);
        }

        public override void ErrorInform(string title, string message)
        {
            Application.Log.ErrorFormat("PlayerErrorInform PlayerID: {0} Title: {1}, Message: {2}", player.PlayerID, title, message);
        }

        public override Scene FindScene(int sceneID)
        {
            return Hexagram.Instance.Nature.FindScene(sceneID);
        }

        public override void GetSystemVersion(out string serverVersion, out string clientVersion)
        {
            serverVersion = Application.ServerInstance.SystemConfiguration.ServerVersion;
            clientVersion = Application.ServerInstance.SystemConfiguration.ClientVersion;
        }

        public override List<World> ListWorlds()
        {
            return Hexagram.Instance.Nature.ListWorlds();
        }

        public override void LoadScene(int sceneID, string sceneName, int worldID)
        {
            throw new NotImplementedException();
        }

        public override void LoadWorld(int worldID, string worldName)
        {
            throw new NotImplementedException();
        }

        public override bool Login(string account, string password, out string debugMessage, out ErrorCode errorCode)
        {
            return Application.ServerInstance.PlayerFactory.PlayerLogin(player as ServerPlayer, account, password, out debugMessage, out errorCode);
        }

        public override void Logout()
        {
            Application.ServerInstance.PlayerFactory.PlayerLogout(player as ServerPlayer);
        }

        public override void SendEvent(PlayerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> eventParameters = new Dictionary<byte, object>
            {
                { (byte)EventParameterCode.ID, player.PlayerID },
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

        public override void SendOperation(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendResponse(PlayerOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> responseParameters = new Dictionary<byte, object>
            {
                { (byte)ResponseParameterCode.ID, player.PlayerID },
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

        public override void SendWorldOperation(int worldID, WorldOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
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

        public override void UpdateSystemVersion(string serverVersion, string clientVersion)
        {
            throw new NotImplementedException();
        }
    }
}
