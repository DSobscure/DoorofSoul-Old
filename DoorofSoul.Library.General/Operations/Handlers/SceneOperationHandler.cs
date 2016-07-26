using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.ResponseParameters;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers
{
    public abstract class SceneOperationHandler
    {
        protected General.Scene scene;

        protected SceneOperationHandler(General.Scene scene)
        {
            this.scene = scene;
        }

        public virtual bool Handle(SceneOperationCode operationCode, Dictionary<byte, object> parameters)
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
        public void SendError(SceneOperationCode operationCode, ErrorCode errorCode, string debugMessage, string errorMessage)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object> { { (byte)OperationErrorResponseParameterCode.ErrorMessage, errorMessage } };
            scene.SendResponse(operationCode, errorCode, debugMessage, parameters);
            LibraryLog.ErrorFormat("Error On Scene Operation: {0}, ErrorCode:{1}, Debug Message: {2}", operationCode, errorCode, debugMessage);
        }
        public void SendResponse(SceneOperationCode operationCode, Dictionary<byte, object> parameter)
        {
            scene.SendResponse(operationCode, ErrorCode.NoError, null, parameter);
        }
    }
}
