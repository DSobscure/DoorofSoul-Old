﻿using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters.Answer;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Answer
{
    internal class ContainerEventResolver : AnswerEventHandler
    {
        internal ContainerEventResolver(General.Answer answer) : base(answer)
        {
        }

        internal override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 3)
            {
                debugMessage = string.Format("Container Event Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        internal override bool Handle(AnswerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(eventCode, parameters))
            {
                try
                {
                    int containerID = (int)parameters[(byte)ContainerEventParameterCode.ContainerID];
                    ContainerEventCode resolvedEventCode = (ContainerEventCode)parameters[(byte)ContainerEventParameterCode.EventCode];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)ContainerEventParameterCode.Parameters];
                    if (answer.ContainsContainer(containerID))
                    {
                        answer.FindContainer(containerID).ContainerEventManager.Operate(resolvedEventCode, resolvedParameters);
                        return true;
                    }
                    else
                    {
                        LibraryInstance.ErrorFormat("ContainerEvent Error Container ID: {0} Not in Answer ID: {1}", containerID, answer.AnswerID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("ContainerEvent Parameter Cast Error");
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
