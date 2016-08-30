using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.InformDataParameters.Scene;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Scene.InformData
{
    internal class SynchronizeEntityPositionHandler : InformDataHandler
    {
        public SynchronizeEntityPositionHandler(NatureComponents.Scene scene) : base(scene, 2)
        {
        }

        internal override bool Handle(SceneInformDataCode informCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(informCode, parameters))
            {
                try
                {
                    int entityID = (int)parameters[(byte)SynchronizeEntityPositionParameterCode.EntityID];
                    DSVector3 position = (DSVector3)parameters[(byte)SynchronizeEntityPositionParameterCode.Position];
                    if(scene.ContainsEntity(entityID))
                    {
                        scene.FindEntity(entityID).SynchronizePosition(position); 
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("SynchronizeEntityPosition Event Parameter Cast Error");
                    LibraryInstance.ErrorFormat(ex.Message);
                    LibraryInstance.ErrorFormat(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    LibraryInstance.ErrorFormat(ex.Message);
                    LibraryInstance.ErrorFormat(ex.StackTrace);
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
