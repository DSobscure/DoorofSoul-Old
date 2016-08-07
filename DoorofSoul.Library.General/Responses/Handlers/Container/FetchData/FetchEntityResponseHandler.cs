using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Container;
using DoorofSoul.Protocol.Language;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers.Container.FetchData
{
    internal class FetchEntityResponseHandler : FetchDataResponseHandler
    {
        internal FetchEntityResponseHandler(General.Container container) : base(container)
        {
        }

        internal override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            switch (returnCode)
            {
                case ErrorCode.NoError:
                    {
                        if (parameters.Count != 4)
                        {
                            LibraryInstance.ErrorFormat(string.Format("Fetch Entity Response Parameter Error, Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                default:
                    {
                        LibraryInstance.ErrorFormat("Fetch Entity Response Error DebugMessage: {0}", debugMessage);
                        container.ContainerEventManager.ErrorInform(LauguageDictionarySelector.Instance[container.UsingLanguage]["Unknown Error"], LauguageDictionarySelector.Instance[container.UsingLanguage]["Fetch Entity Error"], Protocol.Communication.Channels.ContainerCommunicationChannel.Answer);
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
                    int entityID = (int)parameters[(byte)FetchEntityResponseParameterCode.EntityID];
                    string entityName = (string)parameters[(byte)FetchEntityResponseParameterCode.EntityName];
                    int locatedSceneID = (int)parameters[(byte)FetchEntityResponseParameterCode.LocatedSceneID];
                    EntitySpaceProperties entitySpaceProperties = (EntitySpaceProperties)parameters[(byte)FetchEntityResponseParameterCode.EntitySpaceProperties];
                    General.Entity entity = new General.Entity(entityID, entityName, locatedSceneID, entitySpaceProperties);
                    container.BindEntity(entity);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("Fetch Entity Response Parameter Cast Error");
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
