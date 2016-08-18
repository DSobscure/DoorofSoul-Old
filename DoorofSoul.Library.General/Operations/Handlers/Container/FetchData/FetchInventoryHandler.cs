using System;
using System.Collections.Generic;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Container;

namespace DoorofSoul.Library.General.Operations.Handlers.Container.FetchData
{
    internal class FetchInventoryHandler : FetchDataHandler
    {
        internal FetchInventoryHandler(General.Container container) : base(container, 0)
        {
        }

        internal override bool Handle(ContainerFetchDataCode fetchCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(fetchCode, parameters))
            {
                try
                {
                    var result = new Dictionary<byte, object>
                    {
                        { (byte)FetchInventoryResponseParameterCode.InventoryID, container.Inventory.InventoryID },
                        { (byte)FetchInventoryResponseParameterCode.Capacity, container.Inventory.Capacity }
                    };
                    SendResponse(fetchCode, result);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("FetchInventory Invalid Cast!");
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
