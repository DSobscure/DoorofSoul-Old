﻿using DoorofSoul.Library.General.Events.Handlers;
using DoorofSoul.Library.General.Events.Handlers.Scene;
using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters;
using DoorofSoul.Protocol.Communication.EventParameters.World;
using DoorofSoul.Protocol.Communication.EventParameters.Scene;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Managers
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
                { SceneEventCode.EntityEnter, new EntityEnterHandler(scene) },
                { SceneEventCode.EntityExit, new EntityExitHandler(scene) },
                { SceneEventCode.BroadcastMessage, new BroadcastMessageHandler(scene) },
                { SceneEventCode.SynchronizeEntityPosition, new SynchronizeEntityPositionHandler(scene) },
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

        public void EntityEnter(Entity entity)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)EntityEnterParameterCode.EntityID, entity.EntityID },
                { (byte)EntityEnterParameterCode.EntityName, entity.EntityName },
                { (byte)EntityEnterParameterCode.EntitySpaceProperties, entity.SpaceProperties },
            };
            SendEvent(SceneEventCode.EntityEnter, parameters);
        }
        public void EntityExit(Entity entity)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)EntityExitParameterCode.EntityID, entity.EntityID }
            };
            SendEvent(SceneEventCode.EntityExit, parameters);
        }
        public void BroadcastMessage(MessageTypeCode messageType, MessageSourceTypeCode messageSourceType, string sourceName, string message)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)BroadcastMessageParameterCode.MessageType, (byte)messageType },
                { (byte)BroadcastMessageParameterCode.MessageSourceType, (byte)messageSourceType },
                { (byte)BroadcastMessageParameterCode.SourceName, sourceName },
                { (byte)BroadcastMessageParameterCode.Message, message }
            };
            SendEvent(SceneEventCode.BroadcastMessage, parameters);
        }
        public void SynchronizeEntityPosition(Entity entity)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)SynchronizeEntityPositionParameterCode.EntityID, entity.EntityID },
                { (byte)SynchronizeEntityPositionParameterCode.Position, entity.Position }
            };
            SendEvent(SceneEventCode.SynchronizeEntityPosition, parameters);
        }
    }
}
