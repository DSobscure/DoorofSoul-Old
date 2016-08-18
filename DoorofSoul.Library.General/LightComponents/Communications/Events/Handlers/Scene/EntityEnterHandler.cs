using DoorofSoul.Library.General.NatureComponents.EntityElements;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters.Scene;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Scene
{
    internal class EntityEnterHandler : SceneEventHandler
    {
        internal EntityEnterHandler(NatureComponents.Scene scene) : base(scene, 3)
        {
        }

        internal override bool Handle(SceneEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(eventCode, parameters))
            {
                try
                {
                    int entityID = (int)parameters[(byte)EntityEnterParameterCode.EntityID];
                    string entityName = (string)parameters[(byte)EntityEnterParameterCode.EntityName];
                    EntitySpaceProperties entitySpaceProperties = (EntitySpaceProperties)parameters[(byte)EntityEnterParameterCode.EntitySpaceProperties];
                    NatureComponents.Entity entity = new NatureComponents.Entity(entityID, entityName, scene.SceneID, entitySpaceProperties);
                    scene.EntityEnter(entity);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("SceneEntityEnter Event Parameter Cast Error");
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
