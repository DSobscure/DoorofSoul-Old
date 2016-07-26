using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers.Soul
{
    public abstract class FetchDataResponseHandler
    {
        protected General.Soul soul;

        protected FetchDataResponseHandler(General.Soul soul)
        {
            this.soul = soul;
        }

        public virtual bool Handle(SoulFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameter)
        {
            string debugMessage;
            if (CheckParameter(parameter, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryLog.ErrorFormat("Soul FetchData Parameter Error On {0} SoulID: {1} Debug Message: {2}", fetchCode, soul.SoulID, debugMessage);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
