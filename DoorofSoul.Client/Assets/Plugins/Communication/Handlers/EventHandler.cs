using ExitGames.Client.Photon;
using DoorofSoul.Protocol.Communication;

namespace DoorofSoul.Client.Handlers
{
    public abstract class EventHandler
    {

        // Use this for initialization
        public virtual bool Handle(EventData eventData)
        {
            if (CheckError(eventData))
            {
                return true;
            }
            else
            {
                Global.SystemManagers.DebugInformManager.DebugInform(string.Format("Event Error On {0}", (EventCode)eventData.Code));
                return false;
            }
        }
        public abstract bool CheckError(EventData eventData);
    }
}

