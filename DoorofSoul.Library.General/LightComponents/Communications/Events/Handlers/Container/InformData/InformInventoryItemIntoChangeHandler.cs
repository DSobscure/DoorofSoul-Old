using System;
using System.Collections.Generic;
using System.Linq;
using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Library.General.NatureComponents.ContainerElements;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.InformDataParameters.Container;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Container.InformData
{
    internal class InformInventoryItemIntoChangeHandler : InformDataHandler
    {
        internal InformInventoryItemIntoChangeHandler(NatureComponents.Container container) : base(container, 3)
        {
        }

        internal override bool Handle(ContainerInformDataCode informCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(informCode, parameters))
            {
                try
                {
                    Item item = (Item)parameters[(byte)InformInventoryItemInfoChangeParameterCode.Item];
                    int itemCount = (int)parameters[(byte)InformInventoryItemInfoChangeParameterCode.Count];
                    int positionIndex = (int)parameters[(byte)InformInventoryItemInfoChangeParameterCode.PositionIndex];
                    container.Inventory.LoadItem(item, itemCount, positionIndex);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("InformInventoryItemIntoChange Parameter Cast Error");
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
