using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers.Answer
{
    public abstract class FetchDataResponseHandler
    {
        protected General.Answer answer;

        protected FetchDataResponseHandler(General.Answer answer)
        {
            this.answer = answer;
        }

        public virtual bool Handle(AnswerFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameters)
        {
            if (CheckError(parameters, returnCode, fetchDebugMessage))
            {
                return true;
            }
            else
            {
                LibraryLog.ErrorFormat("Answer FetchData Parameter Error On {0} AnswerID: {1} Debug Message: {2}", fetchCode, answer.AnswerID, fetchDebugMessage);
                return false;
            }
        }
        public abstract bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage);
    }
}
