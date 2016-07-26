using DoorofSoul.Library.General.Responses.Handlers;
using DoorofSoul.Library.General.Responses.Handlers.Soul;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Managers
{
    public class SoulResponseManager
    {
        protected readonly Dictionary<SoulOperationCode, SoulResponseHandler> operationTable;
        protected readonly Soul soul;

        public SoulResponseManager(Soul soul)
        {
            this.soul = soul;
            operationTable = new Dictionary<SoulOperationCode, SoulResponseHandler>
            {
                { SoulOperationCode.FetchData, new FetchDataResponseResolver(soul) }
            };
        }

        public void Operate(SoulOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, parameters))
                {
                    LibraryLog.ErrorFormat("Soul Response Error: {0} from AnswerID: {1}", operationCode, soul.SoulID);
                }
            }
            else
            {
                LibraryLog.ErrorFormat("Unknow Soul Response:{0} from AnswerID: {1}", operationCode, soul.SoulID);
            }
        }
    }
}
