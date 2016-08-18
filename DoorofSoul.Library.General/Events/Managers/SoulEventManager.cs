using DoorofSoul.Library.General.Events.Handlers;
using DoorofSoul.Library.General.Events.Handlers.Soul;
using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;
using DoorofSoul.Protocol.Communication.EventParameters.Answer;

namespace DoorofSoul.Library.General.Events.Managers
{
    public class SoulEventManager
    {
        private readonly Dictionary<SoulEventCode, SoulEventHandler> eventTable;
        protected readonly Soul soul;
        public InformDataResolver InformDataResolver { get; protected set; }

        internal SoulEventManager(Soul soul)
        {
            this.soul = soul;
            InformDataResolver = new InformDataResolver(soul);
            eventTable = new Dictionary<SoulEventCode, SoulEventHandler>
            {
                { SoulEventCode.InformData, InformDataResolver },
            };
        }

        internal void Operate(SoulEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (eventTable.ContainsKey(eventCode))
            {
                if (!eventTable[eventCode].Handle(eventCode, parameters))
                {
                    LibraryInstance.ErrorFormat("Soul Event Error: {0} from SoulID: {1}", eventCode, soul.SoulID);
                }
            }
            else
            {
                LibraryInstance.ErrorFormat("Unknow Soul Event:{0} from SoulID: {1}", eventCode, soul.SoulID);
            }
        }

        internal void SendEvent(SoulEventCode eventCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)SoulEventParameterCode.SoulID, soul.SoulID },
                { (byte)SoulEventParameterCode.EventCode, (byte)eventCode },
                { (byte)SoulEventParameterCode.Parameters, parameters }
            };
            soul.Answer.AnswerEventManager.SendEvent(AnswerEventCode.SoulEvent, eventData);
        }

        public void ErrorInform(string title, string message)
        {
            soul.Answer.AnswerEventManager.ErrorInform(title, message);
        }
    }
}
