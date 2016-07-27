﻿using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers.Scene
{
    public abstract class FetchDataResponseHandler
    {
        protected General.Scene scene;

        protected FetchDataResponseHandler(General.Scene scene)
        {
            this.scene = scene;
        }

        public virtual bool Handle(SceneFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameters)
        {
            if (CheckError(parameters, returnCode, fetchDebugMessage))
            {
                return true;
            }
            else
            {
                LibraryLog.ErrorFormat("Scene FetchData Parameter Error On {0} SceneID: {1} Debug Message: {2}", fetchCode, scene.SceneID, fetchDebugMessage);
                return false;
            }
        }
        public abstract bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage);
    }
}
