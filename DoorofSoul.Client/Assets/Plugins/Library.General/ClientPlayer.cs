using System;
using System.Collections.Generic;
using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Player;

namespace Assets.Plugins.Library.General
{
    public class ClientPlayer : Player
    {
        public override void FetchAnswer(out Answer answer)
        {
            throw new NotImplementedException();
        }

        public override void FetchScene(int sceneID, out Scene scene)
        {
            throw new NotImplementedException();
        }

        public override void FetchSystemVersion(out string serverVersion, out string clientVersion)
        {
            throw new NotImplementedException();
        }

        public override bool Login(string account, string password, out string debugMessage, out string errorMessage)
        {
            debugMessage = null;
            errorMessage = null;
            var parameters = new Dictionary<byte, object>
            {
                { (byte)LoginParameterCode.Account, account },
                { (byte)LoginParameterCode.Password, password }
            };
            Global.PhotonService.SendPlayerOperation(PlayerOperationCode.Login, PlayerID, parameters);
            return false;
        }

        public override void Logout()
        {
            var parameters = new Dictionary<byte, object>();
            Global.PhotonService.SendPlayerOperation(PlayerOperationCode.Logout, PlayerID, parameters);
        }

        public override void SendError(PlayerOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendEvent(PlayerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendResponse(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendWorldError(int worldID, WorldOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendWorldEvent(int worldID, WorldEventCode eventCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendWorldResponse(int worldID, WorldOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
