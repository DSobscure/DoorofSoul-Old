using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Library.General.NatureComponents.ContainerElements;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Container;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Container
{
    internal class MoveInventoryItemInfoHandler : ContainerOperationHandler
    {
        internal MoveInventoryItemInfoHandler(NatureComponents.Container container) : base(container, 2)
        {
        }

        internal override bool Handle(ContainerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                try
                {
                    int originPosition = (int)parameters[(byte)MoveInventoryItemInfoParameterCode.NewPosition];
                    int newPosition = (int)parameters[(byte)MoveInventoryItemInfoParameterCode.OriginPosition];
                    Inventory inventory = container.Inventory;
                    lock (inventory)
                    {
                        inventory.SwapItemInfo(originPosition, newPosition);
                        return true;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("MoveInventoryItemInfo Operation Invalid Cast!");
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
