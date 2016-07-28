using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Server.Operations.Handlers;
using Photon.SocketServer;
using System.Collections.Generic;

namespace DoorofSoul.Server.Operations
{
    public class OperationResolver
    {
        protected Dictionary<OperationCode, OperationHandler> operationTable;
        protected readonly Peer peer;

        public OperationResolver(Peer peer)
        {
            this.peer = peer;
            operationTable = new Dictionary<OperationCode, OperationHandler>
            {
                { OperationCode.PlayerOperation, new PlayerOperationResolver(peer) },
                { OperationCode.WorldOperation, new WorldOperationResolver(peer) }
            };
        }

        public void Operate(OperationRequest operationRequest)
        {
            OperationCode operationCode = (OperationCode)operationRequest.OperationCode;
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, operationRequest.Parameters))
                {
                    Application.Log.ErrorFormat("Operation Error: {0} from Guid: {1} IP:{2}", operationCode, peer.Guid, peer.RemoteIPAddress);
                }
            }
            else
            {
                Application.Log.ErrorFormat("Unknow Operation:{0} from Guid: {1} IP:{2}", operationCode, peer.Guid, peer.RemoteIPAddress);
            }
        }
    }
}
