using DoorofSoul.Library.General.NatureComponents.ContainerElements;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Answer;
using DoorofSoul.Protocol.Language;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers.Answer.FetchData
{
    internal class FetchContainersResponseHandler : FetchDataResponseHandler
    {
        internal FetchContainersResponseHandler(ThroneComponents.Answer answer) : base(answer)
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
                        answer.AnswerEventManager.ErrorInform(LauguageDictionarySelector.Instance[answer.UsingLanguage]["Unknown Error"], LauguageDictionarySelector.Instance[answer.UsingLanguage]["Fetch Container Error"]);
                        return false;
                    }
            }
        }

        internal override bool Handle(AnswerFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameters)
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
                    answer.LoadContainers(new List<NatureComponents.Container> { container });
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
