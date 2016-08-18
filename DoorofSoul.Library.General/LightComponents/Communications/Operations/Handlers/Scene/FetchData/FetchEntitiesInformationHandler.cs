using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Scene;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Scene.FetchData
{
    internal class FetchEntitiesHandler : FetchDataHandler
    {
        internal FetchEntitiesHandler(NatureComponents.Scene scene) : base(scene, 0)
        {
        }

        internal override bool Handle(SceneFetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            if (base.Handle(fetchCode, parameter))
            {
                try
                {
                    foreach (NatureComponents.Entity entity in scene.Entities)
                    {
                        var result = new Dictionary<byte, object>
                            {
                                { (byte)FetchEntitiesResponseParameterCode.EntityID, entity.EntityID },
                                { (byte)FetchEntitiesResponseParameterCode.EntityName, entity.EntityName },
                                { (byte)FetchEntitiesResponseParameterCode.EntitySpaceProperties, entity.SpaceProperties }
                            };
                        SendResponse(fetchCode, result);
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("FetchEntities Invalid Cast!");
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
