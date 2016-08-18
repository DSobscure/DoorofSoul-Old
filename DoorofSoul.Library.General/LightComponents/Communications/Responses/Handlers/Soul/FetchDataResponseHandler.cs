using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers.Soul
{
    public abstract class FetchDataResponseHandler
    {
        protected ThroneComponents.Soul soul;

        protected FetchDataResponseHandler(ThroneComponents.Soul soul)
        {
            this.soul = soul;
        }

        internal virtual bool Handle(SoulFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameters)
        {
            if (CheckError(parameters, returnCode, fetchDebugMessage))
            {
                return true;
            }
            else
            {
                LibraryInstance.ErrorFormat("Soul FetchData Parameter Error On {0} SoulID: {1} Debug Message: {2}", fetchCode, soul.SoulID, fetchDebugMessage);
                return false;
            }
        }
        internal abstract bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage);
    }
}
