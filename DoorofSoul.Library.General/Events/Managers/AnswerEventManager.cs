using DoorofSoul.Library.General.Events.Handlers;
using DoorofSoul.Library.General.Events.Handlers.Answer;
using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;
using DoorofSoul.Protocol.Communication.EventParameters.Player;

namespace DoorofSoul.Library.General.Events.Managers
{
    public class AnswerEventManager
    {
        private readonly Dictionary<AnswerEventCode, AnswerEventHandler> eventTable;
        protected readonly Answer answer;
        public InformDataResolver InformDataResolver { get; protected set; }

        internal AnswerEventManager(Answer answer)
        {
            this.answer = answer;
            InformDataResolver = new InformDataResolver(answer);
            eventTable = new Dictionary<AnswerEventCode, AnswerEventHandler>
            {
                { AnswerEventCode.SoulEvent, new SoulEventResolver(answer) },
                { AnswerEventCode.ContainerEvent, new ContainerEventResolver(answer) },
                { AnswerEventCode.InformData, InformDataResolver },
            };
        }

        internal void Operate(AnswerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (eventTable.ContainsKey(eventCode))
            {
                if (!eventTable[eventCode].Handle(eventCode, parameters))
                {
                    LibraryInstance.ErrorFormat("Answer Event Error: {0} from AnswerID: {1}", eventCode, answer.AnswerID);
                }
            }
            else
            {
                LibraryInstance.ErrorFormat("Unknow Answer Event:{0} from AnswerID: {1}", eventCode, answer.AnswerID);
            }
        }

        internal void SendEvent(AnswerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)AnswerEventParameterCode.AnswerID, answer.AnswerID },
                { (byte)AnswerEventParameterCode.EventCode, (byte)eventCode },
                { (byte)AnswerEventParameterCode.Parameters, parameters }
            };
            answer.Player.PlayerEventManager.SendEvent(PlayerEventCode.AnswerEvent, eventData);
        }

        public void ErrorInform(string title, string message)
        {
            answer.Player.PlayerEventManager.ErrorInform(title, message);
        }
    }
}
