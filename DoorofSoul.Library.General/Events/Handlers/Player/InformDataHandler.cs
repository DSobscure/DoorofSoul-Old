using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Player
{
    public abstract class InformDataHandler
    {
        protected General.Player player;

        protected InformDataHandler(General.Player player)
        {
            this.player = player;
        }

        public virtual bool Handle(PlayerInformDataCode informCode, ErrorCode returnCode, Dictionary<byte, object> parameter)
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
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
