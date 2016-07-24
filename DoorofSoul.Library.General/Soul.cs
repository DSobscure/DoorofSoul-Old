using DoorofSoul.Library.General.Events.Managers;
using DoorofSoul.Library.General.Operations.Managers;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters.Answer;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Answer;
using System.Collections.Generic;

namespace DoorofSoul.Library.General
{
    public class Soul
    {
        #region properties
        public int SoulID { get; protected set; }
        public int AnswerID { get; protected set; }
        public Answer Answer { get; protected set; }
        public string SoulName { get; set; }
        protected Dictionary<int, Container> containerDictionary;
        public IEnumerable<Container> Containers { get { return containerDictionary.Values; } }
        public int ContainerCount { get { return containerDictionary.Count; } }
        public bool IsActive { get; set; }
        #endregion

        #region communication
        public SoulEventManager SoulEventManager { get; protected set; }
        public SoulOperationManager SoulOperationManager { get; protected set; }
        public void SendEvent(SoulEventCode eventCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)SoulEventParameterCode.SoulID, SoulID },
                { (byte)SoulEventParameterCode.EventCode, (byte)eventCode },
                { (byte)SoulEventParameterCode.Parameters, parameters }
            };
            Answer.SendEvent(AnswerEventCode.SoulEvent, eventData);
        }
        public void SendResponse(SoulOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> operationData = new Dictionary<byte, object>
            {
                { (byte)SoulOperationParameterCode.SoulID, SoulID },
                { (byte)SoulOperationParameterCode.OperationCode, (byte)operationCode },
                { (byte)SoulOperationParameterCode.Parameters, parameters }
            };
            Answer.SendResponse(AnswerOperationCode.SoulOperation, operationData);
        }
        public void SendError(SoulOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> operationData = new Dictionary<byte, object>
            {
                { (byte)SoulOperationParameterCode.SoulID, SoulID },
                { (byte)SoulOperationParameterCode.OperationCode, (byte)operationCode },
                { (byte)SoulOperationParameterCode.Parameters, parameters }
            };
            Answer.SendError(AnswerOperationCode.SoulOperation, errorCode, debugMessage, operationData);
        }
        #endregion

        public Soul(int soulID, Answer answer, string soulName)
        {
            SoulID = soulID;
            AnswerID = answer.AnswerID;
            Answer = answer;
            SoulName = soulName;
            containerDictionary = new Dictionary<int, Container>();
            SoulEventManager = new SoulEventManager(this);
            SoulOperationManager = new SoulOperationManager(this);
        }
        public void LinkContainer(Container container)
        {
            if(!containerDictionary.ContainsKey(container.ContainerID))
            {
                containerDictionary.Add(container.ContainerID, container);
            }
        }
        public void UnlinkContainer(Container container)
        {
            if (containerDictionary.ContainsKey(container.ContainerID))
            {
                containerDictionary.Remove(container.ContainerID);
            }
        }
        public void UnlinkAllContainers()
        {
            containerDictionary.Clear();
        }
    }
}
