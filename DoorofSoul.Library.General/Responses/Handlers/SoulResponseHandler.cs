using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers
{
    public abstract class SoulResponseHandler
    {
        protected General.Soul soul;

        protected SoulResponseHandler(General.Soul soul)
        {
            this.soul = soul;
        }

        public virtual bool Handle(SoulOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryLog.ErrorFormat("Soul Response Parameter Error On {0} SoulID: {1} Debug Message: {2}", operationCode, soul.SoulID, debugMessage);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
