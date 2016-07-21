using DoorofSoul.Protocol.Communication;
using DoorofSoul.Server.Operations;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using System;

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
            Application.ServerInstance.PlayerFactory.PlayerDisconnect(Player);
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            operationManager.Operate(operationRequest);
        }

        public void RelifeWithOldPlayer(ServerPlayer old)
        {
            Player = old;
        }
    }
}
