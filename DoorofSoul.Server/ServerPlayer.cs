using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoorofSoul.Library.General;

namespace DoorofSoul.Server
{
    public class ServerPlayer : Player
    {
        public Guid Guid
        {
            get { return peer.Guid; }
        }

        protected Peer peer;

        public ServerPlayer(Peer peer)
        {
            this.peer = peer;
            LastConnectedIPAddress = peer.RemoteIPAddress;
        }
    }
}
