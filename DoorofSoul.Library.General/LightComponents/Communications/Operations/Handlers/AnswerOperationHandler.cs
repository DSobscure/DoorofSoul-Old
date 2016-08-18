using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.ResponseParameters;
using DoorofSoul.Protocol.Language;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers
{
    public abstract class AnswerOperationHandler
    {
        protected ThroneComponents.Answer answer;
        protected int correctParameterCount;

        protected AnswerOperationHandler(ThroneComponents.Answer answer, int correctParameterCount)
        {
            this.answer = answer;
            this.correctParameterCount = correctParameterCount;
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
