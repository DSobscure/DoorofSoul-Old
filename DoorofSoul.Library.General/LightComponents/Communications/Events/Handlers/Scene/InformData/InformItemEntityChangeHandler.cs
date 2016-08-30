using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Library.General.NatureComponents.SceneElements;
using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.InformDataParameters.Scene;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Scene.InformData
{
    internal class InformItemEntityChangeHandler : InformDataHandler
    {
        internal InformItemEntityChangeHandler(NatureComponents.Scene scene) : base(scene, 4)
        {
        }
        internal override bool Handle(SceneInformDataCode informCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(informCode, parameters))
            {
                try
                {
                    int itemEntityID = (int)parameters[(byte)InformItemEntityChangeParameterCode.ItemEntityID];
                    int itemID = (int)parameters[(byte)InformItemEntityChangeParameterCode.ItemID];
                    DSVector3 position = (DSVector3)parameters[(byte)InformItemEntityChangeParameterCode.Position];
                    DataChangeTypeCode changeTypeCode = (DataChangeTypeCode)parameters[(byte)InformItemEntityChangeParameterCode.DataChangeType];
                    ItemEntity itemEntity = new ItemEntity(itemEntityID, itemID, scene.SceneID, position);
                    switch (changeTypeCode)
                    {
                        case DataChangeTypeCode.Load:
                            scene.ItemEntityManager.LoadItemEntity(itemEntity);
                            break;
                        case DataChangeTypeCode.Unload:
                            scene.ItemEntityManager.UnloadItemEntity(itemEntity);
                            break;
                        case DataChangeTypeCode.Update:
                            break;
                        case DataChangeTypeCode.Initial:
                            break;
                        case DataChangeTypeCode.ClearAll:
                            break;
                        default:
                            break;
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("InformItemEntityChange Parameter Cast Error");
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
