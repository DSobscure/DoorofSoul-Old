using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.ResponseParameters.Answer;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers.Answer
{
    public class SoulOperationResponseResolver : AnswerResponseHandler
    {
        public SoulOperationResponseResolver(General.Answer answer) : base(answer)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 5)
            {
                debugMessage = string.Format("Soul OperationResponse Parameter Error Parameter Count: {0}", parameter.Count);
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
                try
                {
                    int soulID = (int)parameters[(byte)SoulResponseParameterCode.SoulID];
                    SoulOperationCode resolvedOperationCode = (SoulOperationCode)parameters[(byte)SoulResponseParameterCode.OperationCode];
                    ErrorCode returnCode = (ErrorCode)parameters[(byte)SoulResponseParameterCode.ReturnCode];
                    string debugMessage = (string)parameters[(byte)SoulResponseParameterCode.DebugMessage];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)SoulResponseParameterCode.Parameters];
                    if (answer.ContainsSoul(soulID))
                    {
                        if(returnCode == ErrorCode.NoError)
                        {
                            answer.FindSoul(soulID).SoulResponseManager.Operate(resolvedOperationCode, resolvedParameters);
                            return true;
                        }
                        else
                        {
                            LibraryLog.ErrorFormat("SoulOperationResponse Error Soul ID: {0} ErrorCode: {1}, DebugMessage: {2}", soulID, returnCode, debugMessage);
                            return false;
                        }
                    }
                    else
                    {
                        LibraryLog.ErrorFormat("SoulOperationResponse Error Soul ID: {0} Not in Answer ID: {1}", soulID, answer.AnswerID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.ErrorFormat("SoulOperationResponse Parameter Cast Error");
                    LibraryLog.ErrorFormat(ex.Message);
                    LibraryLog.ErrorFormat(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    LibraryLog.ErrorFormat(ex.Message);
                    LibraryLog.ErrorFormat(ex.StackTrace);
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
