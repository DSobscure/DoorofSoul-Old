using DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers;
using DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers.Player;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;
using DoorofSoul.Protocol.Language;
using System;

namespace DoorofSoul.Library.General.LightComponents.Communications.Responses.Managers
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
                { PlayerOperationCode.Register, new RegisterResponseHandler(player) },
            };
        }

        public void Operate(PlayerOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, returnCode, debugMessage, parameters))
                {
                    LibraryInstance.ErrorFormat("Player Response Error: {0} from AnswerID: {1}", operationCode, player.PlayerID);
                }
            }
            else
            {
                LibraryInstance.ErrorFormat("Unknow Player Response:{0} from AnswerID: {1}", operationCode, player.PlayerID);
            }
        }

        public void SendResponse(PlayerOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            player.PlayerCommunicationInterface.SendResponse(operationCode, errorCode, debugMessage, parameters);
        }

        public void Login(int playerID, string account, string nickname, SupportLauguages usingLanguage, int answerID)
        {
            player.LoadPlayer(playerID, account, nickname, usingLanguage, answerID);
            player.IsOnline = true;
        }
        public void LoginFailed()
        {
            player.IsOnline = false;
        }
        public void Logout()
        {
            player.IsOnline = false;
        }
        public void FetchSystemVersion(string serverVersion, string clientVersion)
        {
            player.PlayerCommunicationInterface.UpdateSystemVersion(serverVersion, clientVersion);
        }

        public void FetchWorlds(int worldID, string worldName)
        {
            player.PlayerCommunicationInterface.LoadWorld(worldID, worldName);
        }
        public void FetchSceneResponse(int sceneID, string sceneName, int worldID)
        {
            player.PlayerCommunicationInterface.LoadScene(sceneID, sceneName, worldID);
        }
    }
}
