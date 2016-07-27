using DoorofSoul.Library.General.Events.Managers;
using DoorofSoul.Library.General.Operations.Managers;
using DoorofSoul.Library.General.Responses.Managers;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.Channels;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;
using System.Linq;
using DoorofSoul.Protocol.Language;

namespace DoorofSoul.Library.General
{
    public class Container
    {
        #region properties
        public int ContainerID { get; protected set; }
        public int EntityID { get; protected set; }
        protected Dictionary<int, Soul> soulDictionary;
        public IEnumerable<Soul> Souls { get { return soulDictionary.Values; } }
        public bool IsEmptyContainer { get { return soulDictionary.Count == 0; } }
        public Entity Entity { get; protected set; }
        public SupportLauguages UsingLanguage { get { return soulDictionary.FirstOrDefault().Value.UsingLanguage; } }
        #endregion

        #region communication
        public ContainerEventManager ContainerEventManager { get; protected set; }
        public ContainerOperationManager ContainerOperationManager { get; protected set; }
        public ContainerResponseManager ContainerResponseManager { get; protected set; }
        public void SendEvent(ContainerEventCode eventCode, Dictionary<byte, object> parameters, ContainerCommunicationChannel channel)
        {
            switch(channel)
            {
                case ContainerCommunicationChannel.Answer:
                    {
                        Dictionary<byte, object> eventData = new Dictionary<byte, object>
                        {
                            { (byte)Protocol.Communication.EventParameters.Answer.ContainerEventParameterCode.ContainerID, ContainerID },
                            { (byte)Protocol.Communication.EventParameters.Answer.ContainerEventParameterCode.EventCode, (byte)eventCode },
                            { (byte)Protocol.Communication.EventParameters.Answer.ContainerEventParameterCode.Parameters, parameters }
                        };
                        if (soulDictionary.Count > 0)
                        {
                            soulDictionary.First().Value.Answer.SendEvent(AnswerEventCode.ContainerEvent, eventData);
                        }
                        else
                        {
                            LibraryLog.ErrorFormat("Not Exist Soul for Container Communication, ContainerID: {0}", ContainerID);
                        }
                    }
                    break;
                case ContainerCommunicationChannel.Scene:
                    {
                        Dictionary<byte, object> eventData = new Dictionary<byte, object>
                        {
                            { (byte)Protocol.Communication.EventParameters.Scene.ContainerEventParameterCode.ContainerID, ContainerID },
                            { (byte)Protocol.Communication.EventParameters.Scene.ContainerEventParameterCode.EventCode, (byte)eventCode },
                            { (byte)Protocol.Communication.EventParameters.Scene.ContainerEventParameterCode.Parameters, parameters }
                        };
                        Entity.LocatedScene.SendEvent(SceneEventCode.ContainerEvent, eventData);
                    }
                    break;
                default:
                    LibraryLog.ErrorFormat("Not Exist Channel for Container Communication, Channel: {0}", channel);
                    break;
            }
        }
        public void SendOperation(AnswerOperationCode operationCode, Dictionary<byte, object> parameters, ContainerCommunicationChannel channel)
        {
            switch (channel)
            {
                case ContainerCommunicationChannel.Answer:
                    {
                        Dictionary<byte, object> operationData = new Dictionary<byte, object>
                        {
                            { (byte)Protocol.Communication.OperationParameters.Answer.ContainerOperationParameterCode.ContainerID, ContainerID },
                            { (byte)Protocol.Communication.OperationParameters.Answer.ContainerOperationParameterCode.OperationCode, (byte)operationCode },
                            { (byte)Protocol.Communication.OperationParameters.Answer.ContainerOperationParameterCode.Parameters, parameters }
                        };
                        if (soulDictionary.Count > 0)
                        {
                            soulDictionary.First().Value.Answer.SendOperation(AnswerOperationCode.ContainerOperation, operationData);
                        }
                        else
                        {
                            LibraryLog.ErrorFormat("Not Exist Soul for Container Communication, ContainerID: {0}", ContainerID);
                        }
                    }
                    break;
                case ContainerCommunicationChannel.Scene:
                    {
                        Dictionary<byte, object> operationData = new Dictionary<byte, object>
                        {
                            { (byte)Protocol.Communication.OperationParameters.Scene.ContainerOperationParameterCode.ContainerID, ContainerID },
                            { (byte)Protocol.Communication.OperationParameters.Scene.ContainerOperationParameterCode.OperationCode, (byte)operationCode },
                            { (byte)Protocol.Communication.OperationParameters.Scene.ContainerOperationParameterCode.Parameters, parameters }
                        };
                        Entity.LocatedScene.SendOperation(SceneOperationCode.ContainerOperation, operationData);
                    }
                    break;
                default:
                    LibraryLog.ErrorFormat("Not Exist Channel for Container Communication, Channel: {0}", channel);
                    break;
            }
        }
        public void SendResponse(ContainerOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters, ContainerCommunicationChannel channel)
        {
            switch (channel)
            {
                case ContainerCommunicationChannel.Answer:
                    {
                        Dictionary<byte, object> operationData = new Dictionary<byte, object>
                        {
                            { (byte)Protocol.Communication.ResponseParameters.Answer.ContainerResponseParameterCode.ContainerID, ContainerID },
                            { (byte)Protocol.Communication.ResponseParameters.Answer.ContainerResponseParameterCode.OperationCode, (byte)operationCode },
                            { (byte)Protocol.Communication.ResponseParameters.Answer.ContainerResponseParameterCode.ReturnCode, (short)errorCode },
                            { (byte)Protocol.Communication.ResponseParameters.Answer.ContainerResponseParameterCode.DebugMessage, debugMessage },
                            { (byte)Protocol.Communication.ResponseParameters.Answer.ContainerResponseParameterCode.Parameters, parameters }
                        };
                        if (soulDictionary.Count > 0)
                        {
                            soulDictionary.First().Value.Answer.SendResponse(AnswerOperationCode.ContainerOperation, ErrorCode.NoError, null, operationData);
                        }
                        else
                        {
                            LibraryLog.ErrorFormat("Not Exist Soul for Container Communication, ContainerID: {0}", ContainerID);
                        }
                    }
                    break;
                case ContainerCommunicationChannel.Scene:
                    {
                        Dictionary<byte, object> operationData = new Dictionary<byte, object>
                        {
                            { (byte)Protocol.Communication.ResponseParameters.Scene.ContainerResponseParameterCode.ContainerID, ContainerID },
                            { (byte)Protocol.Communication.ResponseParameters.Scene.ContainerResponseParameterCode.OperationCode, (byte)operationCode },
                            { (byte)Protocol.Communication.ResponseParameters.Scene.ContainerResponseParameterCode.ReturnCode, (short)errorCode },
                            { (byte)Protocol.Communication.ResponseParameters.Scene.ContainerResponseParameterCode.DebugMessage, debugMessage },
                            { (byte)Protocol.Communication.ResponseParameters.Scene.ContainerResponseParameterCode.Parameters, parameters }
                        };
                        Entity.LocatedScene.SendResponse(SceneOperationCode.ContainerOperation, ErrorCode.NoError, null, operationData);
                    }
                    break;
                default:
                    LibraryLog.ErrorFormat("Not Exist Channel for Container Communication, Channel: {0}", channel);
                    break;
            }
        }
        public void ErrorInform(string title, string message, ContainerCommunicationChannel channel)
        {
            switch (channel)
            {
                case ContainerCommunicationChannel.Answer:
                    {
                        if (soulDictionary.Count > 0)
                        {
                            soulDictionary.First().Value.Answer.ErrorInform(title, message);
                        }
                        else
                        {
                            LibraryLog.ErrorFormat("Not Exist Soul for Container Communication, ContainerID: {0}", ContainerID);
                        }
                    }
                    break;
                case ContainerCommunicationChannel.Scene:
                    {
                        Entity.LocatedScene.ErrorInform(title, message);
                    }
                    break;
                default:
                    LibraryLog.ErrorFormat("Not Exist Channel for Container Communication, Channel: {0}", channel);
                    break;
            }
        }
        #endregion

        public Container(int containerID, int entityID)
        {
            ContainerID = containerID;
            EntityID = entityID;
            soulDictionary = new Dictionary<int, Soul>();
            ContainerEventManager = new ContainerEventManager(this);
            ContainerOperationManager = new ContainerOperationManager(this);
            ContainerResponseManager = new ContainerResponseManager(this);
        }
        public void BindEntity(Entity entity)
        {
            Entity = entity;
        }

        public void LinkSoul(Soul soul)
        {
            if(!soulDictionary.ContainsKey(soul.SoulID))
            {
                soulDictionary.Add(soul.SoulID, soul);
            }
        }
        public void UnlinkSoul(Soul soul)
        {
            if (soulDictionary.ContainsKey(soul.SoulID))
            {
                soulDictionary.Remove(soul.SoulID);
            }
        }
    }
}
