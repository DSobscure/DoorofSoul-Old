using DoorofSoul.Library.General.Events.Handlers;
using DoorofSoul.Library.General.Events.Handlers.Container;
using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;
using DoorofSoul.Protocol.Communication.Channels;

namespace DoorofSoul.Library.General.Events.Managers
{
    public class ContainerEventManager
    {
        private readonly Dictionary<ContainerEventCode, ContainerEventHandler> eventTable;
        protected readonly Container container;

        internal ContainerEventManager(Container container)
        {
            this.container = container;
            eventTable = new Dictionary<ContainerEventCode, ContainerEventHandler>
            {
                { ContainerEventCode.InformData, new InformDataResolver(container) },
                { ContainerEventCode.ObserveSceneEntitiesPosition, new ObserveSceneEntitiesPositionHandler(container) },
            };
        }

        internal void Operate(ContainerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (eventTable.ContainsKey(eventCode))
            {
                if (!eventTable[eventCode].Handle(eventCode, parameters))
                {
                    LibraryInstance.ErrorFormat("Container Event Error: {0} from ContainerID: {1}", eventCode, container.ContainerID);
                }
            }
            else
            {
                LibraryInstance.ErrorFormat("Unknow Container Event:{0} from ContainerID: {1}", eventCode, container.ContainerID);
            }
        }

        internal void SendEvent(ContainerEventCode eventCode, Dictionary<byte, object> parameters, ContainerCommunicationChannel channel)
        {
            switch (channel)
            {
                case ContainerCommunicationChannel.Answer:
                    {
                        Dictionary<byte, object> eventData = new Dictionary<byte, object>
                        {
                            { (byte)Protocol.Communication.EventParameters.Answer.ContainerEventParameterCode.ContainerID, container.ContainerID },
                            { (byte)Protocol.Communication.EventParameters.Answer.ContainerEventParameterCode.EventCode, (byte)eventCode },
                            { (byte)Protocol.Communication.EventParameters.Answer.ContainerEventParameterCode.Parameters, parameters }
                        };
                        if (!container.IsEmptyContainer)
                        {
                            container.FirstSoul.Answer.AnswerEventManager.SendEvent(AnswerEventCode.ContainerEvent, eventData);
                        }
                        else
                        {
                            LibraryInstance.ErrorFormat("Not Exist Soul for Container Communication, ContainerID: {0}", container.ContainerID);
                        }
                    }
                    break;
                case ContainerCommunicationChannel.Scene:
                    {
                        Dictionary<byte, object> eventData = new Dictionary<byte, object>
                        {
                            { (byte)Protocol.Communication.EventParameters.Scene.ContainerEventParameterCode.ContainerID, container.ContainerID },
                            { (byte)Protocol.Communication.EventParameters.Scene.ContainerEventParameterCode.EventCode, (byte)eventCode },
                            { (byte)Protocol.Communication.EventParameters.Scene.ContainerEventParameterCode.Parameters, parameters }
                        };
                        container.Entity.LocatedScene.SceneEventManager.SendEvent(SceneEventCode.ContainerEvent, eventData);
                    }
                    break;
                default:
                    LibraryInstance.ErrorFormat("Not Exist Channel for Container Communication, Channel: {0}", channel);
                    break;
            }
        }
        public void ErrorInform(string title, string message, ContainerCommunicationChannel channel)
        {
            switch (channel)
            {
                case ContainerCommunicationChannel.Answer:
                    {
                        if (!container.IsEmptyContainer)
                        {
                            container.FirstSoul.Answer.AnswerEventManager.ErrorInform(title, message);
                        }
                        else
                        {
                            LibraryInstance.ErrorFormat("Not Exist Soul for Container Communication, ContainerID: {0}", container.ContainerID);
                        }
                    }
                    break;
                case ContainerCommunicationChannel.Scene:
                    {
                        container.Entity.LocatedScene.SceneEventManager.ErrorInform(title, message);
                    }
                    break;
                default:
                    LibraryInstance.ErrorFormat("Not Exist Channel for Container Communication, Channel: {0}", channel);
                    break;
            }
        }
        public void ObserveSceneEntitiesPosition()
        {
            SendEvent(ContainerEventCode.ObserveSceneEntitiesPosition, new Dictionary<byte, object>(), ContainerCommunicationChannel.Answer);
        }
    }
}
