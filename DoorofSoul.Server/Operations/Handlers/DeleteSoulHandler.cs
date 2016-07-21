using Photon.SocketServer;
using System;
using System.Collections.Generic;
using DoorofSoul.Protocol.Communication.OperationParameters;
using DoorofSoul.Library;

namespace DoorofSoul.Server.Operations.Handlers
{
    public class DeleteSoulHandler : OperationHandler
    {
        public DeleteSoulHandler(Peer peer) : base(peer)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 1)
            {
                debugMessage = "Delete Soul Operation Parameter Error";
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
                int soulID = (int)operationRequest.Parameters[(byte)DeleteSoulOperationParameterCode.SoulID];
                if (peer.Player.Answer.ContainsSoul(soulID))
                {
                    if (Hexagram.Instance.Throne.DeleteSoul(peer.Player.Answer.AnswerID, soulID))
                    {
                        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
                        SendResponse(operationRequest.OperationCode, parameters);
                        return true;
                    }
                    else
                    {
                        debugMessage = string.Format("Soul Delete Error SoulID: {0}, AnswerID: {1}", soulID, peer.Player.Answer.AnswerID);
                        errorMessage = LauguageDictionarySelector.Instance[peer.UsingLanguage]["Delete Soul Error"];
                        SendError(operationRequest.OperationCode, Protocol.Communication.ErrorCode.Fail, debugMessage, errorMessage);
                        return false;
                    }
                }
                else
                {
                    debugMessage = string.Format("Soul Delete Permission Deny SoulID: {0}, AnswerID: {1}", soulID, peer.Player.Answer.AnswerID);
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
