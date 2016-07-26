using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers.World
{
    public abstract class FetchDataResponseHandler
    {
        protected General.World world;

        protected FetchDataResponseHandler(General.World world)
        {
            this.world = world;
        }

        public virtual bool Handle(WorldFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameter)
        {
            string debugMessage;
            if (CheckParameter(parameter, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryLog.ErrorFormat("World FetchData Parameter Error On {0} WorldID: {1} Debug Message: {2}", fetchCode, world.WorldID, debugMessage);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
