using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Container;
using DoorofSoul.Protocol.Language;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers.Container.FetchData
{
    internal class FetchInventoryResponseHandler : FetchDataResponseHandler
    {
        internal FetchInventoryResponseHandler(General.Container container) : base(container)
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
                            LibraryInstance.ErrorFormat(string.Format("Fetch Inventory Response Parameter Error, Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                default:
                    {
                        LibraryInstance.ErrorFormat("Fetch Inventory Response Error DebugMessage: {0}", debugMessage);
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
                    int inventoryID = (int)parameters[(byte)FetchInventoryResponseParameterCode.InventoryID];
                    int capacity = (int)parameters[(byte)FetchInventoryResponseParameterCode.Capacity];
                    Inventory inventory = new Inventory(inventoryID, container.ContainerID, capacity);
                    container.BindInventory(inventory);
                    container.ContainerOperationManager.FetchDataResolver.FetchInventoryItems();
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("Fetch Inventory Response Parameter Cast Error");
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
