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
                Application.ServerInstance.PlayerFactory.PlayerLogout(peer.Player);
                Dictionary<byte, object> parameters = new Dictionary<byte, object>();
                SendResponse(operationRequest.OperationCode, parameters);
                return true;
            }
            else
            {
                SendError(operationRequest.OperationCode, Protocol.Communication.ErrorCode.InvalidOperation, "Logout Failed", LauguageDictionarySelector.Instance[peer.UsingLanguage]["Logout Failed"]);
                return false;
            }
        }
    }
}
