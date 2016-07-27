using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers
{
    public abstract class SceneResponseHandler
    {
        protected General.Scene scene;

        protected SceneResponseHandler(General.Scene scene)
        {
            this.scene = scene;
        }

        public virtual bool Handle(SceneOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (CheckError(parameters, returnCode, debugMessage))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public abstract bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage);
    }
}
