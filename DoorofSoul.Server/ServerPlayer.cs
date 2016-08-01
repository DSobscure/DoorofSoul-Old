using DoorofSoul.Database.Library;
using DoorofSoul.Library.General;
using System;

namespace DoorofSoul.Server
{
    public class ServerPlayer : Player
    {

        public Guid Guid
        {
            get { return peer.Guid; }
        }

        protected Peer peer;

        public ServerPlayer(Peer peer, PlayerCommunicationInterface communicationInterface) : base(communicationInterface)
        {
            this.peer = peer;
            LastConnectedIPAddress = peer.RemoteIPAddress;
        }
        public void LoadPlayer(PlayerData player)
        {
            PlayerID = player.playerID;
            Account = player.account;
            Nickname = player.nickname;
            UsingLanguage = player.usingLanguage;
            LastConnectedIPAddress = player.lastConnectedIPAddress;
            AnswerID = player.answerID;
        }
        public void RelifeWithNewPlayer(ServerPlayer newPlayer)
        {
            peer = newPlayer.peer;
            peer.RelifeWithOldPlayer(this);
            LastConnectedIPAddress = peer.RemoteIPAddress;
        }
    }
}
