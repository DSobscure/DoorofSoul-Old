using DoorofSoul.Protocol.Communication.InformDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Entity
{
    public abstract class InformDataHandler
    {
        protected General.Entity entity;
        protected int correctParameterCount;

        protected InformDataHandler(General.Entity entity, int correctParameterCount)
        {
            this.entity = entity;
            this.correctParameterCount = correctParameterCount;
        }

        public virtual bool Handle(EntityInformDataCode informCode, Dictionary<byte, object> parameter)
        {
            string debugMessage;
            if (CheckParameter(parameter, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryInstance.ErrorFormat("Entity InformData Parameter Error On {0} EntityID: {1} Debug Message: {2}", informCode, entity.EntityID, debugMessage);
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
