using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters.Scene;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Scene
{
    internal class EntityExitHandler : SceneEventHandler
    {
        internal EntityExitHandler(NatureComponents.Scene scene) : base(scene, 1)
        {
        }

        internal override bool Handle(SceneEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(eventCode, parameters))
            {
                try
                {
                    int entityID = (int)parameters[(byte)EntityExitParameterCode.EntityID];
                    scene.EntityExit(entityID);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("EntityEnter Event Parameter Cast Error");
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
