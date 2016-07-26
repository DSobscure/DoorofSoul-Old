using DoorofSoul.Protocol.Language;
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
        protected OperationResolver operationResolver;
        public ServerPlayer Player { get; protected set; }


        public Peer(InitRequest initRequest, out ServerPlayer player) : base(initRequest)
        {
            Guid = Guid.NewGuid();
            operationResolver = new OperationResolver(this);
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
            operationResolver.Operate(operationRequest);
        }

        public void RelifeWithOldPlayer(ServerPlayer old)
        {
            Player = old;
        }
    }
}
