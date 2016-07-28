using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers
{
    public abstract class SoulEventHandler
    {
        protected General.Soul soul;

        protected SoulEventHandler(General.Soul soul)
        {
            this.soul = soul;
        }

        public virtual bool Handle(SoulEventCode eventCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryLog.ErrorFormat("Soul Event Parameter Error On {0} SoulID: {1} Debug Message: {2}", eventCode, soul.SoulID, debugMessage);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
