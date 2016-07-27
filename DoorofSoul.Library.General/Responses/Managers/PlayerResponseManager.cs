﻿using DoorofSoul.Library.General.Responses.Handlers;
using DoorofSoul.Library.General.Responses.Handlers.Player;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Managers
{
    public class PlayerResponseManager
    {
        protected readonly Dictionary<PlayerOperationCode, PlayerResponseHandler> operationTable;
        protected readonly Player player;

        public PlayerResponseManager(Player player)
        {
            this.player = player;
            operationTable = new Dictionary<PlayerOperationCode, PlayerResponseHandler>
            {
                { PlayerOperationCode.AnswerOperation, new AnswerOperationResponseResolver(player) },
                { PlayerOperationCode.FetchData, new FetchDataResponseResolver(player) },
                { PlayerOperationCode.Login, new LoginResponseHandler(player) },
                { PlayerOperationCode.Logout, new LogoutResponseHandler(player) },
            };
        }

        public void Operate(PlayerOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, returnCode, debugMessage, parameters))
                {
                    LibraryLog.ErrorFormat("Player Response Error: {0} from AnswerID: {1}", operationCode, player.PlayerID);
                }
            }
            else
            {
                LibraryLog.ErrorFormat("Unknow Player Response:{0} from AnswerID: {1}", operationCode, player.PlayerID);
            }
        }
    }
}
