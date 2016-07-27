using DoorofSoul.Library.General.Responses.Handlers;
using DoorofSoul.Library.General.Responses.Handlers.Container;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Managers
{
    public class ContainerResponseManager
    {
        protected readonly Dictionary<ContainerOperationCode, ContainerResponseHandler> operationTable;
        protected readonly Container container;

        public ContainerResponseManager(Container container)
        {
            this.container = container;
            operationTable = new Dictionary<ContainerOperationCode, ContainerResponseHandler>
            {
                { ContainerOperationCode.FetchData, new FetchDataResponseResolver(container) }
            };
        }

        public void Operate(ContainerOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, returnCode, debugMessage, parameters))
                {
                    LibraryLog.ErrorFormat("Container Response Error: {0} from AnswerID: {1}", operationCode, container.ContainerID);
                }
            }
            else
            {
                LibraryLog.ErrorFormat("Unknow Container Response:{0} from AnswerID: {1}", operationCode, container.ContainerID);
            }
        }
    }
}
