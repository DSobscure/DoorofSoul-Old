using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers
{
    public abstract class PlayerResponseHandler
    {
        protected General.Player player;

        protected PlayerResponseHandler(General.Player player)
        {
            this.player = player;
        }

        public virtual bool Handle(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryLog.ErrorFormat("Player Response Parameter Error On {0} PlayerID: {1} Debug Message: {2}", operationCode, player.PlayerID, debugMessage);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
