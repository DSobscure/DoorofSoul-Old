using DoorofSoul.Protocol.Communication;
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

        public virtual bool Handle(ContainerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                SendError(operationCode, ErrorCode.ParameterError, debugMessage, null);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
        public void SendError(ContainerOperationCode operationCode, ErrorCode errorCode, string debugMessage, string errorMessage)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object> { { (byte)OperationErrorResponseParameterCode.ErrorMessage, errorMessage } };
            container.SendError(operationCode, errorCode, debugMessage, parameters);
            LibraryLog.ErrorFormat("Error On Soul Operation: {0}, ErrorCode:{1}, Debug Message: {2}", operationCode, errorCode, debugMessage);
        }
        public void SendResponse(ContainerOperationCode operationCode, Dictionary<byte, object> parameter)
        {
            container.SendResponse(operationCode, parameter);
        }
    }
}
