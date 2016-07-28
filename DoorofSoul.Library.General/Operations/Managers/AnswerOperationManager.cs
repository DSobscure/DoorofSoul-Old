using DoorofSoul.Library.General.Operations.Handlers;
using DoorofSoul.Library.General.Operations.Handlers.Answer;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Managers
{
    public class AnswerOperationManager
    {
        protected readonly Dictionary<AnswerOperationCode, AnswerOperationHandler> operationTable;
        protected readonly Answer answer;

        public AnswerOperationManager(Answer answer)
        {
            this.answer = answer;
            operationTable = new Dictionary<AnswerOperationCode, AnswerOperationHandler>
            {
                { AnswerOperationCode.SoulOperation, new SoulOperationResolver(answer) },
                { AnswerOperationCode.ContainerOperation, new ContainerOperationResolver(answer) },
                { AnswerOperationCode.FetchData, new FetchDataResolver(answer) },
                { AnswerOperationCode.CreateSoul, new CreateSoulHandler(answer) },
                { AnswerOperationCode.DeleteSoul, new DeleteSoulHandler(answer) },
                { AnswerOperationCode.ActivateSoul, new ActivateSoulHandler(answer) },
            };
        }

        public void Operate(AnswerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, parameters))
                {
                    LibraryLog.ErrorFormat("Answe Operation Error: {0} from AnswerID: {1}", operationCode, answer.AnswerID);
                }
            }
            else
            {
                LibraryLog.ErrorFormat("Unknow Answer Operation:{0} from AnswerID: {1}", operationCode, answer.AnswerID);
            }
        }
    }
}
