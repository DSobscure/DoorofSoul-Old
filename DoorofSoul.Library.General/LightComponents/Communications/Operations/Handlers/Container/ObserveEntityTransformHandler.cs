using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Container;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Container
{
    internal class ObserveEntityTransformHandler : ContainerOperationHandler
    {
        public ObserveEntityTransformHandler(NatureComponents.Container container) : base(container, 3)
        { 
        }

        internal override bool Handle(ContainerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                try
                {
                    int entityID = (int)parameters[(byte)ObserveEntityTransformParameterCode.EntityID];
                    DSVector3 position = (DSVector3)parameters[(byte)ObserveEntityTransformParameterCode.Position];
                    DSVector3 rotation = (DSVector3)parameters[(byte)ObserveEntityTransformParameterCode.Rotation];
                    if (container.Entity.LocatedScene.SceneEye.Observer == container)
                    {
                        container.Entity.LocatedScene.SceneEye.UpdateEntityTransform(entityID, position, rotation);
                        return true;
                    }
                    else
                    {
                        LibraryInstance.ErrorFormat("Container ObserveEntityTransform PermissionDeny ContainerID: {0}", container.ContainerID);
                        SendError(operationCode, ErrorCode.PermissionDeny, "Observer is not you");
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("Container ObserveEntityTransform Operation Invalid Cast!");
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
