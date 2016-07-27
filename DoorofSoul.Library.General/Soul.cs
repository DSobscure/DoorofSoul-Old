using DoorofSoul.Library.General.Events.Managers;
using DoorofSoul.Library.General.Operations.Managers;
using DoorofSoul.Library.General.Responses.Managers;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters.Answer;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Answer;
using DoorofSoul.Protocol.Communication.ResponseParameters.Answer;
using DoorofSoul.Protocol.Language;
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
        public SupportLauguages UsingLanguage { get { return Answer.UsingLanguage; } }
        #endregion

        #region communication
        internal SoulEventManager SoulEventManager { get; set; }
        internal SoulOperationManager SoulOperationManager { get; set; }
        internal SoulResponseManager SoulResponseManager { get; set; }
        internal void SendEvent(SoulEventCode eventCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)SoulEventParameterCode.SoulID, SoulID },
                { (byte)SoulEventParameterCode.EventCode, (byte)eventCode },
                { (byte)SoulEventParameterCode.Parameters, parameters }
            };
            Answer.SendEvent(AnswerEventCode.SoulEvent, eventData);
        }
        internal void SendOperation(SoulOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> operationData = new Dictionary<byte, object>
            {
                { (byte)SoulOperationParameterCode.SoulID, SoulID },
                { (byte)SoulOperationParameterCode.OperationCode, (byte)operationCode },
                { (byte)SoulOperationParameterCode.Parameters, parameters }
            };
            Answer.SendOperation(AnswerOperationCode.SoulOperation, operationData);
        }
        internal void SendResponse(SoulOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> responseData = new Dictionary<byte, object>
            {
                { (byte)SoulResponseParameterCode.SoulID, SoulID },
                { (byte)SoulResponseParameterCode.OperationCode, (byte)operationCode },
                { (byte)SoulResponseParameterCode.ReturnCode, (short)errorCode },
                { (byte)SoulResponseParameterCode.DebugMessage, debugMessage },
                { (byte)SoulResponseParameterCode.Parameters, parameters }
            };
            Answer.SendResponse(AnswerOperationCode.SoulOperation, ErrorCode.NoError, null, responseData);
        }
        internal void ErrorInform(string title, string message)
        {
            Answer.ErrorInform(title, message);
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
            SoulResponseManager = new SoulResponseManager(this);
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
