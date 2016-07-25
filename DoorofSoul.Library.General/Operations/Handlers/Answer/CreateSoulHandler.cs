﻿using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Answer;
using DoorofSoul.Protocol.Language;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers.Answer
{
    public class CreateSoulHandler : AnswerOperationHandler
    {
        public CreateSoulHandler(General.Answer answer) : base(answer)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 1)
            {
                debugMessage = string.Format("Create Soul Operation Parameter Error Parameter Count: {0}", parameter.Count);
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
                string soulName = (string)parameters[(byte)CreateSoulOperationParameterCode.SoulName];
                if (answer.SoulCount < answer.SoulCountLimit)
                {
                    if (answer.CreateSoul(soulName))
                    {
                        Dictionary<byte, object> responseParameters = new Dictionary<byte, object>();
                        SendResponse(operationCode, responseParameters);
                        return true;
                    }
                    else
                    {
                        debugMessage = string.Format("Soul Create Error AnswerID: {0}", answer.AnswerID);
                        errorMessage = LauguageDictionarySelector.Instance[answer.Player.UsingLanguage]["Create Soul Error"];
                        SendError(operationCode, Protocol.Communication.ErrorCode.Fail, debugMessage, errorMessage);
                        return false;
                    }
                }
                else
                {
                    debugMessage = string.Format("Soul Create Permission Deny AnswerID: {0}", answer.AnswerID);
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