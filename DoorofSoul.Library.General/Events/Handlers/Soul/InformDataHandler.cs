using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Soul
{
    public abstract class InformDataHandler
    {
        protected General.Soul soul;

        protected InformDataHandler(General.Soul soul)
        {
            this.soul = soul;
        }

        public virtual bool Handle(SoulInformDataCode informCode, ErrorCode returnCode, Dictionary<byte, object> parameter)
        {
            string debugMessage;
            if (CheckParameter(parameter, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryLog.ErrorFormat("Soul InformData Parameter Error On {0} SoulID: {1} Debug Message: {2}", informCode, soul.SoulID, debugMessage);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
