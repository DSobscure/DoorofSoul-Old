using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.ResponseParameters;
using DoorofSoul.Protocol.Language;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers
{
    public abstract class AnswerOperationHandler
    {
        protected General.Answer answer;

        protected AnswerOperationHandler(General.Answer answer)
        {
            this.answer = answer;
        }

        internal virtual bool Handle(AnswerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                SendError(operationCode, ErrorCode.ParameterError, debugMessage);
                return false;
            }
        }
        internal abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
        internal void SendError(AnswerOperationCode operationCode, ErrorCode errorCode, string debugMessage)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>();
            answer.AnswerResponseManager.SendResponse(operationCode, errorCode, debugMessage, parameters);
            LibraryInstance.ErrorFormat("Error On Answer Operation: {0}, ErrorCode:{1}, Debug Message: {2}", operationCode, errorCode, debugMessage);
        }
        internal void SendResponse(AnswerOperationCode operationCode, Dictionary<byte, object> parameter)
        {
            answer.AnswerResponseManager.SendResponse(operationCode, ErrorCode.NoError, null, parameter);
        }
    }
}
