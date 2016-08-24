using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Library.General.NatureComponents.SceneElements;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Scene;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Scene.FetchData
{
    internal class FetchItemEntitiesHandler : FetchDataHandler
    {
        internal FetchItemEntitiesHandler(NatureComponents.Scene scene) : base(scene, 0)
        {
        }

        internal override bool Handle(SceneFetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            if (base.Handle(fetchCode, parameter))
            {
                try
                {
                    foreach (ItemEntity itemEntity in scene.ItemEntityManager.ItemEntities)
                    {
                        var result = new Dictionary<byte, object>
                        {
                            { (byte)FetchItemEntitiesResponseParameterCode.ItemEntityID, itemEntity.ItemEntityID },
                            { (byte)FetchItemEntitiesResponseParameterCode.ItemID, itemEntity.ItemID },
                            { (byte)FetchItemEntitiesResponseParameterCode.Position, itemEntity.Position }
                        };
                        SendResponse(fetchCode, result);
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("FetchItemEntities Invalid Cast!");
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
