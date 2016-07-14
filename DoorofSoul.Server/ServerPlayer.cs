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
        public System.Net.IPAddress RemoteIPAddress
        {
            get { return peer.RemoteIPAddress; }
        }
        protected Peer peer;

        public ServerPlayer(Peer peer)
        {
            this.peer = peer;
        }
    }
}
