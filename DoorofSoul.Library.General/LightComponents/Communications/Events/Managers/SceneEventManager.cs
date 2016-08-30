using DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers;
using DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Scene;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters.Scene;
using DoorofSoul.Protocol.Communication.EventParameters.World;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Managers
{
    public class SceneEventManager
    {
        private readonly Dictionary<SceneEventCode, SceneEventHandler> eventTable;
        protected readonly Scene scene;
        public InformDataResolver InformDataResolver { get; protected set; }

        internal SceneEventManager(Scene scene)
        {
            this.scene = scene;
            InformDataResolver = new InformDataResolver(scene);
            eventTable = new Dictionary<SceneEventCode, SceneEventHandler>
            {
                { SceneEventCode.ContainerEvent, new ContainerEventResolver(scene) },
                { SceneEventCode.EntityEvent, new EntityEventResolver(scene) },
                { SceneEventCode.InformData, InformDataResolver },
            };
        }

        internal void Operate(SceneEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (eventTable.ContainsKey(eventCode))
            {
                if (!eventTable[eventCode].Handle(eventCode, parameters))
                {
                    LibraryInstance.ErrorFormat("Scene Event Error: {0} from SceneID: {1}", eventCode, scene.SceneID);
                }
            }
            else
            {
                LibraryInstance.ErrorFormat("Unknow Scene Event:{0} from SceneID: {1}", eventCode, scene.SceneID);
            }
        }
        internal void SendEvent(SceneEventCode eventCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)SceneEventParameterCode.SceneID, scene.SceneID },
                { (byte)SceneEventParameterCode.EventCode, (byte)eventCode },
                { (byte)SceneEventParameterCode.Parameters, parameters }
            };
            scene.World.WorldEventManager.SendSceneEvent(scene, WorldEventCode.SceneEvent, eventData);
        }

        public void ErrorInform(string title, string message)
        {
            scene.World.WorldEventManager.ErrorInform(title, message);
        }
    }
}
