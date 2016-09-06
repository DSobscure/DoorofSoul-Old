using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Scene;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Scene.FetchData
{
    internal class FetchContainersHandler : FetchDataHandler
    {
        internal FetchContainersHandler(NatureComponents.Scene scene) : base(scene, 0)
        {
        }

        internal override bool Handle(SceneFetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            if (base.Handle(fetchCode, parameter))
            {
                try
                {
                    foreach (NatureComponents.Container container in scene.Containers)
                    {
                        var result = new Dictionary<byte, object>
                        {
                            { (byte)FetchContainersResponseParameterCode.ContainerID, container.ContainerID },
                            { (byte)FetchContainersResponseParameterCode.EntityID, container.EntityID },
                            { (byte)FetchContainersResponseParameterCode.ContainerName, container.ContainerName },
                            { (byte)FetchContainersResponseParameterCode.ContainerAttributes, container.Attributes }
                        };
                        SendResponse(fetchCode, result);
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("Fetch Containers Invalid Cast!");
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
