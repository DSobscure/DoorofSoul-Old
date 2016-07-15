using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;

namespace DoorofSoul.Server.Operations.Handlers
{
    public class PlayerLogoutHandler : OperationHandler
    {
        public PlayerLogoutHandler(Peer peer) : base(peer)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 0)
            {
                debugMessage = "Player Logout Operation Parameter Error";
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(OperationRequest operationRequest)
        {
            if(base.Handle(operationRequest))
            {
                Application.ServerInstance.PlayerLogout(peer.player);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
