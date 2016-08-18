using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers
{
    internal abstract class SceneResponseHandler
    {
        protected NatureComponents.Scene scene;

        protected SceneResponseHandler(NatureComponents.Scene scene)
        {
            this.scene = scene;
        }

        internal virtual bool Handle(SceneOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
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
        internal abstract bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage);
    }
}
