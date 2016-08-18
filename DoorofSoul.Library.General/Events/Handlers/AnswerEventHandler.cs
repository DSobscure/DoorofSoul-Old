using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers
{
    public abstract class AnswerEventHandler
    {
        protected General.Answer answer;
        protected int correctParameterCount;

        protected AnswerEventHandler(General.Answer answer, int correctParameterCount)
        {
            this.answer = answer;
            this.correctParameterCount = correctParameterCount;
        }

        internal virtual bool Handle(AnswerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryInstance.ErrorFormat("Answer Event Parameter Error On {0} AnswerID: {1} Debug Message: {2}", eventCode, answer.AnswerID, debugMessage);
                return false;
            }
        }
        internal virtual bool CheckParameter(Dictionary<byte, object> parameters, out string debugMessage)
        {
            if (parameters.Count != correctParameterCount)
            {
                debugMessage = string.Format("Parameter Count: {0} Should be {1}", parameters.Count, correctParameterCount);
                return false;
            }
            else
            {
                debugMessage = "";
                return true;
            }
        }
    }
}
