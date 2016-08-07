using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.ResponseParameters;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers
{
    public abstract class SoulOperationHandler
    {
        protected General.Soul soul;

        protected SoulOperationHandler(General.Soul soul)
        {
            this.soul = soul;
        }

        public virtual bool Handle(SoulOperationCode operationCode, Dictionary<byte, object> parameters)
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
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
        public void SendError(SoulOperationCode operationCode, ErrorCode errorCode, string debugMessage)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>();
            soul.SoulResponseManager.SendResponse(operationCode, errorCode, debugMessage, parameters);
            LibraryInstance.ErrorFormat("Error On Soul Operation: {0}, ErrorCode:{1}, Debug Message: {2}", operationCode, errorCode, debugMessage);
        }
        public void SendResponse(SoulOperationCode operationCode, Dictionary<byte, object> parameter)
        {
            soul.SoulResponseManager.SendResponse(operationCode, ErrorCode.NoError, null, parameter);
        }
    }
}
