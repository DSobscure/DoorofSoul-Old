using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers.Container
{
    internal abstract class FetchDataResponseHandler
    {
        protected NatureComponents.Container container;

        protected FetchDataResponseHandler(NatureComponents.Container container)
        {
            this.container = container;
        }

        internal virtual bool Handle(ContainerFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameters)
        {
            if (CheckError(parameters, returnCode, fetchDebugMessage))
            {
                return true;
            }
            else
            {
                LibraryInstance.ErrorFormat("Container FetchData Parameter Error On {0} ContainerID: {1} Debug Message: {2}", fetchCode, container.ContainerID, fetchDebugMessage);
                return false;
            }
        }
        internal abstract bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage);
    }
}
