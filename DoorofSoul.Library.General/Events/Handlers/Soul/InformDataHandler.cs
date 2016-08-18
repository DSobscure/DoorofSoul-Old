using DoorofSoul.Protocol.Communication.InformDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Soul
{
    internal abstract class InformDataHandler
    {
        protected General.Soul soul;
        protected int correctParameterCount;

        protected InformDataHandler(General.Soul soul, int correctParameterCount)
        {
            this.soul = soul;
            this.correctParameterCount = correctParameterCount;
        }

        internal virtual bool Handle(SoulInformDataCode informCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryInstance.ErrorFormat("Soul InformData Parameter Error On {0} SoulID: {1} Debug Message: {2}", informCode, soul.SoulID, debugMessage);
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
