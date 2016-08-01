using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers
{
    internal abstract class PlayerEventHandler
    {
        protected General.Player player;

        protected PlayerEventHandler(General.Player player)
        {
            this.player = player;
        }

        internal virtual bool Handle(PlayerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryLog.ErrorFormat("Player Event Parameter Error On {0} PlayerID: {1} Debug Message: {2}", eventCode, player.PlayerID, debugMessage);
                return false;
            }
        }
        internal abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
