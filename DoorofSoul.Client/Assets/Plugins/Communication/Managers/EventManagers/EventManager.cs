using System.Collections.Generic;
using DoorofSoul.Protocol.Communication;
using ExitGames.Client.Photon;
using DoorofSoul.Client.Communication.Handlers;

namespace DoorofSoul.Client.Communication.Managers.EventManagers
{
    public class EventManager
    {
        protected readonly Dictionary<EventCode, EventHandler> eventTable;

        public EventManager()
        {
            eventTable = new Dictionary<EventCode, EventHandler>
            {
                { EventCode.InformData, Global.EventManagers.InformDataEventManager }
            };
        }

        public void Operate(EventData eventData)
        {
            EventCode eventCode = (EventCode)eventData.Code;
            if (eventTable.ContainsKey(eventCode))
            {
                eventTable[eventCode].Handle(eventData);
            }
            else
            {
                Global.SystemManagers.DebugInformManager.DebugInform(string.Format("Unknow Event: {0}", eventCode));
            }
        }
    }
}
