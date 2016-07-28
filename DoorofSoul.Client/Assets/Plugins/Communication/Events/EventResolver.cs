using DoorofSoul.Client.Communication.Events.Handlers;
using DoorofSoul.Protocol.Communication.EventCodes;
using ExitGames.Client.Photon;
using System.Collections.Generic;

namespace DoorofSoul.Client.Communication.Events
{
    public class EventResolver
    {
        protected Dictionary<EventCode, EventHandler> eventTable;
        protected readonly PhotonService photonService;

        public EventResolver(PhotonService photonService)
        {
            this.photonService = photonService;
            eventTable = new Dictionary<EventCode, EventHandler>
            {
                { EventCode.PlayerEvent, new PlayerEventResolver(photonService) },
                { EventCode.WorldEvent, new WorldEventResolver(photonService) }
            };
        }

        public void Operate(EventData eventData)
        {
            EventCode eventCode = (EventCode)eventData.Code;
            if (eventTable.ContainsKey(eventCode))
            {
                if (!eventTable[eventCode].Handle(eventCode, eventData.Parameters))
                {
                    photonService.DebugReturn(DebugLevel.ERROR, string.Format("Event Error EventCode: {0}", eventCode));
                }
            }
            else
            {
                photonService.DebugReturn(DebugLevel.ERROR, string.Format("Unknow Event EventCode: {0}", eventCode));
            }
        }
    }
}
