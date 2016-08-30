using DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Scene.InformData;
using DoorofSoul.Library.General.NatureComponents.SceneElements;
using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.InformDataParameters.Scene;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Scene
{
    public class InformDataResolver : SceneEventHandler
    {
        protected readonly Dictionary<SceneInformDataCode, InformDataHandler> informTable;

        internal InformDataResolver(NatureComponents.Scene scene) : base(scene, 2)
        {
            informTable = new Dictionary<SceneInformDataCode, InformDataHandler>
            {
                { SceneInformDataCode.EntityEnter, new InformEntityEnterHandler(scene) },
                { SceneInformDataCode.EntityExit, new InformEntityExitHandler(scene) },
                { SceneInformDataCode.ContainerEnter, new InformContainerEnterHandler(scene) },
                { SceneInformDataCode.ContainerExit, new InformContainerExitHandler(scene) },
                { SceneInformDataCode.BroadcastMessage, new InformBroadcastMessageHandler(scene) },
                { SceneInformDataCode.SynchronizeEntityPosition, new SynchronizeEntityPositionHandler(scene) },
                { SceneInformDataCode.ItemEntityChange, new InformItemEntityChangeHandler(scene) },
            };
        }

        internal override bool Handle(SceneEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(eventCode, parameters))
            {
                SceneInformDataCode informCode = (SceneInformDataCode)parameters[(byte)InformDataEventParameterCode.InformCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)InformDataEventParameterCode.Parameters];
                if (informTable.ContainsKey(informCode))
                {
                    return informTable[informCode].Handle(informCode, resolvedParameters);
                }
                else
                {
                    LibraryInstance.ErrorFormat("Scene InformData Event Not Exist Inform Code: {0}", informCode);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        internal void SendInform(SceneInformDataCode informCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> informDataParameters = new Dictionary<byte, object>
            {
                { (byte)InformDataEventParameterCode.InformCode, (byte)informCode },
                { (byte)InformDataEventParameterCode.Parameters, parameters }
            };
            scene.SceneEventManager.SendEvent(SceneEventCode.InformData, informDataParameters);
        }
        public void InformItemEntityChange(ItemEntity itemEntity, DataChangeTypeCode changeTypeCode)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)InformItemEntityChangeParameterCode.ItemEntityID, itemEntity.ItemEntityID },
                { (byte)InformItemEntityChangeParameterCode.ItemID, itemEntity.ItemID },
                { (byte)InformItemEntityChangeParameterCode.Position, itemEntity.Position },
                { (byte)InformItemEntityChangeParameterCode.DataChangeType, changeTypeCode },
            };
            SendInform(SceneInformDataCode.ItemEntityChange, parameters);
        }
        public void InformEntityEnter(NatureComponents.Entity entity)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)InformEntityEnterParameterCode.EntityID, entity.EntityID },
                { (byte)InformEntityEnterParameterCode.EntityName, entity.EntityName },
                { (byte)InformEntityEnterParameterCode.EntitySpaceProperties, entity.SpaceProperties },
            };
            SendInform(SceneInformDataCode.EntityEnter, parameters);
        }
        public void InformEntityExit(NatureComponents.Entity entity)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)InformEntityExitParameterCode.EntityID, entity.EntityID }
            };
            SendInform(SceneInformDataCode.EntityExit, parameters);
        }
        public void InformContainerEnter(NatureComponents.Container container)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)InformContainerEnterParameterCode.ContainerID, container.ContainerID },
                { (byte)InformContainerEnterParameterCode.EntityID, container.ContainerID },
                { (byte)InformContainerEnterParameterCode.ContainerName, container.ContainerName },
                { (byte)InformContainerEnterParameterCode.ContainerAttributes, container.Attributes },
            };
            SendInform(SceneInformDataCode.ContainerEnter, parameters);
        }
        public void InformContainerExit(NatureComponents.Container container)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)InformContainerExitParameterCode.ContainerID, container.ContainerID }
            };
            SendInform(SceneInformDataCode.ContainerExit, parameters);
        }
        public void SynchronizeEntityPosition(NatureComponents.Entity entity)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)SynchronizeEntityPositionParameterCode.EntityID, entity.EntityID },
                { (byte)SynchronizeEntityPositionParameterCode.Position, entity.Position }
            };
            SendInform(SceneInformDataCode.SynchronizeEntityPosition, parameters);
        }
        public void InformBroadcastMessage(MessageTypeCode messageType, MessageSourceTypeCode messageSourceType, string sourceName, string message)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)InformBroadcastMessageParameterCode.MessageType, (byte)messageType },
                { (byte)InformBroadcastMessageParameterCode.MessageSourceType, (byte)messageSourceType },
                { (byte)InformBroadcastMessageParameterCode.SourceName, sourceName },
                { (byte)InformBroadcastMessageParameterCode.Message, message }
            };
            SendInform(SceneInformDataCode.BroadcastMessage, parameters);
        }
    }
}
