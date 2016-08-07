using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers
{
    internal abstract class SceneEventHandler
    {
        protected General.Scene scene;

        protected SceneEventHandler(General.Scene scene)
        {
            this.scene = scene;
        }

        internal virtual bool Handle(SceneEventCode eventCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryInstance.ErrorFormat("Scene Event Parameter Error On {0} SceneID: {1} Debug Message: {2}", eventCode, scene.SceneID, debugMessage);
                return false;
            }
        }
        internal abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
