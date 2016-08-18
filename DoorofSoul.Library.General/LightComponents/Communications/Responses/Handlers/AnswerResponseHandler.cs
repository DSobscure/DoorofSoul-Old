using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers
{
    internal abstract class AnswerResponseHandler
    {
        protected ThroneComponents.Answer answer;

        protected AnswerResponseHandler(ThroneComponents.Answer answer)
        {
            this.answer = answer;
        }

        internal virtual bool Handle(AnswerOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (CheckError(parameters, returnCode, debugMessage))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        internal abstract bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage);
    }
}
