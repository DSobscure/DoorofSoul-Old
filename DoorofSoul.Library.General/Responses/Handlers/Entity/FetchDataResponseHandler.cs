using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers.Entity
{
    public abstract class FetchDataResponseHandler
    {
        protected General.Entity entity;

        protected FetchDataResponseHandler(General.Entity entity)
        {
            this.entity = entity;
        }

        public virtual bool Handle(EntityFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameter)
        {
            string debugMessage;
            if (CheckParameter(parameter, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryLog.ErrorFormat("Entity FetchData Parameter Error On {0} EntityID: {1} Debug Message: {2}", fetchCode, entity.EntityID, debugMessage);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
