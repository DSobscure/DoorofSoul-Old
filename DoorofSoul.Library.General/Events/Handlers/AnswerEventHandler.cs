using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers
{
    internal abstract class AnswerEventHandler
    {
        protected General.Answer answer;

        protected AnswerEventHandler(General.Answer answer)
        {
            this.answer = answer;
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
                LibraryLog.ErrorFormat("Answer Event Parameter Error On {0} AnswerID: {1} Debug Message: {2}", eventCode, answer.AnswerID, debugMessage);
                return false;
            }
        }
        internal abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
