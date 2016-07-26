using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication.OperationCodes;

namespace DoorofSoul.Library.General.Responses.Handlers.Player
{
    public class LogoutResponseResolver : PlayerResponseHandler
    {
        public LogoutResponseResolver(General.Player player) : base(player)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            throw new NotImplementedException();
        }

        public override bool Handle(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            return base.Handle(operationCode, parameters);
        }
    }
}
