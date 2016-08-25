using System;
using System.Collections.Generic;
using System.Linq;
using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Library.General.NatureComponents.ContainerElements;
using DoorofSoul.Library.General.NatureComponents.SceneElements;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Container;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Container
{
    internal class PickupItemEntityHandler : ContainerOperationHandler
    {
        internal PickupItemEntityHandler(NatureComponents.Container container) : base(container, 1)
        {
        }

        internal override bool Handle(ContainerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                try
                {
                    int itemEntityID = (int)parameters[(byte)PickupItemEntityParameterCode.ItemEntityID];
                    lock(container.Entity.LocatedScene.ItemEntityManager)
                    {
                        if (container.Entity.LocatedScene.ItemEntityManager.ContainsItemEntity(itemEntityID))
                        {
                            ItemEntity itemEntity = container.Entity.LocatedScene.ItemEntityManager.FindItemEntity(itemEntityID);
                            Item item = LibraryInstance.ElementInterface.FindItem(itemEntity.ItemID);
                            InventoryItemInfo info;
                            if (container.Inventory.AddItem(item, 1, out info))
                            {
                                if (info.item != null)
                                {
                                    container.Entity.LocatedScene.ItemEntityManager.DeleteItemEntity(itemEntity.ItemEntityID);
                                }
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            LibraryInstance.ErrorFormat("PickupItemEntity Operation ItemEntity Not In Scene, ItemEntityID: {0}, SceneID: {1}", itemEntityID, container.Entity.LocatedScene.SceneID);
                            return false;
                        }
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("PickupItemEntity Operation Invalid Cast!");
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
