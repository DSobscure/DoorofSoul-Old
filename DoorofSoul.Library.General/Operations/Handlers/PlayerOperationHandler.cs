using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.ResponseParameters;
using DoorofSoul.Protocol.Language;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers
{
    public abstract class PlayerOperationHandler
    {
        protected General.Player player;

        protected PlayerOperationHandler(General.Player player)
        {
            this.player = player;
        }

        public virtual bool Handle(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
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
        public void SendError(PlayerOperationCode operationCode, ErrorCode errorCode, string debugMessage)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>();
            player.SendResponse(operationCode, errorCode, debugMessage, parameters);
            LibraryLog.ErrorFormat("Error On Player Operation: {0}, ErrorCode:{1}, Debug Message: {2}", operationCode, errorCode, debugMessage);
        }
        public void SendResponse(PlayerOperationCode operationCode, Dictionary<byte, object> parameter)
        {
            player.SendResponse(operationCode, ErrorCode.NoError, null, parameter);
        }
    }
}
