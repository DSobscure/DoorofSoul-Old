using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorofSoul.Server.Operations.Handlers
{
    public class PlayerLogoutHandler : OperationHandler
    {
        public PlayerLogoutHandler(Peer peer) : base(peer)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            throw new NotImplementedException();
        }
    }
}
