﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Language;

namespace DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers.Answer
{
    internal class DeleteSoulResponseHandler : AnswerResponseHandler
    {
        internal DeleteSoulResponseHandler(ThroneComponents.Answer answer) : base(answer)
        {
        }

        internal override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            switch (returnCode)
            {
                case ErrorCode.NoError:
                    {
                        if (parameters.Count != 0)
                        {
                            LibraryInstance.ErrorFormat(string.Format("Delete Soul Response Parameter Error, Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                case ErrorCode.Fail:
                    {
                        LibraryInstance.ErrorFormat("Delete Soul Response Error DebugMessage: {0}", debugMessage);
                        answer.AnswerEventManager.ErrorInform(LauguageDictionarySelector.Instance[answer.UsingLanguage]["Fail"], LauguageDictionarySelector.Instance[answer.UsingLanguage]["Delete Soul Fail"]);
                        return false;
                    }
                case ErrorCode.PermissionDeny:
                    {
                        LibraryInstance.ErrorFormat("Delete Soul Response Error DebugMessage: {0}", debugMessage);
                        answer.AnswerEventManager.ErrorInform(LauguageDictionarySelector.Instance[answer.UsingLanguage]["Permission Deny"], LauguageDictionarySelector.Instance[answer.UsingLanguage]["Delete Soul PermissionDeny"]);
                        return false;
                    }
                default:
                    {
                        LibraryInstance.ErrorFormat("Delete Soul Response Error DebugMessage: {0}", debugMessage);
                        answer.AnswerEventManager.ErrorInform(LauguageDictionarySelector.Instance[answer.UsingLanguage]["Unknown Error"], LauguageDictionarySelector.Instance[answer.UsingLanguage]["Delete Soul Error"]);
                        return false;
                    }
            }
        }

        internal override bool Handle(AnswerOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, returnCode, debugMessage, parameters))
            {
                answer.ClearSouls();
                answer.ClearContainers();
                answer.AnswerOperationManager.FetchDataResolver.FetchSouls();
                answer.AnswerOperationManager.FetchDataResolver.FetchContainers();
                answer.AnswerOperationManager.FetchDataResolver.FetchSoulContainerLinks();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}