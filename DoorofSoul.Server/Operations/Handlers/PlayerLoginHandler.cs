using System;
using System.Collections.Generic;
using Photon.SocketServer;
using DoorofSoul.Protocol.Communication.OperationParameters;
using DoorofSoul.Protocol.Communication.ResponseParameters;

namespace DoorofSoul.Server.Operations.Handlers
{
    public class PlayerLoginHandler : OperationHandler
    {
        public PlayerLoginHandler(Peer peer) : base(peer)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 2)
            {
                debugMessage = "Player Login Operation Parameter Error";
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(OperationRequest operationRequest)
        {
            if(base.Handle(operationRequest))
            {
                string debugMessage, errorMessage;
                string account = (string)operationRequest.Parameters[(byte)PlayerLoginOperationParameterCode.Account];
                string password = (string)operationRequest.Parameters[(byte)PlayerLoginOperationParameterCode.Password];
                bool result = Application.ServerInstance.PlayerLogin(peer.player, account, password, out debugMessage, out errorMessage);
                if(result)
                {
                    Dictionary<byte, object> parameters = new Dictionary<byte, object>
                    {
                        { (byte)PlayerLoginResponseParameterCode.PlayerID, peer.player.PlayerID },
                        { (byte)PlayerLoginResponseParameterCode.Account, peer.player.Account },
                        { (byte)PlayerLoginResponseParameterCode.Nickname, peer.player.Nickname },
                        { (byte)PlayerLoginResponseParameterCode.UsingLanguageCode, (byte)peer.player.UsingLanguage },
                        { (byte)PlayerLoginResponseParameterCode.AnswerID, peer.player.AnswerID }
                    };
                    SendResponse(operationRequest.OperationCode, parameters);
                }
                else
                {
                    SendError(operationRequest.OperationCode, Protocol.Communication.ErrorCode.PermissionDeny, debugMessage, errorMessage);
                }
                return result;
            }
            else
            {
                return false;
            }
        }
    }
}
