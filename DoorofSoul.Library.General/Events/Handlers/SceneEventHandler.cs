using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers
{
    public abstract class SceneEventHandler
    {
        protected General.Scene scene;

        protected SceneEventHandler(General.Scene scene)
        {
            this.scene = scene;
        }

        public virtual bool Handle(SceneEventCode eventCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryLog.ErrorFormat("Scene Event Parameter Error On {0} SceneID: {1} Debug Message: {2}", eventCode, scene.SceneID, debugMessage);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
