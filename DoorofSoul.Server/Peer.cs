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
        public SupportLauguages UsingLanguage { get { return Player.UsingLanguage; } }
        protected OperationManager operationManager;
        public ServerPlayer Player { get; protected set; }


        public Peer(InitRequest initRequest, out ServerPlayer player) : base(initRequest)
        {
            Guid = Guid.NewGuid();
            operationManager = new OperationManager(this);
            Player = new ServerPlayer(this);
            player = Player;
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            Application.Log.InfoFormat("Player Disconnect from: {0} because: {1}", RemoteIPAddress, reasonDetail);
            Application.ServerInstance.PlayerDisconnect(Player);
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            operationManager.Operate(operationRequest);
        }
    }
}
