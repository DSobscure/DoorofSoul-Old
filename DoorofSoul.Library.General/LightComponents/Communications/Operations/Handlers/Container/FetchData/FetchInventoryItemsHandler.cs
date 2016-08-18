using DoorofSoul.Library.General.NatureComponents.ContainerElements;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Container;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Container.FetchData
{
    internal class FetchInventoryItemsHandler : FetchDataHandler
    {
        internal FetchInventoryItemsHandler(NatureComponents.Container container) : base(container, 0)
        {
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
