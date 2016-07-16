using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication;
using Photon.SocketServer;

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
            SendEvent = SendServerEvent;
        }

        public void SendServerEvent(EventCode eventCode, Dictionary<byte, object> parameters)
        {
            EventData eventData = new EventData
            {
                Code = (byte)eventCode,
                Parameters = parameters
            };
            peer.SendEvent(eventData, new SendParameters());
        }
    }
}
