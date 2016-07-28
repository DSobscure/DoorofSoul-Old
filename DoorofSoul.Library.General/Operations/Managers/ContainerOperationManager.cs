using DoorofSoul.Library.General.Operations.Handlers;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;
using DoorofSoul.Library.General.Operations.Handlers.Container;

namespace DoorofSoul.Library.General.Operations.Managers
{
    public class ContainerOperationManager
    {
        protected readonly Dictionary<ContainerOperationCode, ContainerOperationHandler> operationTable;
        protected readonly Container container;

        public ContainerOperationManager(Container container)
        {
            this.container = container;
            operationTable = new Dictionary<ContainerOperationCode, ContainerOperationHandler>
            {
                { ContainerOperationCode.FetchData, new FetchDataResolver(container) }
            };
        }

        public void Operate(ContainerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, parameters))
                {
                    LibraryLog.ErrorFormat("Container Operation Error: {0} from ContainerID: {1}", operationCode, container.ContainerID);
                }
            }
            else
            {
                LibraryLog.ErrorFormat("Unknow Container Operation:{0} from ContainerID: {1}", operationCode, container.ContainerID);
            }
        }
    }
}
