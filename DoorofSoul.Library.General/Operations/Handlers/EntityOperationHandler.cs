using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.ResponseParameters;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers
{
    public abstract class EntityOperationHandler
    {
        protected General.Entity entity;

        protected EntityOperationHandler(General.Entity entity)
        {
            this.entity = entity;
        }

        public virtual bool Handle(EntityOperationCode operationCode, Dictionary<byte, object> parameters)
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
        public void SendError(EntityOperationCode operationCode, ErrorCode errorCode, string debugMessage, string errorMessage)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object> { { (byte)OperationErrorResponseParameterCode.ErrorMessage, errorMessage } };
            entity.SendResponse(operationCode, errorCode, debugMessage, parameters);
            LibraryLog.ErrorFormat("Error On Soul Operation: {0}, ErrorCode:{1}, Debug Message: {2}", operationCode, errorCode, debugMessage);
        }
        public void SendResponse(EntityOperationCode operationCode, Dictionary<byte, object> parameter)
        {
            entity.SendResponse(operationCode, ErrorCode.NoError, null, parameter);
        }
    }
}
