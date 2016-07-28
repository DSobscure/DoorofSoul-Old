using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Answer;
using DoorofSoul.Protocol.Language;
using System.Collections.Generic;
using System;

namespace DoorofSoul.Library.General.Responses.Handlers.Answer.FetchData
{
    public class FetchSoulContainerLinksResponseHandler : FetchDataResponseHandler
    {
        public FetchSoulContainerLinksResponseHandler(General.Answer answer) : base(answer)
        {
        }

        public override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            switch (returnCode)
            {
                case ErrorCode.NoError:
                    {
                        if (parameters.Count != 2)
                        {
                            LibraryLog.ErrorFormat(string.Format("Fetch SoulContainerLinks Response Parameter Error, Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                default:
                    {
                        LibraryLog.ErrorFormat("Fetch SoulContainerLinks Response Error DebugMessage: {0}", debugMessage);
                        answer.ErrorInform(LauguageDictionarySelector.Instance[answer.UsingLanguage]["Unknown Error"], LauguageDictionarySelector.Instance[answer.UsingLanguage]["Fetch SoulContainerLinks Error"]);
                        return false;
                    }
            }
        }

        public override bool Handle(AnswerFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(fetchCode, returnCode, fetchDebugMessage, parameters))
            {
                try
                {
                    int soulID = (int)parameters[(byte)FetchSoulContainerLinksResponseParameterCode.SoulID];
                    int containerID = (int)parameters[(byte)FetchSoulContainerLinksResponseParameterCode.ContainerID];
                    answer.LinkSoulContainer(soulID, containerID);
                    if(answer.ContainsContainer(containerID))
                    {
                        General.Container container = answer.FindContainer(containerID);
                        if(container.Entity == null)
                        {
                            container.FetchEntity();
                        }
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.Error("Fetch Souls SoulContainerLinks Parameter Cast Error");
                    LibraryLog.Error(ex.Message);
                    LibraryLog.Error(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    LibraryLog.Error(ex.Message);
                    LibraryLog.Error(ex.StackTrace);
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
