using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.ResponseParameters.Answer;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers.Answer
{
    internal class SoulOperationResponseResolver : AnswerResponseHandler
    {
        internal SoulOperationResponseResolver(General.Answer answer) : base(answer)
        {
        }

        internal override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            if (returnCode == ErrorCode.NoError)
            {
                if (parameters.Count != 5)
                {
                    LibraryInstance.ErrorFormat("Soul OperationResponse Parameter Error Parameter Count: {0}", parameters.Count);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                LibraryInstance.ErrorFormat("SoulOperationResponse Error ErrorCode: {0}, DebugMessage: {1}", returnCode, debugMessage);
                return false;
            }
        }

        internal override bool Handle(AnswerOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, returnCode, debugMessage, parameters))
            {
                try
                {
                    int soulID = (int)parameters[(byte)SoulResponseParameterCode.SoulID];
                    SoulOperationCode resolvedOperationCode = (SoulOperationCode)parameters[(byte)SoulResponseParameterCode.OperationCode];
                    ErrorCode resolvedReturnCode = (ErrorCode)parameters[(byte)SoulResponseParameterCode.ReturnCode];
                    string resolvedDebugMessage = (string)parameters[(byte)SoulResponseParameterCode.DebugMessage];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)SoulResponseParameterCode.Parameters];
                    if (answer.ContainsSoul(soulID))
                    {
                        answer.FindSoul(soulID).SoulResponseManager.Operate(resolvedOperationCode, resolvedReturnCode, resolvedDebugMessage, resolvedParameters);
                        return true;
                    }
                    else
                    {
                        LibraryInstance.ErrorFormat("SoulOperationResponse Error Soul ID: {0} Not in Answer ID: {1}", soulID, answer.AnswerID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("SoulOperationResponse Parameter Cast Error");
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
