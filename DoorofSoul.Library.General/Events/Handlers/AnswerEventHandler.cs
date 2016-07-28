using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers
{
    public abstract class AnswerEventHandler
    {
        protected General.Answer answer;

        protected AnswerEventHandler(General.Answer answer)
        {
            this.answer = answer;
        }

        public virtual bool Handle(AnswerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryLog.ErrorFormat("Answer Event Parameter Error On {0} AnswerID: {1} Debug Message: {2}", eventCode, answer.AnswerID, debugMessage);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
