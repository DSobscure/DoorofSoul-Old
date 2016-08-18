using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Answer;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Answer
{
    internal class CreateSoulHandler : AnswerOperationHandler
    {
        internal CreateSoulHandler(ThroneComponents.Answer answer) : base(answer, 2)
        {
        }

        internal override bool Handle(AnswerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                string debugMessage;
                string soulName = (string)parameters[(byte)CreateSoulParameterCode.SoulName];
                SoulKernelTypeCode mainSoulType = (SoulKernelTypeCode)parameters[(byte)CreateSoulParameterCode.MainSoulType];
                if (answer.SoulCount < answer.SoulCountLimit)
                {
                    if (answer.Player.PlayerCommunicationInterface.CreateSoul(answer, soulName, mainSoulType))
                    {
                        Dictionary<byte, object> responseParameters = new Dictionary<byte, object>();
                        SendResponse(operationCode, responseParameters);
                        return true;
                    }
                    else
                    {
                        debugMessage = string.Format("Soul Create Error AnswerID: {0}", answer.AnswerID);
                        SendError(operationCode, Protocol.Communication.ErrorCode.Fail, debugMessage);
                        return false;
                    }
                }
                else
                {
                    debugMessage = string.Format("Soul Create Permission Deny AnswerID: {0}", answer.AnswerID);
                    SendError(operationCode, Protocol.Communication.ErrorCode.PermissionDeny, debugMessage);
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
