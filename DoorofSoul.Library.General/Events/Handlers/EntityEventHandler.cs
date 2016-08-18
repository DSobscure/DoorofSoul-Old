using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers
{
    public abstract class EntityEventHandler
    {
        protected General.Entity entity;
        protected int correctParameterCount;

        protected EntityEventHandler(General.Entity entity, int correctParameterCount)
        {
            this.entity = entity;
            this.correctParameterCount = correctParameterCount;
        }

        internal virtual bool Handle(EntityEventCode eventCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryInstance.ErrorFormat("Entity Event Parameter Error On {0} EntityID: {1} Debug Message: {2}", eventCode, entity.EntityID, debugMessage);
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
