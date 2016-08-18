using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters.Scene;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Scene
{
    internal class ContainerEventResolver : SceneEventHandler
    {
        internal ContainerEventResolver(NatureComponents.Scene scene) : base(scene, 3)
        {
        }

        internal override bool Handle(SceneEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(eventCode, parameters))
            {
                try
                {
                    int containerID = (int)parameters[(byte)ContainerEventParameterCode.ContainerID];
                    ContainerEventCode resolvedEventCode = (ContainerEventCode)parameters[(byte)ContainerEventParameterCode.EventCode];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)ContainerEventParameterCode.Parameters];
                    if (scene.ContainsContainer(containerID))
                    {
                        scene.FindContainer(containerID).ContainerEventManager.Operate(resolvedEventCode, resolvedParameters);
                        return true;
                    }
                    else
                    {
                        LibraryInstance.ErrorFormat("ContainerEvent Error Container ID: {0} Not in Scene ID: {1}", containerID, scene.SceneID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("ContainerEvent Parameter Cast Error");
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
