﻿using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers.World
{
    public abstract class FetchDataResponseHandler
    {
        protected NatureComponents.World world;

        protected FetchDataResponseHandler(NatureComponents.World world)
        {
            this.world = world;
        }

        public virtual bool Handle(WorldFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameters)
        {
            if (CheckError(parameters, returnCode, fetchDebugMessage))
            {
                return true;
            }
            else
            {
                LibraryInstance.ErrorFormat("World FetchData Parameter Error On {0} WorldID: {1} Debug Message: {2}", fetchCode, world.WorldID, fetchDebugMessage);
                return false;
            }
        }
        public abstract bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage);
    }
}