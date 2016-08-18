using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers
{
    public abstract class SoulEventHandler
    {
        protected ThroneComponents.Soul soul;
        protected int correctParameterCount;

        protected SoulEventHandler(ThroneComponents.Soul soul, int correctParameterCount)
        {
            this.soul = soul;
            this.correctParameterCount = correctParameterCount;
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
