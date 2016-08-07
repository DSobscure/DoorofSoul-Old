using DoorofSoul.Library.General.Operations.Handlers;
using DoorofSoul.Library.General.Operations.Handlers.Answer;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataParameters;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Answer;
using DoorofSoul.Protocol.Communication.OperationParameters.Player;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Managers
{
    public class AnswerOperationManager
    {
        private readonly Dictionary<AnswerOperationCode, AnswerOperationHandler> operationTable;
        protected readonly Answer answer;
        public FetchDataResolver FetchDataResolver { get; protected set; }

        internal AnswerOperationManager(Answer answer)
        {
            this.answer = answer;
            FetchDataResolver = new FetchDataResolver(answer);
            operationTable = new Dictionary<AnswerOperationCode, AnswerOperationHandler>
            {
                { AnswerOperationCode.SoulOperation, new SoulOperationResolver(answer) },
                { AnswerOperationCode.ContainerOperation, new ContainerOperationResolver(answer) },
                { AnswerOperationCode.FetchData, FetchDataResolver },
                { AnswerOperationCode.CreateSoul, new CreateSoulHandler(answer) },
                { AnswerOperationCode.DeleteSoul, new DeleteSoulHandler(answer) },
                { AnswerOperationCode.ActivateSoul, new ActivateSoulHandler(answer) },
            };
        }

        internal void Operate(AnswerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, parameters))
                {
                    LibraryInstance.ErrorFormat("Answe Operation Error: {0} from AnswerID: {1}", operationCode, answer.AnswerID);
                }
            }
            else
            {
                LibraryInstance.ErrorFormat("Unknow Answer Operation:{0} from AnswerID: {1}", operationCode, answer.AnswerID);
            }
        }

        internal void SendOperation(AnswerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> operationData = new Dictionary<byte, object>
            {
                { (byte)AnswerOperationParameterCode.AnswerID, answer.AnswerID },
                { (byte)AnswerOperationParameterCode.OperationCode, (byte)operationCode },
                { (byte)AnswerOperationParameterCode.Parameters, parameters }
            };
            answer.Player.PlayerOperationManager.SendOperation(PlayerOperationCode.AnswerOperation, operationData);
        }

        public void DeleteSoul(int soulID)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)DeleteSoulParameterCode.SoulID, soulID }
            };
            SendOperation(AnswerOperationCode.DeleteSoul, parameters);
        }
        public void CreateSoul(string soulName)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)CreateSoulParameterCode.SoulName, soulName }
            };
            SendOperation(AnswerOperationCode.CreateSoul, parameters);
        }
        public void ActivateSoul(int soulID)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)ActivateSoulOperationParameterCode.SoulID, soulID }
            };
            SendOperation(AnswerOperationCode.ActivateSoul, parameters);
        }
    }
}
