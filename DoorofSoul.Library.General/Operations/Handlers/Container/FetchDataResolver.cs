using DoorofSoul.Library.General.Operations.Handlers.Container.FetchData;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.Channels;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataParameters;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers.Container
{
    public class FetchDataResolver : ContainerOperationHandler
    {
        private readonly Dictionary<ContainerFetchDataCode, FetchDataHandler> fetchTable;

        internal FetchDataResolver(General.Container container) : base(container, 2)
        {
            fetchTable = new Dictionary<ContainerFetchDataCode, FetchDataHandler>
            {
                { ContainerFetchDataCode.Entity, new FetchEntityHandler(container) },
                { ContainerFetchDataCode.Inventory, new FetchInventoryHandler(container) },
                { ContainerFetchDataCode.InventoryItems, new FetchInventoryItemsHandler(container) },
            };
        }

        internal override bool Handle(ContainerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                string debugMessage;
                ContainerFetchDataCode fetchCode = (ContainerFetchDataCode)parameters[(byte)FetchDataParameterCode.FetchDataCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)FetchDataParameterCode.Parameters];
                if (fetchTable.ContainsKey(fetchCode))
                {
                    return fetchTable[fetchCode].Handle(fetchCode, resolvedParameters);
                }
                else
                {
                    debugMessage = string.Format("Container Fetch Operation Not Exist Fetch Code: {0}", fetchCode);
                    SendError(operationCode, ErrorCode.InvalidOperation, debugMessage);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        internal void SendOperation(ContainerFetchDataCode fetchCode, Dictionary<byte, object> parameters, ContainerCommunicationChannel channel)
        {
            Dictionary<byte, object> fetchDataParameters = new Dictionary<byte, object>
            {
                { (byte)FetchDataParameterCode.FetchDataCode, (byte)fetchCode },
                { (byte)FetchDataParameterCode.Parameters, parameters }
            };
            container.ContainerOperationManager.SendOperation(ContainerOperationCode.FetchData, fetchDataParameters, channel);
        }

        public void FetchEntity()
        {
            SendOperation(ContainerFetchDataCode.Entity, new Dictionary<byte, object>(), ContainerCommunicationChannel.Answer);
        }
        public void FetchInventory()
        {
            SendOperation(ContainerFetchDataCode.Inventory, new Dictionary<byte, object>(), ContainerCommunicationChannel.Answer);
        }
        public void FetchInventoryItems()
        {
            SendOperation(ContainerFetchDataCode.InventoryItems, new Dictionary<byte, object>(), ContainerCommunicationChannel.Answer);
        }
    }
}
