using DoorofSoul.Library.General.Events.Handlers;
using DoorofSoul.Library.General.Events.Handlers.Scene;
using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;
namespace DoorofSoul.Library.General.Events.Managers
{
    public class SceneEventManager
    {
        protected readonly Dictionary<SceneEventCode, SceneEventHandler> eventTable;
        protected readonly Scene scene;

        public SceneEventManager(Scene scene)
        {
            this.scene = scene;
            eventTable = new Dictionary<SceneEventCode, SceneEventHandler>
            {
                { SceneEventCode.ContainerEvent, new ContainerEventResolver(scene) },
                { SceneEventCode.EntityEvent, new EntityEventResolver(scene) },
                { SceneEventCode.InformData, new InformDataResolver(scene) },
            };
        }

        public void Operate(SceneEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (eventTable.ContainsKey(eventCode))
            {
                if (!eventTable[eventCode].Handle(eventCode, parameters))
                {
                    LibraryLog.ErrorFormat("Scene Event Error: {0} from SceneID: {1}", eventCode, scene.SceneID);
                }
            }
            else
            {
                LibraryLog.ErrorFormat("Unknow Scene Event:{0} from SceneID: {1}", eventCode, scene.SceneID);
            }
        }
    }
}
