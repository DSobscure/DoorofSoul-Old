using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Scene
{
    public abstract class InformDataHandler
    {
        protected General.Scene scene;

        protected InformDataHandler(General.Scene scene)
        {
            this.scene = scene;
        }

        public virtual bool Handle(SceneInformDataCode informCode, ErrorCode returnCode, Dictionary<byte, object> parameter)
        {
            string debugMessage;
            if (CheckParameter(parameter, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryLog.ErrorFormat("Scene InformData Parameter Error On {0} SceneID: {1} Debug Message: {2}", informCode, scene.SceneID, debugMessage);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
