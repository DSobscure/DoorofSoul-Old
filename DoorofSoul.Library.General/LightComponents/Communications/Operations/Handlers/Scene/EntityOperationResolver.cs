using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Scene;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Scene
{
    internal class EntityOperationResolver : SceneOperationHandler
    {
        internal EntityOperationResolver(NatureComponents.Scene scene) : base(scene, 3)
        {
        }

        internal override bool Handle(SceneOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                try
                {
                    int entityID = (int)parameters[(byte)EntityOperationParameterCode.EntityID];
                    EntityOperationCode resolvedOperationCode = (EntityOperationCode)parameters[(byte)EntityOperationParameterCode.OperationCode];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)EntityOperationParameterCode.Parameters];
                    if (scene.ContainsEntity(entityID))
                    {
                        scene.FindEntity(entityID).EntityOperationManager.Operate(resolvedOperationCode, resolvedParameters);
                        return true;
                    }
                    else
                    {
                        LibraryInstance.ErrorFormat("EntityOperation Error Entity ID: {0} Not in Scene ID: {1}", entityID, scene.SceneID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("EntityOperation Parameter Cast Error");
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
