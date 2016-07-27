using System;
using System.Collections.Generic;
using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Player;
using DoorofSoul.Protocol.Language;

namespace DoorofSoul.Client.Library.General
{
    public class ClientPlayer : Player
    {
        public override bool ActiveSoul(Answer answer, int soulID)
        {
            throw new NotImplementedException();
        }

        public override bool CreateSoul(Answer answer, string soulName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteSoul(Answer answer, int soulID)
        {
            throw new NotImplementedException();
        }

        public override void ErrorInform(string title, string message)
        {
            throw new NotImplementedException();
        }

        public override void FetchAnswer(out Answer answer)
        {
            throw new NotImplementedException();
        }

        public override void FetchScene(int sceneID, out DoorofSoul.Library.General.Scene scene)
        {
            throw new NotImplementedException();
        }

        public override void FetchSceneResponse(int sceneID, string sceneName, int worldID)
        {
            throw new NotImplementedException();
        }

        public override void FetchSystemVersion(out string serverVersion, out string clientVersion)
        {
            throw new NotImplementedException();
        }

        public override void FetchSystemVersionResponse(string serverVersion, string clientVersion)
        {
            throw new NotImplementedException();
        }

        public override bool Login(string account, string password, out string debugMessage, out ErrorCode errorCode)
        {
            debugMessage = null;
            errorCode = ErrorCode.NoError;
            var parameters = new Dictionary<byte, object>
            {
                { (byte)LoginParameterCode.Account, account },
                { (byte)LoginParameterCode.Password, password }
            };
            SendOperation(PlayerOperationCode.Login, parameters);
            return false;
        }

        public override void LoginResponse(int playerID, string account, string nickname, SupportLauguages usingLanguage, int answerID)
        {
            throw new NotImplementedException();
        }

        public override void Logout()
        {
            var parameters = new Dictionary<byte, object>();
            Global.Global.PhotonService.SendPlayerOperation(PlayerOperationCode.Logout, PlayerID, parameters);
        }

        public override void LogoutResponse()
        {
            throw new NotImplementedException();
        }

        public override void SendEvent(PlayerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendOperation(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendResponse(PlayerOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendWorldEvent(int worldID, WorldEventCode eventCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendWorldOperation(int worldID, WorldOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendWorldResponse(int worldID, WorldOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
