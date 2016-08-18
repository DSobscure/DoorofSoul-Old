using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers.Answer
{
    internal abstract class FetchDataResponseHandler
    {
        protected ThroneComponents.Answer answer;

        protected FetchDataResponseHandler(ThroneComponents.Answer answer)
        {
            this.answer = answer;
        }

        internal virtual bool Handle(AnswerFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameters)
        {
            if (CheckError(parameters, returnCode, fetchDebugMessage))
            {
                return true;
            }
            else
            {
                LibraryInstance.ErrorFormat("Answer FetchData Parameter Error On {0} AnswerID: {1} Debug Message: {2}", fetchCode, answer.AnswerID, fetchDebugMessage);
                return false;
            }
        }
        internal abstract bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage);
    }
}
