using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.InformDataParameters.Scene;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Scene.InformData
{
    internal class InformEntityExitHandler : InformDataHandler
    {
        internal InformEntityExitHandler(NatureComponents.Scene scene) : base(scene, 1)
        {
        }

        internal override bool Handle(SceneInformDataCode informCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(informCode, parameters))
            {
                try
                {
                    int entityID = (int)parameters[(byte)InformEntityExitParameterCode.EntityID];
                    scene.EntityExit(entityID);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("InformEntityEnter Event Parameter Cast Error");
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
