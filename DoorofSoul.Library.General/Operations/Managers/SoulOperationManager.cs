using DoorofSoul.Library.General.Operations.Handlers;
using DoorofSoul.Library.General.Operations.Handlers.Soul;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Answer;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Managers
{
    public class SoulOperationManager
    {
        private readonly Dictionary<SoulOperationCode, SoulOperationHandler> operationTable;
        protected readonly Soul soul;

        internal SoulOperationManager(Soul soul)
        {
            this.soul = soul;
            operationTable = new Dictionary<SoulOperationCode, SoulOperationHandler>
            {
                { SoulOperationCode.FetchData, new FetchDataResolver(soul) },
            };
        }

        internal void Operate(SoulOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, parameters))
                {
                    LibraryLog.ErrorFormat("Soul Operation Error: {0} from SoulID: {1}", operationCode, soul.SoulID);
                }
            }
            else
            {
                LibraryLog.ErrorFormat("Unknow Soul Operation:{0} from SoulID: {1}", operationCode, soul.SoulID);
            }
        }

        internal void SendOperation(SoulOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> operationData = new Dictionary<byte, object>
            {
                { (byte)SoulOperationParameterCode.SoulID, soul.SoulID },
                { (byte)SoulOperationParameterCode.OperationCode, (byte)operationCode },
                { (byte)SoulOperationParameterCode.Parameters, parameters }
            };
            soul.Answer.AnswerOperationManager.SendOperation(AnswerOperationCode.SoulOperation, operationData);
        }
    }
}
