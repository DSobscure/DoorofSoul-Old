using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Answer;
using DoorofSoul.Protocol.Language;

namespace DoorofSoul.Library.General.Operations.Handlers.Answer
{
    public class DeleteSoulHandler : AnswerOperationHandler
    {
        public DeleteSoulHandler(General.Answer answer) : base(answer)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 1)
            {
                debugMessage = string.Format("Delete Soul Operation Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(AnswerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                string debugMessage, errorMessage;
                int soulID = (int)parameters[(byte)DeleteSoulOperationParameterCode.SoulID];
                if (answer.ContainsSoul(soulID))
                {
                    if (answer.DeleteSoul(soulID))
                    {
                        Dictionary<byte, object> responseParameters = new Dictionary<byte, object>();
                        SendResponse(operationCode, responseParameters);
                        return true;
                    }
                    else
                    {
                        debugMessage = string.Format("Soul Delete Error SoulID: {0}, AnswerID: {1}", soulID, answer.AnswerID);
                        errorMessage = LauguageDictionarySelector.Instance[answer.Player.UsingLanguage]["Delete Soul Error"];
                        SendError(operationCode, Protocol.Communication.ErrorCode.Fail, debugMessage, errorMessage);
                        return false;
                    }
                }
                else
                {
                    debugMessage = string.Format("Soul Delete Permission Deny SoulID: {0}, AnswerID: {1}", soulID, answer.AnswerID);
                    errorMessage = LauguageDictionarySelector.Instance[answer.Player.UsingLanguage]["Permission Deny"];
                    SendError(operationCode, Protocol.Communication.ErrorCode.PermissionDeny, debugMessage, errorMessage);
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
