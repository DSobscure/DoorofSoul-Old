using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers
{
    internal abstract class WorldEventHandler
    {
        protected General.World world;

        protected WorldEventHandler(General.World world)
        {
            this.world = world;
        }

        internal virtual bool Handle(WorldEventCode eventCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryLog.ErrorFormat("World Event Parameter Error On {0} WorldID: {1} Debug Message: {2}", eventCode, world.WorldID, debugMessage);
                return false;
            }
        }
        internal abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
