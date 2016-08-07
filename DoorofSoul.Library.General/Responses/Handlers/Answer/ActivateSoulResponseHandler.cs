using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.ResponseParameters.Answer;
using DoorofSoul.Protocol.Language;

namespace DoorofSoul.Library.General.Responses.Handlers.Answer
{
    internal class ActivateSoulResponseHandler : AnswerResponseHandler
    {
        internal ActivateSoulResponseHandler(General.Answer answer) : base(answer)
        {
        }

        internal override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            switch(returnCode)
            {
                case ErrorCode.NoError:
                    {
                        if (parameters.Count != 1)
                        {
                            LibraryInstance.ErrorFormat(string.Format("ActivateSoulResponse Parameter Error, Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                case ErrorCode.Fail:
                    {
                        LibraryInstance.ErrorFormat("ActiveSoul Error DebugMessage: {0}", debugMessage);
                        answer.AnswerEventManager.ErrorInform(LauguageDictionarySelector.Instance[answer.UsingLanguage]["Fail"], LauguageDictionarySelector.Instance[answer.UsingLanguage]["Activate Soul Fail"]);
                        return false;
                    }
                case ErrorCode.PermissionDeny:
                    {
                        LibraryInstance.ErrorFormat("ActiveSoul Error DebugMessage: {0}", debugMessage);
                        answer.AnswerEventManager.ErrorInform(LauguageDictionarySelector.Instance[answer.UsingLanguage]["Permission Deny"], LauguageDictionarySelector.Instance[answer.UsingLanguage]["Activate Soul Fail"]);
                        return false;
                    }
                default:
                    {
                        LibraryInstance.ErrorFormat("ActiveSoul Error DebugMessage: {0}", debugMessage);
                        answer.AnswerEventManager.ErrorInform(LauguageDictionarySelector.Instance[answer.UsingLanguage]["Unknown Error"], LauguageDictionarySelector.Instance[answer.UsingLanguage]["Activate Soul Fail"]);
                        return false;
                    }
            }
        }

        internal override bool Handle(AnswerOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, returnCode, debugMessage, parameters))
            {
                try
                {
                    int soulID = (int)parameters[(byte)ActiveSoulResponseParameterCode.SoulID];
                    if (answer.ContainsSoul(soulID))
                    {
                        answer.FindSoul(soulID).IsActivate = true;
                        return true;
                    }
                    else
                    {
                        LibraryInstance.Error("ActiveSoulResponseError Soul Not Exist");
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("ActiveSoul Parameter Cast Error");
                    LibraryInstance.Error(ex.Message);
                    LibraryInstance.Error(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    LibraryInstance.Error(ex.Message);
                    LibraryInstance.Error(ex.StackTrace);
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
