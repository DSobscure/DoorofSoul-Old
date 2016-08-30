using DoorofSoul.Library.General.NatureComponents.ContainerElements;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.InformDataParameters.Scene;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Scene.InformData
{
    internal class InformContainerEnterHandler : InformDataHandler
    {
        internal InformContainerEnterHandler(NatureComponents.Scene scene) : base(scene, 4)
        {
        }

        internal override bool Handle(SceneInformDataCode informCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(informCode, parameters))
            {
                try
                {
                    int containerID = (int)parameters[(byte)InformContainerEnterParameterCode.ContainerID];
                    int entityID = (int)parameters[(byte)InformContainerEnterParameterCode.EntityID];
                    string containerName = (string)parameters[(byte)InformContainerEnterParameterCode.ContainerName];
                    ContainerAttributes attributes = (ContainerAttributes)parameters[(byte)InformContainerEnterParameterCode.ContainerAttributes];
                    NatureComponents.Container container = new NatureComponents.Container(containerID, entityID, containerName, attributes);
                    scene.ContainerEnter(container);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("InformContainerEnter Event Parameter Cast Error");
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
