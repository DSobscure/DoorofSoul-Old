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

        public virtual bool Handle(SceneOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryLog.ErrorFormat("Scene Response Parameter Error On {0} SceneID: {1} Debug Message: {2}", operationCode, scene.SceneID, debugMessage);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
