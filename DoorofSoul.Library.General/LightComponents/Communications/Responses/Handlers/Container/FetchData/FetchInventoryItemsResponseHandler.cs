using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Container;
using DoorofSoul.Protocol.Language;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers.Container.FetchData
{
    internal class FetchInventoryItemsResponseHandler : FetchDataResponseHandler
    {
        internal FetchInventoryItemsResponseHandler(NatureComponents.Container container) : base(container)
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
                            LibraryInstance.ErrorFormat(string.Format("Fetch InventoryItems Response Parameter Error, Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                default:
                    {
                        LibraryInstance.ErrorFormat("Fetch InventoryItems Response Error DebugMessage: {0}", debugMessage);
                        container.ContainerEventManager.ErrorInform(LauguageDictionarySelector.Instance[container.UsingLanguage]["Unknown Error"], LauguageDictionarySelector.Instance[container.UsingLanguage]["Fetch InventoryItems Error"], Protocol.Communication.Channels.ContainerCommunicationChannel.Answer);
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
                    if(container.Inventory != null)
                    {
                        int itemInfoID = (int)parameters[(byte)FetchInventoryItemsResponseParameterCode.ItemInfoID];
                        Item item = (Item)parameters[(byte)FetchInventoryItemsResponseParameterCode.Item];
                        int count = (int)parameters[(byte)FetchInventoryItemsResponseParameterCode.Count];
                        int positionIndex = (int)parameters[(byte)FetchInventoryItemsResponseParameterCode.PositionIndex];
                        container.Inventory.LoadItem(itemInfoID, item, count, positionIndex);
                        return true;
                    }
                    else
                    {
                        LibraryInstance.Error("Fetch InventoryItems Response Fail, No Inventory");
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("Fetch InventoryItems Response Parameter Cast Error");
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
