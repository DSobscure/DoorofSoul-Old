using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Answer;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers.Answer
{
    internal class SoulOperationResolver : AnswerOperationHandler
    {
        internal SoulOperationResolver(General.Answer answer) : base(answer)
        {
        }

        internal override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 3)
            {
                debugMessage = string.Format("Soul Operation Parameter Error Parameter Count: {0}", parameter.Count);
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
                try
                {
                    int soulID = (int)parameters[(byte)SoulOperationParameterCode.SoulID];
                    SoulOperationCode resolvedOperationCode = (SoulOperationCode)parameters[(byte)SoulOperationParameterCode.OperationCode];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)SoulOperationParameterCode.Parameters];
                    if (answer.ContainsSoul(soulID))
                    {
                        answer.FindSoul(soulID).SoulOperationManager.Operate(resolvedOperationCode, resolvedParameters);
                        return true;
                    }
                    else
                    {
                        LibraryLog.ErrorFormat("SoulOperation Error Soul ID: {0} Not in Answer ID: {1}", soulID, answer.AnswerID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.ErrorFormat("SoulOperation Parameter Cast Error");
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
