using DoorofSoul.Library.General.Events.Handlers;
using DoorofSoul.Library.General.Events.Handlers.Entity;
using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;
namespace DoorofSoul.Library.General.Events.Managers
{
    public class EntityEventManager
    {
        protected readonly Dictionary<EntityEventCode, EntityEventHandler> eventTable;
        protected readonly Entity entity;

        public EntityEventManager(Entity entity)
        {
            this.entity = entity;
            eventTable = new Dictionary<EntityEventCode, EntityEventHandler>
            {
                { EntityEventCode.InformData, new InformDataResolver(entity) }
            };
        }

        public void Operate(EntityEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (eventTable.ContainsKey(eventCode))
            {
                if (!eventTable[eventCode].Handle(eventCode, parameters))
                {
                    LibraryLog.ErrorFormat("Entity Event Error: {0} from EntityID: {1}", eventCode, entity.EntityID);
                }
            }
            else
            {
                LibraryLog.ErrorFormat("Unknow Entity Event:{0} from EntityID: {1}", eventCode, entity.EntityID);
            }
        }
    }
}
