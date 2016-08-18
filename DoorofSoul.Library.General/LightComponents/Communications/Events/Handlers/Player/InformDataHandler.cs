using DoorofSoul.Protocol.Communication.InformDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Player
{
    public abstract class InformDataHandler
    {
        protected General.Player player;
        protected int correctParameterCount;

        protected InformDataHandler(General.Player player, int correctParameterCount)
        {
            this.player = player;
            this.correctParameterCount = correctParameterCount;
        }

        public virtual bool Handle(PlayerInformDataCode informCode, Dictionary<byte, object> parameter)
        {
            string debugMessage;
            if (CheckParameter(parameter, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryInstance.ErrorFormat("Player InformData Parameter Error On {0} PlayerID: {1} Debug Message: {2}", informCode, player.PlayerID, debugMessage);
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
