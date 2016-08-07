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

        internal virtual bool Handle(EntityOperationCode operationCode, Dictionary<byte, object> parameters)
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
        internal void SendError(EntityOperationCode operationCode, ErrorCode errorCode, string debugMessage)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>();
            entity.EntityResponseManager.SendResponse(operationCode, errorCode, debugMessage, parameters);
            LibraryInstance.ErrorFormat("Error On Soul Operation: {0}, ErrorCode:{1}, Debug Message: {2}", operationCode, errorCode, debugMessage);
        }
        internal void SendResponse(EntityOperationCode operationCode, Dictionary<byte, object> parameter)
        {
            entity.EntityResponseManager.SendResponse(operationCode, ErrorCode.NoError, null, parameter);
        }
    }
}
