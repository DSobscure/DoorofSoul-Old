using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoorofSoul.Protocol.Communication;
using Photon.SocketServer;
using DoorofSoul.Server.Operations.Handlers;

namespace DoorofSoul.Server.Operations
{
    public class OperationManager
    {
        protected readonly Dictionary<OperationCode, OperationHandler> operationTable;
        protected readonly Peer peer;

        public OperationManager(Peer peer)
        {
            this.peer = peer;
            operationTable = new Dictionary<OperationCode, OperationHandler>
            {
                { OperationCode.FetchData, new FetchDataManager(peer) }
            };
        }

        public void Operate(OperationRequest operationRequest)
        {
            OperationCode operationCode = (OperationCode)operationRequest.OperationCode;
            if(operationTable.ContainsKey(operationCode))
            {
                if(!operationTable[operationCode].Handle(operationRequest))
                {
                    Application.Log.ErrorFormat("Operation Error: {0} from Peer Guid:{1} IP:{2}", operationCode, peer.Guid, peer.RemoteIP);
                }
            }
            else
            {
                Application.Log.ErrorFormat("Unknow Operation:{0} from Peer Guid:{1} IP:{2}", operationCode, peer.Guid, peer.RemoteIP);
            }
        }
    }
}
