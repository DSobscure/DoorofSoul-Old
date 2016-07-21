using Photon.SocketServer;
using System;
using System.Collections.Generic;
using DoorofSoul.Protocol.Communication.OperationParameters;
using DoorofSoul.Library;

namespace DoorofSoul.Server.Operations.Handlers
{
    public class CreateSoulHandler : OperationHandler
    {
        public CreateSoulHandler(Peer peer) : base(peer)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 1)
            {
                debugMessage = "Create Soul Operation Parameter Error";
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
            if (base.Handle(operationRequest))
            {
                string debugMessage, errorMessage;
                string soulName = (string)operationRequest.Parameters[(byte)CreateSoulOperationParameterCode.SoulName];
                if (peer.Player.Answer.SoulCount < peer.Player.Answer.SoulCountLimit)
                {
                    if (Hexagram.Instance.Throne.CreateSoul(peer.Player.Answer.AnswerID, soulName))
                    {
                        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
                        SendResponse(operationRequest.OperationCode, parameters);
                        return true;
                    }
                    else
                    {
                        debugMessage = string.Format("Soul Create Error AnswerID: {0}", peer.Player.Answer.AnswerID);
                        errorMessage = LauguageDictionarySelector.Instance[peer.UsingLanguage]["Create Soul Error"];
                        SendError(operationRequest.OperationCode, Protocol.Communication.ErrorCode.Fail, debugMessage, errorMessage);
                        return false;
                    }
                }
                else
                {
                    debugMessage = string.Format("Soul Create Permission Deny AnswerID: {0}", peer.Player.Answer.AnswerID);
                    errorMessage = LauguageDictionarySelector.Instance[peer.UsingLanguage]["Permission Deny"];
                    SendError(operationRequest.OperationCode, Protocol.Communication.ErrorCode.PermissionDeny, debugMessage, errorMessage);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
