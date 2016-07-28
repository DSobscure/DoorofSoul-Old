using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers
{
    public abstract class EntityEventHandler
    {
        protected General.Entity entity;

        protected EntityEventHandler(General.Entity entity)
        {
            this.entity = entity;
        }

        public virtual bool Handle(EntityEventCode eventCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryLog.ErrorFormat("Entity Event Parameter Error On {0} EntityID: {1} Debug Message: {2}", eventCode, entity.EntityID, debugMessage);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
