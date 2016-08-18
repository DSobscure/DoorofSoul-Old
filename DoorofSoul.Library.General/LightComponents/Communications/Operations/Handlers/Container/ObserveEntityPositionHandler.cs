using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Container;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Container
{
    internal class ObserveEntityPositionHandler : ContainerOperationHandler
    {
        public ObserveEntityPositionHandler(NatureComponents.Container container) : base(container, 2)
        {
        }

        internal override bool Handle(ContainerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                try
                {
                    int entityID = (int)parameters[(byte)ObserveEntityPositionParameterCode.EntityID];
                    DSVector3 position = (DSVector3)parameters[(byte)ObserveEntityPositionParameterCode.Position];
                    if (container.Entity.LocatedScene.SceneEye.Observer == container)
                    {
                        container.Entity.LocatedScene.SceneEye.UpdateEntityPosition(entityID, position);
                        return true;
                    }
                    else
                    {
                        LibraryInstance.ErrorFormat("Container ObserveEntityPosition PermissionDeny ContainerID: {0}", container.ContainerID);
                        SendError(operationCode, ErrorCode.PermissionDeny, "Observer is not you");
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("Container ObserveEntityPosition Operation Invalid Cast!");
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
