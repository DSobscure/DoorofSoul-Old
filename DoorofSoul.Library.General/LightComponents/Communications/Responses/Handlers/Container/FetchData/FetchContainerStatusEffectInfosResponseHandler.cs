using System;
using System.Collections.Generic;
using System.Linq;
using DoorofSoul.Protocol.Language;
using DoorofSoul.Library.General.KnowledgeComponents.StatusEffects;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Container;

namespace DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers.Container.FetchData
{
    internal class FetchContainerStatusEffectInfosResponseHandler : FetchDataResponseHandler
    {
        public FetchContainerStatusEffectInfosResponseHandler(NatureComponents.Container container) : base(container)
        {
        }

        internal override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            switch (returnCode)
            {
                case ErrorCode.NoError:
                    {
                        if (parameters.Count != 1)
                        {
                            LibraryInstance.ErrorFormat(string.Format("Fetch ContainerStatusEffectInfos Response Parameter Error, Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                default:
                    {
                        LibraryInstance.ErrorFormat("Fetch ContainerStatusEffectInfos Response Error DebugMessage: {0}", debugMessage);
                        container.ContainerEventManager.ErrorInform(LauguageDictionarySelector.Instance[container.UsingLanguage]["Unknown Error"], LauguageDictionarySelector.Instance[container.UsingLanguage]["Fetch ContainerStatusEffectInfos Error"], Protocol.Communication.Channels.ContainerCommunicationChannel.Answer);
                        return false;
                    }
            }
        }

        internal override bool Handle(ContainerFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(fetchCode, returnCode, fetchDebugMessage, parameters))
            {
                try
                {
                    ContainerStatusEffectInfo info = (ContainerStatusEffectInfo)parameters[(byte)FetchContainerStatusEffectInfosResponseParameterCode.ContainerStatusEffectInfo];
                    if(container.ContainerID == info.AffectedContainerID)
                    {
                        container.ContainerStatusEffectManager.LoadStatusEffectInfo(info);
                        return true;
                    }
                    else
                    {
                        LibraryInstance.ErrorFormat("Fetch ContainerStatusEffectInfos Response Fail, ContainerID: {0}, AffectedContainerID: {1}", container.ContainerID, info.AffectedContainerID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("Fetch ContainerStatusEffectInfos Response Parameter Cast Error");
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
