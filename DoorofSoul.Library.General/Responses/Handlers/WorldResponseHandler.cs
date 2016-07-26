using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers
{
    public abstract class WorldResponseHandler
    {
        protected General.World world;

        protected WorldResponseHandler(General.World world)
        {
            this.world = world;
        }

        public virtual bool Handle(WorldOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryLog.ErrorFormat("World Response Parameter Error On {0} WorldID: {1} Debug Message: {2}", operationCode, world.WorldID, debugMessage);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
