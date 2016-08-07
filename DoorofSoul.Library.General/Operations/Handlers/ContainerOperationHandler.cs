using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.Channels;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.ResponseParameters;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers
{
    public abstract class ContainerOperationHandler
    {
        protected General.Container container;

        protected ContainerOperationHandler(General.Container container)
        {
            this.container = container;
        }

        internal virtual bool Handle(ContainerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                SendError(operationCode, ErrorCode.ParameterError, debugMessage);
                return false;
            }
        }
        internal abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
        internal void SendError(ContainerOperationCode operationCode, ErrorCode errorCode, string debugMessage)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>();
            container.ContainerResponseManager.SendResponse(operationCode, errorCode, debugMessage, parameters, ContainerCommunicationChannel.Answer);
            LibraryInstance.ErrorFormat("Error On Soul Operation: {0}, ErrorCode:{1}, Debug Message: {2}", operationCode, errorCode, debugMessage);
        }
        internal void SendResponse(ContainerOperationCode operationCode, Dictionary<byte, object> parameter)
        {
            container.ContainerResponseManager.SendResponse(operationCode, ErrorCode.NoError, null, parameter, ContainerCommunicationChannel.Answer);
        }
    }
}
