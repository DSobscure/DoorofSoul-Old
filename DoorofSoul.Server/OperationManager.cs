using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoorofSoul.Protocol.Communication;
using Photon.SocketServer;
using DoorofSoul.Server.Operations.Handlers;
using DoorofSoul.Protocol.Communication.OperationCodes;

namespace DoorofSoul.Server
{
    public class OperationManager
    {
        protected readonly Peer peer;

        public OperationManager(Peer peer)
        {
            this.peer = peer;
        }

        public void Operate(OperationRequest operationRequest)
        {
            PlayerOperationCode operationCode = (PlayerOperationCode)operationRequest.OperationCode;
            peer.Player.PlayerOperationManager.Operate(operationCode, operationRequest.Parameters);
        }
    }
}
