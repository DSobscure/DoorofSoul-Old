using DoorofSoul.Library.General.Events.Handlers;
using DoorofSoul.Library.General.Events.Handlers.Entity;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters.Scene;
using DoorofSoul.Protocol.Communication.EventParameters.Entity;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Managers
{
    public class EntityEventManager
    {
        private readonly Dictionary<EntityEventCode, EntityEventHandler> eventTable;
        protected readonly Entity entity;

        internal EntityEventManager(Entity entity)
        {
            this.entity = entity;
            eventTable = new Dictionary<EntityEventCode, EntityEventHandler>
            {
                { EntityEventCode.InformData, new InformDataResolver(entity) },
                { EntityEventCode.StartRotate, new StartRotateHandler(entity) },
                { EntityEventCode.StartMove, new StartMoveHandler(entity) },
            };
        }

        internal void Operate(EntityEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (eventTable.ContainsKey(eventCode))
            {
                if (!eventTable[eventCode].Handle(eventCode, parameters))
                {
                    LibraryInstance.ErrorFormat("Entity Event Error: {0} from EntityID: {1}", eventCode, entity.EntityID);
                }
            }
            else
            {
                LibraryInstance.ErrorFormat("Unknow Entity Event:{0} from EntityID: {1}", eventCode, entity.EntityID);
            }
        }

        internal void SendEvent(EntityEventCode eventCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)EntityEventParameterCode.EntityID, entity.EntityID },
                { (byte)EntityEventParameterCode.EventCode, (byte)eventCode },
                { (byte)EntityEventParameterCode.Parameters, parameters }
            };
            entity.LocatedScene.SceneEventManager.SendEvent(SceneEventCode.EntityEvent, eventData);
        }

        public void ErrorInform(string title, string message)
        {
            entity.LocatedScene.SceneEventManager.ErrorInform(title, message);
        }
        public void StartRotate(float angularVelocity)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)StartRotateParameterCode.AngularVelocity, angularVelocity }
            };
            SendEvent(EntityEventCode.StartRotate, parameters);
        }
        public void StartMove(float velocity)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)StartMoveParameterCode.Velocity, velocity }
            };
            SendEvent(EntityEventCode.StartMove, parameters);
        }
    }
}
