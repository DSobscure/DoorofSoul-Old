using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers
{
    public abstract class AnswerResponseHandler
    {
        protected General.Answer answer;

        protected AnswerResponseHandler(General.Answer answer)
        {
            this.answer = answer;
        }

        public virtual bool Handle(AnswerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryLog.ErrorFormat("Answer Response Parameter Error On {0} AnswerID: {1} Debug Message: {2}", operationCode, answer.AnswerID, debugMessage);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
