using DoorofSoul.Library.General.Events.Handlers;
using DoorofSoul.Library.General.Events.Handlers.Soul;
using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Managers
{
    public class SoulEventManager
    {
        protected readonly Dictionary<SoulEventCode, SoulEventHandler> eventTable;
        protected readonly Soul soul;

        public SoulEventManager(Soul soul)
        {
            this.soul = soul;
            eventTable = new Dictionary<SoulEventCode, SoulEventHandler>
            {
                { SoulEventCode.InformData, new InformDataResolver(soul) },
            };
        }

        public void Operate(SoulEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (eventTable.ContainsKey(eventCode))
            {
                if (!eventTable[eventCode].Handle(eventCode, parameters))
                {
                    LibraryLog.ErrorFormat("Soul Event Error: {0} from SoulID: {1}", eventCode, soul.SoulID);
                }
            }
            else
            {
                LibraryLog.ErrorFormat("Unknow Soul Event:{0} from SoulID: {1}", eventCode, soul.SoulID);
            }
        }
    }
}
