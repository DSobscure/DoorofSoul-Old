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

        public virtual bool Handle(AnswerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                SendError(operationCode, ErrorCode.ParameterError, debugMessage, LauguageDictionarySelector.Instance[answer.Player.UsingLanguage]["Operation Parameter Error"]);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
        public void SendError(AnswerOperationCode operationCode, ErrorCode errorCode, string debugMessage, string errorMessage)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object> { { (byte)OperationErrorResponseParameterCode.ErrorMessage, errorMessage } };
            answer.SendError(operationCode, errorCode, debugMessage, parameters);
            LibraryLog.ErrorFormat("Error On Answer Operation: {0}, ErrorCode:{1}, Debug Message: {2}", operationCode, errorCode, debugMessage);
        }
        public void SendResponse(AnswerOperationCode operationCode, Dictionary<byte, object> parameter)
        {
            answer.SendResponse(operationCode, parameter);
        }
    }
}
