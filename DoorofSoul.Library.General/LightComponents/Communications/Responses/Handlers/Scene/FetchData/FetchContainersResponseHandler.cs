using System;
using System.Collections.Generic;
using System.Linq;
using DoorofSoul.Protocol.Language;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Scene;
using DoorofSoul.Library.General.NatureComponents.ContainerElements;

namespace DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers.Scene.FetchData
{
    internal class FetchContainersResponseHandler : FetchDataResponseHandler
    {
        internal FetchContainersResponseHandler(NatureComponents.Scene scene) : base(scene)
        {
        }

        internal override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            switch (returnCode)
            {
                case ErrorCode.NoError:
                    {
                        if (parameters.Count != 4)
                        {
                            LibraryInstance.ErrorFormat(string.Format("Fetch Containers Response Parameter Error, Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                default:
                    {
                        LibraryInstance.ErrorFormat("Fetch Containers Response Error DebugMessage: {0}", debugMessage);
                        scene.SceneEventManager.ErrorInform(LauguageDictionarySelector.Instance[scene.UsingLanguage]["Unknown Error"], LauguageDictionarySelector.Instance[scene.UsingLanguage]["Fetch Container Error"]);
                        return false;
                    }
            }
        }

        internal override bool Handle(SceneFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(fetchCode, returnCode, fetchDebugMessage, parameters))
            {
                try
                {
                    int containerID = (int)parameters[(byte)FetchContainersResponseParameterCode.ContainerID];
                    int entityID = (int)parameters[(byte)FetchContainersResponseParameterCode.EntityID];
                    string containerName = (string)parameters[(byte)FetchContainersResponseParameterCode.ContainerName];
                    ContainerAttributes attributes = (ContainerAttributes)parameters[(byte)FetchContainersResponseParameterCode.ContainerAttributes];
                    NatureComponents.Container container = new NatureComponents.Container(containerID, entityID, containerName, attributes);
                    scene.ContainerEnter(container);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("Fetch Containers Response Parameter Cast Error");
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
