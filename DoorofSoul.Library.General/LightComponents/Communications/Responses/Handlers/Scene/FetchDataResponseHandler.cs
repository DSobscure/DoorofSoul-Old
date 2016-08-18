using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers.Scene
{
    internal abstract class FetchDataResponseHandler
    {
        protected NatureComponents.Scene scene;

        protected FetchDataResponseHandler(NatureComponents.Scene scene)
        {
            this.scene = scene;
        }

        internal virtual bool Handle(SceneFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameters)
        {
            if (CheckError(parameters, returnCode, fetchDebugMessage))
            {
                return true;
            }
            else
            {
                LibraryInstance.ErrorFormat("Scene FetchData Parameter Error On {0} SceneID: {1} Debug Message: {2}", fetchCode, scene.SceneID, fetchDebugMessage);
                return false;
            }
        }
        internal abstract bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage);
    }
}
