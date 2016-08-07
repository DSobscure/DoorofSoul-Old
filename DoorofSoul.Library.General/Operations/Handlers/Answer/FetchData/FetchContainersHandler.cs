using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Answer;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers.Answer.FetchData
{
    internal class FetchContainersHandler : FetchDataHandler
    {
        internal FetchContainersHandler(General.Answer answer) : base(answer)
        {
        }

        internal override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 0)
            {
                debugMessage = string.Format("Answer Fetch Container Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        internal override bool Handle(AnswerFetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            if (base.Handle(fetchCode, parameter))
            {
                try
                {
                    foreach (General.Container container in answer.Containers)
                    {
                        var result = new Dictionary<byte, object>
                        {
                            { (byte)FetchContainersResponseParameterCode.ContainerID, container.ContainerID },
                            { (byte)FetchContainersResponseParameterCode.EntityID, container.EntityID },
                            { (byte)FetchContainersResponseParameterCode.ContainerName, container.ContainerName },
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
