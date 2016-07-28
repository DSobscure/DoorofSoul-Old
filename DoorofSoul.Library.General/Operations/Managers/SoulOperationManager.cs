using DoorofSoul.Library.General.Operations.Handlers;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;
using DoorofSoul.Library.General.Operations.Handlers.Soul;

namespace DoorofSoul.Library.General.Operations.Managers
{
    public class SoulOperationManager
    {
        protected readonly Dictionary<SoulOperationCode, SoulOperationHandler> operationTable;
        protected readonly Soul soul;

        public SoulOperationManager(Soul soul)
        {
            this.soul = soul;
            operationTable = new Dictionary<SoulOperationCode, SoulOperationHandler>
            {
                { SoulOperationCode.FetchData, new FetchDataResolver(soul) },
            };
        }

        public void Operate(SoulOperationCode operationCode, Dictionary<byte, object> parameters)
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
    }
}
