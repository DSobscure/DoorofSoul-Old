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
        public SupportLauguages UsingLanguage { get { return player.UsingLanguage; } }
        protected OperationManager operationManager;
        internal ServerPlayer player;


        public Peer(InitRequest initRequest, out ServerPlayer player) : base(initRequest)
        {
            Guid = Guid.NewGuid();
            operationManager = new OperationManager(this);
            player = new ServerPlayer(this);
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            Application.Log.InfoFormat("Player Disconnect from: {0} because: {1}", RemoteIPAddress, reasonDetail);
            Application.ServerInstance.PlayerDisconnect(player);
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            operationManager.Operate(operationRequest);
        }
    }
}
