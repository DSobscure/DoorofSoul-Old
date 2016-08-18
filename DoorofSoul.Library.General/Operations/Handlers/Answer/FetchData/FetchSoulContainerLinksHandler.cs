using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Answer;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers.Answer.FetchData
{
    internal class FetchSoulContainerLinksHandler : FetchDataHandler
    {
        internal FetchSoulContainerLinksHandler(General.Answer answer) : base(answer, 0)
        {
        }

        internal override bool Handle(AnswerFetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            if (base.Handle(fetchCode, parameter))
            {
                try
                {
                    foreach (General.Soul soul in answer.Souls)
                    {
                        foreach (General.Container container in soul.Containers)
                        {
                            var result = new Dictionary<byte, object>
                            {
                                { (byte)FetchSoulContainerLinksResponseParameterCode.SoulID, soul.SoulID },
                                { (byte)FetchSoulContainerLinksResponseParameterCode.ContainerID, container.ContainerID }
                            };
                            SendResponse(fetchCode, result);
                        }
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("Fetch Soul Container Links Invalid Cast!");
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
