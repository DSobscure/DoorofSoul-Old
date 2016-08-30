using DoorofSoul.Library.General.NatureComponents.EntityElements;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.InformDataParameters.Scene;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Scene.InformData
{
    internal class InformEntityEnterHandler : InformDataHandler
    {
        internal InformEntityEnterHandler(NatureComponents.Scene scene) : base(scene, 3)
        {
        }

        internal override bool Handle(SceneInformDataCode informCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(informCode, parameters))
            {
                try
                {
                    int entityID = (int)parameters[(byte)InformEntityEnterParameterCode.EntityID];
                    string entityName = (string)parameters[(byte)InformEntityEnterParameterCode.EntityName];
                    EntitySpaceProperties entitySpaceProperties = (EntitySpaceProperties)parameters[(byte)InformEntityEnterParameterCode.EntitySpaceProperties];
                    NatureComponents.Entity entity = new NatureComponents.Entity(entityID, entityName, scene.SceneID, entitySpaceProperties);
                    scene.EntityEnter(entity);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("InformSceneEntityEnter Event Parameter Cast Error");
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
