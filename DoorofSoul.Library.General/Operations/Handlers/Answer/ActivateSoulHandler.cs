using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Answer;
using DoorofSoul.Protocol.Communication.ResponseParameters.Answer;
using DoorofSoul.Protocol.Language;

namespace DoorofSoul.Library.General.Operations.Handlers.Answer
{
    internal class ActivateSoulHandler : AnswerOperationHandler
    {
        internal ActivateSoulHandler(General.Answer answer) : base(answer)
        {
        }

        internal override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 1)
            {
                debugMessage = string.Format("Activate Soul Operation Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        internal override bool Handle(AnswerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                string debugMessage;
                int soulID = (int)parameters[(byte)ActivateSoulOperationParameterCode.SoulID];
                if (answer.ContainsSoul(soulID))
                {
                    if (answer.Player.PlayerCommunicationInterface.ActiveSoul(answer, soulID))
                    {
                        General.Soul soul = answer.FindSoul(soulID);
                        General.Container defaultContainer = soul.Containers.FirstOrDefault();
                        Dictionary<byte, object> responseParameters = new Dictionary<byte, object>
                        {
                            { (byte)ActiveSoulResponseParameterCode.SoulID, soulID }
                        };
                        SendResponse(operationCode, responseParameters);
                        return true;
                    }
                    else
                    {
                        debugMessage = string.Format("Soul Activate Error SoulID: {0}, AnswerID: {1}", soulID, answer.AnswerID);
                        SendError(operationCode, Protocol.Communication.ErrorCode.Fail, debugMessage);
                        return false;
                    }
                }
                else
                {
                    debugMessage = string.Format("Soul Activate Permission Deny SoulID: {0}, AnswerID: {1}", soulID, answer.AnswerID);
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
