using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.World
{
    public abstract class InformDataHandler
    {
        protected General.World world;

        protected InformDataHandler(General.World world)
        {
            this.world = world;
        }

        public virtual bool Handle(WorldInformDataCode informCode, ErrorCode returnCode, Dictionary<byte, object> parameter)
        {
            string debugMessage;
            if (CheckParameter(parameter, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryLog.ErrorFormat("World InformData Parameter Error On {0} WorldID: {1} Debug Message: {2}", informCode, world.WorldID, debugMessage);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
