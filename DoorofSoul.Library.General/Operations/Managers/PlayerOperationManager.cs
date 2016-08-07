using DoorofSoul.Library.General.Operations.Handlers;
using DoorofSoul.Library.General.Operations.Handlers.Player;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataParameters;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Player;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Managers
{
    public class PlayerOperationManager
    {
        private readonly Dictionary<PlayerOperationCode, PlayerOperationHandler> operationTable;
        protected readonly Player player;
        public FetchDataResolver FetchDataResolver { get; protected set; }

        public PlayerOperationManager(Player player)
        {
            this.player = player;
            FetchDataResolver = new FetchDataResolver(player);
            operationTable = new Dictionary<PlayerOperationCode, PlayerOperationHandler>
            {
                { PlayerOperationCode.AnswerOperation, new AnswerOperationResolver(player) },
                { PlayerOperationCode.FetchData, FetchDataResolver },
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
                    LibraryInstance.ErrorFormat("Player Operation Error: {0} from PlayerID: {1} IP:{2}", operationCode, player.PlayerID, player.LastConnectedIPAddress);
                }
            }
            else
            {
                LibraryInstance.ErrorFormat("Unknow Player Operation:{0} from PlayerID: {1} IP:{2}", operationCode, player.PlayerID, player.LastConnectedIPAddress);
            }
        }

        public void SendOperation(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            player.PlayerCommunicationInterface.SendOperation(operationCode, parameters);
        }
        public void SendAnswerOperation(Answer answer, AnswerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> operationParameters = new Dictionary<byte, object>
            {
                { (byte)AnswerOperationParameterCode.AnswerID, answer.AnswerID },
                { (byte)AnswerOperationParameterCode.OperationCode, operationCode },
                { (byte)AnswerOperationParameterCode.Parameters, parameters }
            };
            SendOperation(PlayerOperationCode.AnswerOperation, operationParameters);
        }

        public void Login(string account, string password)
        {
            var parameters = new Dictionary<byte, object>
            {
                { (byte)LoginParameterCode.Account, account },
                { (byte)LoginParameterCode.Password, password }
            };
            SendOperation(PlayerOperationCode.Login, parameters);
        }
        public void Logout()
        {
            var parameters = new Dictionary<byte, object>();
            SendOperation(PlayerOperationCode.Logout, parameters);
        }
        public void FetchSystemVersion()
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)FetchDataParameterCode.FetchDataCode, PlayerFetchDataCode.SystemVersion },
                { (byte)FetchDataParameterCode.Parameters, new Dictionary<byte, object>() }
            };
            SendOperation(PlayerOperationCode.FetchData, parameters);
        }
    }
}
