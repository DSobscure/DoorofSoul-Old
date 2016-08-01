using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.ResponseParameters;
using DoorofSoul.Protocol.Language;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers
{
    public abstract class WorldOperationHandler
    {
        protected General.World world;

        protected WorldOperationHandler(General.World world)
        {
            this.world = world;
        }

        public virtual bool Handle(WorldOperationCode operationCode, Dictionary<byte, object> parameters)
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
        public void SendError(WorldOperationCode operationCode, ErrorCode errorCode, string debugMessage)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>();
            world.WorldResponseManager.SendResponse(operationCode, errorCode, debugMessage, parameters);
            LibraryLog.ErrorFormat("Error On World Operation: {0}, ErrorCode:{1}, Debug Message: {2}", operationCode, errorCode, debugMessage);
        }
        public void SendResponse(WorldOperationCode operationCode, Dictionary<byte, object> parameter)
        {
            world.WorldResponseManager.SendResponse(operationCode, ErrorCode.NoError, null, parameter);
        }
    }
}
