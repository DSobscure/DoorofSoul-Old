using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Answer;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Answer
{
    internal class ContainerOperationResolver : AnswerOperationHandler
    {
        internal ContainerOperationResolver(ThroneComponents.Answer answer) : base(answer, 3)
        {
        }

        internal override bool Handle(AnswerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if(base.Handle(operationCode, parameters))
            {
                try
                {
                    int containerID = (int)parameters[(byte)ContainerOperationParameterCode.ContainerID];
                    ContainerOperationCode resolvedOperationCode = (ContainerOperationCode)parameters[(byte)ContainerOperationParameterCode.OperationCode];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)ContainerOperationParameterCode.Parameters];
                    if(answer.ContainsContainer(containerID))
                    {
                        answer.FindContainer(containerID).ContainerOperationManager.Operate(resolvedOperationCode, resolvedParameters);
                        return true;
                    }
                    else
                    {
                        LibraryInstance.ErrorFormat("ContainerOperation Error Container ID: {0} Not in Answer ID: {1}", containerID, answer.AnswerID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("ContainerOperation Parameter Cast Error");
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
