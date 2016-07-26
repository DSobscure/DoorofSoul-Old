using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers
{
    public abstract class ContainerResponseHandler
    {
        protected General.Container container;

        protected ContainerResponseHandler(General.Container container)
        {
            this.container = container;
        }

        public virtual bool Handle(ContainerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryLog.ErrorFormat("Container Response Parameter Error On {0} ContainerID: {1} Debug Message: {2}", operationCode, container.ContainerID, debugMessage);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
