using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.ResponseParameters.Answer;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers.Answer
{
    internal class ContainerOperationResponsResolver : AnswerResponseHandler
    {
        internal ContainerOperationResponsResolver(General.Answer answer) : base(answer)
        {
        }

        internal override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            if(returnCode == ErrorCode.NoError)
            {
                if (parameters.Count != 5)
                {
                    LibraryInstance.ErrorFormat("Container OperationResponse Parameter Error Parameter Count: {0}", parameters.Count);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                LibraryInstance.ErrorFormat("ContainerOperationResponse Error ErrorCode: {0}, DebugMessage: {1}", returnCode, debugMessage);
                return false;
            }
        }

        internal override bool Handle(AnswerOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, returnCode, debugMessage, parameters))
            {
                try
                {
                    int containerID = (int)parameters[(byte)ContainerResponseParameterCode.ContainerID];
                    ContainerOperationCode resolvedOperationCode = (ContainerOperationCode)parameters[(byte)ContainerResponseParameterCode.OperationCode];
                    ErrorCode resolvedReturnCode = (ErrorCode)parameters[(byte)ContainerResponseParameterCode.ReturnCode];
                    string resolvedDebugMessage = (string)parameters[(byte)ContainerResponseParameterCode.DebugMessage];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)ContainerResponseParameterCode.Parameters];
                    if (answer.ContainsContainer(containerID))
                    {
                        answer.FindContainer(containerID).ContainerResponseManager.Operate(resolvedOperationCode, resolvedReturnCode, resolvedDebugMessage, resolvedParameters);
                        return true;
                    }
                    else
                    {
                        LibraryInstance.ErrorFormat("ContainerOperationResponse Error Container ID: {0} Not in Answer ID: {1}", containerID, answer.AnswerID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("ContainerOperationResponse Parameter Cast Error");
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
