using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Language;

namespace DoorofSoul.Library.General.Operations.Handlers.Player
{
    public class LogoutHandler : PlayerOperationHandler
    {
        public LogoutHandler(General.Player player) : base(player)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 0)
            {
                debugMessage = string.Format("Logout Operation Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                player.Logout();
                Dictionary<byte, object> responseParameters = new Dictionary<byte, object>();
                SendResponse(operationCode, parameters);
                return true;
            }
            else
            {
                SendError(operationCode, Protocol.Communication.ErrorCode.InvalidOperation, "Logout Failed");
                return false;
            }
        }
    }
}
