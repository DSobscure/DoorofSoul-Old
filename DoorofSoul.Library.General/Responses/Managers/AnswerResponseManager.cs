using DoorofSoul.Library.General.Responses.Handlers;
using DoorofSoul.Library.General.Responses.Handlers.Answer;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;
using DoorofSoul.Protocol.Communication.ResponseParameters.Player;

namespace DoorofSoul.Library.General.Responses.Managers
{
    internal class AnswerResponseManager
    {
        protected readonly Dictionary<AnswerOperationCode, AnswerResponseHandler> operationTable;
        protected readonly Answer answer;

        public AnswerResponseManager(Answer answer)
        {
            this.answer = answer;
            operationTable = new Dictionary<AnswerOperationCode, AnswerResponseHandler>
            {
                { AnswerOperationCode.SoulOperation, new SoulOperationResponseResolver(answer) },
                { AnswerOperationCode.ContainerOperation, new ContainerOperationResponsResolver(answer) },
                { AnswerOperationCode.FetchData, new FetchDataResponseResolver(answer) },
                { AnswerOperationCode.CreateSoul, new CreateSoulResponseHandler(answer) },
                { AnswerOperationCode.DeleteSoul, new DeleteSoulResponseHandler(answer) },
                { AnswerOperationCode.ActivateSoul, new ActivateSoulResponseHandler(answer) },
            };
        }

        public void Operate(AnswerOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, returnCode, debugMessage, parameters))
                {
                    LibraryLog.ErrorFormat("Answer Response Error: {0} from AnswerID: {1}", operationCode, answer.AnswerID);
                }
            }
            else
            {
                LibraryLog.ErrorFormat("Unknow Answer Response:{0} from AnswerID: {1}", operationCode, answer.AnswerID);
            }
        }

        internal void SendResponse(AnswerOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> responseData = new Dictionary<byte, object>
            {
                { (byte)AnswerResponseParameterCode.AnswerID, answer.AnswerID },
                { (byte)AnswerResponseParameterCode.OperationCode, (byte)operationCode },
                { (byte)AnswerResponseParameterCode.ReturnCode, (short)errorCode },
                { (byte)AnswerResponseParameterCode.DebugMessage, debugMessage },
                { (byte)AnswerResponseParameterCode.Parameters, parameters }
            };
            answer.Player.PlayerResponseManager.SendResponse(PlayerOperationCode.AnswerOperation, ErrorCode.NoError, null, responseData);
        }
    }
}
