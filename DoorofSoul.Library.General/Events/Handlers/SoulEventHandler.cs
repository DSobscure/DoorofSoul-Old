using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers
{
    internal abstract class SoulEventHandler
    {
        protected General.Soul soul;

        protected SoulEventHandler(General.Soul soul)
        {
            this.soul = soul;
        }

        internal virtual bool Handle(SoulEventCode eventCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryInstance.ErrorFormat("Soul Event Parameter Error On {0} SoulID: {1} Debug Message: {2}", eventCode, soul.SoulID, debugMessage);
                return false;
            }
        }
        internal abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
