using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers.Player
{
    public abstract class FetchDataResponseHandler
    {
        protected General.Player player;

        protected FetchDataResponseHandler(General.Player player)
        {
            this.player = player;
        }

        public virtual bool Handle(PlayerFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameters)
        {
            if (CheckError(parameters, returnCode, fetchDebugMessage))
            {
                return true;
            }
            else
            {
                LibraryInstance.ErrorFormat("Player FetchData Parameter Error On {0} PlayerID: {1} Debug Message: {2}", fetchCode, player.PlayerID, fetchDebugMessage);
                return false;
            }
        }
        public abstract bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage);
    }
}
