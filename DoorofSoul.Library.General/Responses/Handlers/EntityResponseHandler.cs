using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers
{
    public abstract class EntityResponseHandler
    {
        protected General.Entity entity;

        protected EntityResponseHandler(General.Entity entity)
        {
            this.entity = entity;
        }

        public virtual bool Handle(EntityOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryLog.ErrorFormat("Entity Response Parameter Error On {0} EntityID: {1} Debug Message: {2}", operationCode, entity.EntityID, debugMessage);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
