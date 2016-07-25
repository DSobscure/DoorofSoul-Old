using DoorofSoul.Library.General.Events.Handlers;
using DoorofSoul.Library.General.Events.Handlers.Container;
using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Managers
{
    public class ContainerEventManager
    {
        protected readonly Dictionary<ContainerEventCode, ContainerEventHandler> eventTable;
        protected readonly Container container;

        public ContainerEventManager(Container container)
        {
            this.container = container;
            eventTable = new Dictionary<ContainerEventCode, ContainerEventHandler>
            {
                { ContainerEventCode.InformData, new InformDataResolver(container) }
            };
        }

        public void Operate(ContainerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (eventTable.ContainsKey(eventCode))
            {
                if (!eventTable[eventCode].Handle(eventCode, parameters))
                {
                    LibraryLog.ErrorFormat("Container Event Error: {0} from ContainerID: {1}", eventCode, container.ContainerID);
                }
            }
            else
            {
                LibraryLog.ErrorFormat("Unknow Container Event:{0} from ContainerID: {1}", eventCode, container.ContainerID);
            }
        }
    }
}
