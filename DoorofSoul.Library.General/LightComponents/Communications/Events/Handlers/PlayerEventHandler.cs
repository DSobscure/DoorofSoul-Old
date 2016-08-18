using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers
{
    public abstract class PlayerEventHandler
    {
        protected General.Player player;
        protected int correctParameterCount;

        protected PlayerEventHandler(General.Player player, int correctParameterCount)
        {
            this.player = player;
            this.correctParameterCount = correctParameterCount;
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
                LibraryInstance.ErrorFormat("Player Event Parameter Error On {0} PlayerID: {1} Debug Message: {2}", eventCode, player.PlayerID, debugMessage);
                return false;
            }
        }
        internal virtual bool CheckParameter(Dictionary<byte, object> parameters, out string debugMessage)
        {
            if (parameters.Count != correctParameterCount)
            {
                debugMessage = string.Format("Parameter Count: {0} Should be {1}", parameters.Count, correctParameterCount);
                return false;
            }
            else
            {
                debugMessage = "";
                return true;
            }
        }
    }
}
