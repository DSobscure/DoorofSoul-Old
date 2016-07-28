using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Entity
{
    public abstract class InformDataHandler
    {
        protected General.Entity entity;

        protected InformDataHandler(General.Entity entity)
        {
            this.entity = entity;
        }

        public virtual bool Handle(EntityInformDataCode informCode, ErrorCode returnCode, Dictionary<byte, object> parameter)
        {
            string debugMessage;
            if (CheckParameter(parameter, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryLog.ErrorFormat("Entity InformData Parameter Error On {0} EntityID: {1} Debug Message: {2}", informCode, entity.EntityID, debugMessage);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
