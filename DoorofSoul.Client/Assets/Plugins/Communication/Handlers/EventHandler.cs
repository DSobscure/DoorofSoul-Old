using ExitGames.Client.Photon;
using DoorofSoul.Protocol.Communication;

namespace DoorofSoul.Client.Communication.Handlers
{
    public abstract class EventHandler
    {
        public virtual bool Handle(EventData eventData)
        {
            string debugMessage;
            if (CheckParameter(eventData, out debugMessage))
            {
                return true;
            }
            else
            {
                Global.SystemManagers.DebugInformManager.DebugInform(string.Format("Event Parameter Error On {0} Debug Message: {1}", (EventCode)eventData.Code, debugMessage));
                return false;
            }
        }
        public abstract bool CheckParameter(EventData eventData, out string debugMessage);
    }
}

