using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Language;

namespace DoorofSoul.Library.General.Responses.Handlers.Answer
{
    public class CreateSoulResponseHandler : AnswerResponseHandler
    {
        public CreateSoulResponseHandler(General.Answer answer) : base(answer)
        {
        }

        public override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            switch (returnCode)
            {
                case ErrorCode.NoError:
                    {
                        if (parameters.Count != 0)
                        {
                            LibraryLog.ErrorFormat(string.Format("Create Soul Response Parameter Error, Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                case ErrorCode.Fail:
                    {
                        LibraryLog.ErrorFormat("Create Soul Response Error DebugMessage: {0}", debugMessage);
                        answer.ErrorInform(LauguageDictionarySelector.Instance[answer.UsingLanguage]["Fail"], LauguageDictionarySelector.Instance[answer.UsingLanguage]["Create Soul Fail"]);
                        return false;
                    }
                case ErrorCode.PermissionDeny:
                    {
                        LibraryLog.ErrorFormat("Create Soul Response Error DebugMessage: {0}", debugMessage);
                        answer.ErrorInform(LauguageDictionarySelector.Instance[answer.UsingLanguage]["Permission Deny"], LauguageDictionarySelector.Instance[answer.UsingLanguage]["Create Soul PermissionDeny"]);
                        return false;
                    }
                default:
                    {
                        LibraryLog.ErrorFormat("Create Soul Response Error DebugMessage: {0}", debugMessage);
                        answer.ErrorInform(LauguageDictionarySelector.Instance[answer.UsingLanguage]["Unknown Error"], LauguageDictionarySelector.Instance[answer.UsingLanguage]["Create Soul Error"]);
                        return false;
                    }
            }
        }

        public override bool Handle(AnswerOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, returnCode, debugMessage, parameters))
            {
                answer.FetchSouls();
                answer.FetchContainers();
                answer.FetchSoulContainerLinks();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
