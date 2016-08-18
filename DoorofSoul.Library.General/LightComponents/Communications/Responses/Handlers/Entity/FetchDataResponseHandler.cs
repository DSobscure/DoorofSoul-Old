using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers.Entity
{
    public abstract class FetchDataResponseHandler
    {
        protected NatureComponents.Entity entity;

        protected FetchDataResponseHandler(NatureComponents.Entity entity)
        {
            this.entity = entity;
        }

        public virtual bool Handle(EntityFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameters)
        {
            if (CheckError(parameters, returnCode, fetchDebugMessage))
            {
                return true;
            }
            else
            {
                LibraryInstance.ErrorFormat("Entity FetchData Parameter Error On {0} EntityID: {1} Debug Message: {2}", fetchCode, entity.EntityID, fetchDebugMessage);
                return false;
            }
        }
        public abstract bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage);
    }
}
