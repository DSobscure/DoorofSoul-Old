using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Container;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Container
{
    internal class ObserveBulletHitHandler : ContainerOperationHandler
    {
        internal ObserveBulletHitHandler(NatureComponents.Container container) : base(container, 2)
        {
        }

        internal override bool Handle(ContainerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                try
                {
                    int containerID = (int)parameters[(byte)ObserveBulletHitParameterCode.HitContainerID];
                    float impulse = (float)parameters[(byte)ObserveBulletHitParameterCode.Impulse];
                    if (container.Entity.LocatedScene.SceneEye.Observer == container)
                    {
                        LibraryInstance.ErrorFormat("ContainerID:{0} hit impulse: {1}", containerID, impulse);
                        return true;
                    }
                    else
                    {
                        LibraryInstance.ErrorFormat("Container ObserveBulletHit PermissionDeny ContainerID: {0}", container.ContainerID);
                        SendError(operationCode, ErrorCode.PermissionDeny, "Observer is not you");
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("Container ObserveBulletHit Operation Invalid Cast!");
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
