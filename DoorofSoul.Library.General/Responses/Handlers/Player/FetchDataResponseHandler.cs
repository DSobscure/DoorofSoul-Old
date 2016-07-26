using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers.Player
{
    public abstract class FetchDataResponseHandler
    {
        protected General.Player player;

        protected FetchDataResponseHandler(General.Player player)
        {
            this.player = player;
        }

        public virtual bool Handle(PlayerFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameter)
        {
            string debugMessage;
            if (CheckParameter(parameter, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryLog.ErrorFormat("Player FetchData Parameter Error On {0} PlayerID: {1} Debug Message: {2}", fetchCode, player.PlayerID, debugMessage);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
