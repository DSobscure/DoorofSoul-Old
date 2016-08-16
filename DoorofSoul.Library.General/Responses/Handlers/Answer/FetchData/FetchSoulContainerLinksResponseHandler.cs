using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Answer;
using DoorofSoul.Protocol.Language;
using System.Collections.Generic;
using System;

namespace DoorofSoul.Library.General.Responses.Handlers.Answer.FetchData
{
    internal class FetchSoulContainerLinksResponseHandler : FetchDataResponseHandler
    {
        internal FetchSoulContainerLinksResponseHandler(General.Answer answer) : base(answer)
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
                            LibraryInstance.ErrorFormat(string.Format("Fetch SoulContainerLinks Response Parameter Error, Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                default:
                    {
                        LibraryInstance.ErrorFormat("Fetch SoulContainerLinks Response Error DebugMessage: {0}", debugMessage);
                        answer.AnswerEventManager.ErrorInform(LauguageDictionarySelector.Instance[answer.UsingLanguage]["Unknown Error"], LauguageDictionarySelector.Instance[answer.UsingLanguage]["Fetch SoulContainerLinks Error"]);
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
                    int soulID = (int)parameters[(byte)FetchSoulContainerLinksResponseParameterCode.SoulID];
                    int containerID = (int)parameters[(byte)FetchSoulContainerLinksResponseParameterCode.ContainerID];
                    if (answer.ContainsContainer(containerID))
                    {
                        General.Container container = answer.FindContainer(containerID);
                        if(container.IsEmptyContainer)
                        {
                            answer.LinkSoulContainer(soulID, containerID);
                            container.ContainerOperationManager.FetchDataResolver.FetchEntity();
                            container.ContainerOperationManager.FetchDataResolver.FetchInventory();
                        }
                        else
                        {
                            answer.LinkSoulContainer(soulID, containerID);
                        }
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("Fetch SoulContainerLinks Parameter Cast Error");
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
