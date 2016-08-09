using DoorofSoul.Library.General.ContainerElements;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Container;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers.Container.FetchData
{
    internal class FetchInventoryItemsHandler : FetchDataHandler
    {
        internal FetchInventoryItemsHandler(General.Container container) : base(container)
        {
        }

        internal override bool CheckParameter(Dictionary<byte, object> parameters, out string debugMessage)
        {
            if (parameters.Count != 0)
            {
                debugMessage = string.Format("Container Fetch InventoryItems Parameter Error Parameter Count: {0}", parameters.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        internal override bool Handle(ContainerFetchDataCode fetchCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(fetchCode, parameters))
            {
                try
                {
                    foreach(ItemInfo info in container.Inventory.ItemInfos)
                    {
                        var result = new Dictionary<byte, object>
                        {
                            { (byte)FetchInventoryItemsResponseParameterCode.Item, info.item },
                            { (byte)FetchInventoryItemsResponseParameterCode.Count, info.count },
                            { (byte)FetchInventoryItemsResponseParameterCode.PositionIndex, info.positionIndex }
                        };
                        SendResponse(fetchCode, result);
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("FetchInventoryItems Invalid Cast!");
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
