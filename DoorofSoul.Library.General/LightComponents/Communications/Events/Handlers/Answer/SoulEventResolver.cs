using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters.Answer;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Answer
{
    internal class SoulEventResolver : AnswerEventHandler
    {
        internal SoulEventResolver(ThroneComponents.Answer answer) : base(answer, 3)
        {
        }

        internal override bool Handle(AnswerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(eventCode, parameters))
            {
                try
                {
                    int soulID = (int)parameters[(byte)SoulEventParameterCode.SoulID];
                    SoulEventCode resolvedEventCode = (SoulEventCode)parameters[(byte)SoulEventParameterCode.EventCode];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)SoulEventParameterCode.Parameters];
                    if (answer.ContainsSoul(soulID))
                    {
                        answer.FindSoul(soulID).SoulEventManager.Operate(resolvedEventCode, resolvedParameters);
                        return true;
                    }
                    else
                    {
                        LibraryInstance.ErrorFormat("SoulEvent Error Soul ID: {0} Not in Answer ID: {1}", soulID, answer.AnswerID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("SoulEvent Parameter Cast Error");
                    LibraryInstance.ErrorFormat(ex.Message);
                    LibraryInstance.ErrorFormat(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    LibraryInstance.ErrorFormat(ex.Message);
                    LibraryInstance.ErrorFormat(ex.StackTrace);
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
