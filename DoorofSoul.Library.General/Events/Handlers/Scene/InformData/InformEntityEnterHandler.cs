using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.InformDataParameters.Scene;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Scene.InformData
{
    public class InformEntityEnterHandler : InformDataHandler
    {
        public InformEntityEnterHandler(General.Scene scene) : base(scene)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameters, out string debugMessage)
        {
            if (parameters.Count != 3)
            {
                debugMessage = string.Format("Inform EntityEnter Event Parameter Error, Parameter Count: {0}", parameters.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(SceneInformDataCode informCode, ErrorCode returnCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(informCode, returnCode, parameters))
            {
                try
                {
                    int entityID = (int)parameters[(byte)InformEntityEnterParameterCode.EntityID];
                    string entityName = (string)parameters[(byte)InformEntityEnterParameterCode.EntityName];
                    EntitySpaceProperties entitySpaceProperties = (EntitySpaceProperties)parameters[(byte)InformEntityEnterParameterCode.EntitySpaceProperties];
                    General.Entity entity = new General.Entity(entityID, entityName, scene.SceneID, entitySpaceProperties);
                    scene.EntityEnter(entity);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("Inform SceneEntityEnter Event Parameter Cast Error");
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
