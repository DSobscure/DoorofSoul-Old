using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Answer;
using DoorofSoul.Protocol.Communication.ResponseParameters.Answer;
using DoorofSoul.Protocol.Language;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Answer
{
    internal class ActivateSoulHandler : AnswerOperationHandler
    {
        internal ActivateSoulHandler(ThroneComponents.Answer answer) : base(answer, 1)
        {
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
                        ThroneComponents.Soul soul = answer.FindSoul(soulID);
                        NatureComponents.Container defaultContainer = soul.Containers.FirstOrDefault();
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
