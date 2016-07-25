using DoorofSoul.Library.General.Events.Handlers;
using DoorofSoul.Library.General.Events.Handlers.Answer;
using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Managers
{
    public class AnswerEventManager
    {
        protected readonly Dictionary<AnswerEventCode, AnswerEventHandler> eventTable;
        protected readonly Answer answer;

        public AnswerEventManager(Answer answer)
        {
            this.answer = answer;
            eventTable = new Dictionary<AnswerEventCode, AnswerEventHandler>
            {
                { AnswerEventCode.SoulEvent, new SoulEventResolver(answer) },
                { AnswerEventCode.ContainerEvent, new ContainerEventResolver(answer) },
                { AnswerEventCode.InformData, new InformDataResolver(answer) },
            };
        }

        public void Operate(AnswerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (eventTable.ContainsKey(eventCode))
            {
                if (!eventTable[eventCode].Handle(eventCode, parameters))
                {
                    LibraryLog.ErrorFormat("Answer Event Error: {0} from AnswerID: {1}", eventCode, answer.AnswerID);
                }
            }
            else
            {
                LibraryLog.ErrorFormat("Unknow Answer Event:{0} from AnswerID: {1}", eventCode, answer.AnswerID);
            }
        }
    }
}
