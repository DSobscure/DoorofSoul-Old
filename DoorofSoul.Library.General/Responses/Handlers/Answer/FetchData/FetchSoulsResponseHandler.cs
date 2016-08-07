using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Answer;
using DoorofSoul.Protocol.Language;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers.Answer.FetchData
{
    internal class FetchSoulsResponseHandler : FetchDataResponseHandler
    {
        internal FetchSoulsResponseHandler(General.Answer answer) : base(answer)
        {
        }

        internal override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            switch (returnCode)
            {
                case ErrorCode.NoError:
                    {
                        if (parameters.Count != 2)
                        {
                            LibraryInstance.ErrorFormat(string.Format("Fetch Soul Response Parameter Error, Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                default:
                    {
                        LibraryInstance.ErrorFormat("Fetch Soul Response Error DebugMessage: {0}", debugMessage);
                        answer.AnswerEventManager.ErrorInform(LauguageDictionarySelector.Instance[answer.UsingLanguage]["Unknown Error"], LauguageDictionarySelector.Instance[answer.UsingLanguage]["Fetch Soul Error"]);
                        return false;
                    }
            }
        }

        internal override bool Handle(AnswerFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(fetchCode, returnCode, fetchDebugMessage, parameters))
            {
                try
                {
                    int soulID = (int)parameters[(byte)FetchSoulsResponseParameterCode.SoulID];
                    string soulName = (string)parameters[(byte)FetchSoulsResponseParameterCode.SoulName];
                    answer.LoadSouls(new List<General.Soul> { new General.Soul(soulID, answer, soulName) });
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("Fetch Souls Response Parameter Cast Error");
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
