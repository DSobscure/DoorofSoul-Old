using DoorofSoul.Library.General.Events.Handlers;
using DoorofSoul.Library.General.Events.Handlers.World;
using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Managers
{
    public class WorldEventManager
    {
        private readonly Dictionary<WorldEventCode, WorldEventHandler> eventTable;
        protected readonly World world;

        internal WorldEventManager(World world)
        {
            this.world = world;
            eventTable = new Dictionary<WorldEventCode, WorldEventHandler>
            {
                { WorldEventCode.SceneEvent, new SceneEventResolver(world) },
                { WorldEventCode.InformData, new InformDataResolver(world) },
            };
        }

        public void Operate(WorldEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (eventTable.ContainsKey(eventCode))
            {
                if (!eventTable[eventCode].Handle(eventCode, parameters))
                {
                    LibraryLog.ErrorFormat("World Event Error: {0} from AnswerID: {1}", eventCode, world.WorldID);
                }
            }
            else
            {
                LibraryLog.ErrorFormat("Unknow World Event:{0} from AnswerID: {1}", eventCode, world.WorldID);
            }
        }

        internal void SendEvent(WorldEventCode eventCode, Dictionary<byte, object> parameters)
        {
            world.WorldCommunicationInterface.SendEvent(eventCode, parameters);
        }
        internal void SendSceneEvent(Scene scene, WorldEventCode eventCode, Dictionary<byte, object> parameters)
        {
            world.WorldCommunicationInterface.SendSceneEvent(scene, eventCode, parameters);
        }

        public void ErrorInform(string title, string message)
        {
            world.WorldCommunicationInterface.ErrorInform(title, message);
        }
    }
}
