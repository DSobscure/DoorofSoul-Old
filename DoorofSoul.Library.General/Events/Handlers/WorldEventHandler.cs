using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers
{
    public abstract class WorldEventHandler
    {
        protected General.World world;
        protected int correctParameterCount;

        protected WorldEventHandler(General.World world, int correctParameterCount)
        {
            this.world = world;
            this.correctParameterCount = correctParameterCount;
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
                LibraryInstance.ErrorFormat("World Event Parameter Error On {0} WorldID: {1} Debug Message: {2}", eventCode, world.WorldID, debugMessage);
                return false;
            }
        }
        internal virtual bool CheckParameter(Dictionary<byte, object> parameters, out string debugMessage)
        {
            if (parameters.Count != correctParameterCount)
            {
                debugMessage = string.Format("Parameter Count: {0} Should be {1}", parameters.Count, correctParameterCount);
                return false;
            }
            else
            {
                debugMessage = "";
                return true;
            }
        }
    }
}
