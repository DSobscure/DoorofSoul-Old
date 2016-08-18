using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Language;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Player
{
    internal class LogoutHandler : PlayerOperationHandler
    {
        internal LogoutHandler(General.Player player) : base(player, 0)
        {
        }

        internal override bool Handle(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                player.PlayerCommunicationInterface.Logout();
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
