using DoorofSoul.Library.General.Operations.Handlers;
using DoorofSoul.Library.General.Operations.Handlers.Player;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Managers
{
    public class PlayerOperationManager
    {
        protected readonly Dictionary<PlayerOperationCode, PlayerOperationHandler> operationTable;
        protected readonly Player player;

        public PlayerOperationManager(Player player)
        {
            this.player = player;
            operationTable = new Dictionary<PlayerOperationCode, PlayerOperationHandler>
            {
                { PlayerOperationCode.AnswerOperation, new AnswerOperationResolver(player) },
                { PlayerOperationCode.FetchData, new FetchDataHandler(player) },
                { PlayerOperationCode.Login, new LoginHandler(player) },
                { PlayerOperationCode.Logout, new LogoutHandler(player) },
            };
        }

        public void Operate(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, parameters))
                {
                    LibraryLog.ErrorFormat("Player Operation Error: {0} from PlayerID: {1} IP:{2}", operationCode, player.PlayerID, player.LastConnectedIPAddress);
                }
            }
            else
            {
                LibraryLog.ErrorFormat("Unknow Player Operation:{0} from PlayerID: {1} IP:{2}", operationCode, player.PlayerID, player.LastConnectedIPAddress);
            }
        }
    }
}
