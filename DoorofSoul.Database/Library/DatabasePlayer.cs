using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System;
using System.Collections.Generic;
using DoorofSoul.Protocol.Language;
using System.Net;

namespace DoorofSoul.Database.Library
{
    public class DatabasePlayer : Player
    {
        public DatabasePlayer(int playerID, string account, string nickname, SupportLauguages usingLanguage, IPAddress lastConnectedIPAddress, int answerID) : base(playerID, account, nickname, usingLanguage, lastConnectedIPAddress, answerID)
        {

        }

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
            throw new NotImplementedException();
        }

        public override void Logout()
        {
            throw new NotImplementedException();
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

        public override void SendWorldError(WorldOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendWorldEvent(WorldEventCode eventCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendWorldResponse(WorldOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
