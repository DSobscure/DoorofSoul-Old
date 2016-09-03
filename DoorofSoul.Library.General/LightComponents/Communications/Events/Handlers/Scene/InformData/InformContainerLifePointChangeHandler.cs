using DoorofSoul.Library.General.BasicTypeHelpers;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.InformDataParameters.Scene;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Scene.InformData
{
    internal class InformContainerLifePointChangeHandler : InformDataHandler
    {
        internal InformContainerLifePointChangeHandler(NatureComponents.Scene scene) : base(scene, 3)
        {
        }

        internal override bool Handle(SceneInformDataCode informCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(informCode, parameters))
            {
                try
                {
                    int containerID = (int)parameters[(byte)InformContainerLifePointChangeParameterCode.ContainerID];
                    decimal newLifePoint = (decimal)(DSDecimal)parameters[(byte)InformContainerLifePointChangeParameterCode.LifePoint];
                    decimal delta = (decimal)(DSDecimal)parameters[(byte)InformContainerLifePointChangeParameterCode.Delta];
                    if (scene.ContainsContainer(containerID))
                    {
                        NatureComponents.Container container = scene.FindContainer(containerID);
                        container.Attributes.LifePoint = newLifePoint;
                        container.ContainerController.ShowLifePointDelta(delta);
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("InformContainerLifePointChange Event Parameter Cast Error");
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
