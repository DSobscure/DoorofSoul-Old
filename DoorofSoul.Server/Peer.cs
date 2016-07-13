using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using DoorofSoul.Server.Operations;
using DoorofSoul.Protocol.Communication;

namespace DoorofSoul.Server
{
    public class Peer : ClientPeer
    {
        public Guid Guid { get; }
        protected OperationManager operationManager;
        public SupportLauguages UsingLanguage { get; protected set; }


        public Peer(InitRequest initRequest) : base(initRequest)
        {
            Guid = Guid.NewGuid();
            operationManager = new OperationManager(this);
            Application.Log.Info("new connection");
            UsingLanguage = SupportLauguages.Chinese_Traditional;
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            operationManager.Operate(operationRequest);
        }
    }
}
