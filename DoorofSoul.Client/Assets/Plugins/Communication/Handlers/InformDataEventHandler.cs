using DoorofSoul.Protocol.Communication;
using System.Collections.Generic;

namespace DoorofSoul.Client.Communication.Handlers
{
    public abstract class InformDataEventHandler
    {
        public virtual bool Handle(InformDataCode informCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckError(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                Global.SystemManagers.DebugInformManager.DebugInform(string.Format("Inform Data Event Parameter Error On {0} DebugMessage: {1}", informCode, debugMessage));
                return false;
            }
        }
        public abstract bool CheckError(Dictionary<byte, object> parameters, out string debugMessage);
    }
}

