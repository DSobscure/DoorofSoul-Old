using System;
using System.Collections.Generic;

namespace DoorofSoul.Server.Operations.Handlers
{
    public class PlayerLoginHandler : OperationHandler
    {
        public PlayerLoginHandler(Peer peer) : base(peer)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            throw new NotImplementedException();
        }
    }
}
