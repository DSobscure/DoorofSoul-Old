using DoorofSoul.Client.Communication.Handlers;
using DoorofSoul.Client.Communication.Handlers.ResponseHandlers;
using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Client.Communication.Managers.ResponseManagers
{
    public class ResponseManager
    {
        protected readonly Dictionary<OperationCode, ResponseHandler> responseTable;

        public ResponseManager()
        {
            responseTable = new Dictionary<OperationCode, ResponseHandler>
            {
                { OperationCode.FetchData, new FetchDataResponseHandler() },
                { OperationCode.PlayerLogin, new PlayerLoginResponseHandler() },
                { OperationCode.PlayerLogout, new PlayerLogoutResponseHandler() },
                { OperationCode.CreateSoul, new CreateSoulResponseHandler() },
                { OperationCode.DeleteSoul, new DeleteSoulResponseHandler() },
                { OperationCode.ActivateSoul, new ActivateSoulResponseHandler() },
            };
        }

        public void Operate(OperationResponse operationResponse)
        {
            OperationCode operationCode = (OperationCode)operationResponse.OperationCode;
            if(responseTable.ContainsKey(operationCode))
            {
                responseTable[operationCode].Handle(operationResponse);
            }
            else
            {
                Global.SystemManagers.DebugInformManager.DebugInform(string.Format("Unknow Response: {0}", operationCode));
            }
        }
        #region player login
        private event Action<Player> onPlayerLogin;
        public event Action<Player> OnPlayerLogin
        {
            add { onPlayerLogin += value; }
            remove { onPlayerLogin -= value; }
        }
        public void PlayerLogin(Player player)
        {
            if (onPlayerLogin != null)
            {
                onPlayerLogin(player);
            }
            else
            {
                Global.SystemManagers.DebugInformManager.DebugInform("PlayerLogin Event is null");
            }
        }
        #endregion

        #region player logout
        private event Action onPlayerLogout;
        public event Action OnPlayerLogout
        {
            add { onPlayerLogout += value; }
            remove { onPlayerLogout -= value; }
        }
        public void PlayerLogout()
        {
            if (onPlayerLogout != null)
            {
                onPlayerLogout();
            }
            else
            {
                Global.SystemManagers.DebugInformManager.DebugInform("PlayerLogout Event is null");
            }
        }
        #endregion
    }
}

