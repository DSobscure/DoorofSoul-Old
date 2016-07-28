using System;
using System.Collections.Generic;
using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Player;
using DoorofSoul.Protocol.Communication.OperationParameters.Answer;
using DoorofSoul.Protocol.Communication.FetchDataParameters;
using DoorofSoul.Protocol.Communication.FetchDataParameters.Player;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Language;
using DoorofSoul.Client.Global;

namespace DoorofSoul.Client.Library.General
{
    public class ClientPlayer : Player
    {
        #region OnLogin
        private event Action<Player> onLogin;
        public event Action<Player> OnLogin
        {
            add { onLogin += value; }
            remove { onLogin -= value; }
        }
        #endregion
        #region OnErrorInform
        private event Action<string, string> onErrorInform;
        public event Action<string, string> OnErrorInform
        {
            add { onErrorInform += value; }
            remove { onErrorInform -= value; }
        }
        #endregion

        #region OnLogout
        private event Action onLogout;
        public event Action OnLogout
        {
            add { onLogout += value; }
            remove { onLogout -= value; }
        }
        #endregion
        public override bool ActivateSoul(Answer answer, int soulID)
        {
            Dictionary<byte, object> operationDataParameters = new Dictionary<byte, object>
            {
                { (byte)ActivateSoulOperationParameterCode.SoulID, soulID }
            };
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)AnswerOperationParameterCode.AnswerID, answer.AnswerID },
                { (byte)AnswerOperationParameterCode.OperationCode, AnswerOperationCode.ActivateSoul },
                { (byte)AnswerOperationParameterCode.Parameters, operationDataParameters }
            };
            SendOperation(PlayerOperationCode.AnswerOperation, parameters);
            return false;
        }

        public override bool CreateSoul(Answer answer, string soulName)
        {
            Dictionary<byte, object> operationDataParameters = new Dictionary<byte, object>
            {
                { (byte)CreateSoulOperationParameterCode.SoulName, soulName }
            };
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)AnswerOperationParameterCode.AnswerID, answer.AnswerID },
                { (byte)AnswerOperationParameterCode.OperationCode, AnswerOperationCode.CreateSoul },
                { (byte)AnswerOperationParameterCode.Parameters, operationDataParameters }
            };
            SendOperation(PlayerOperationCode.AnswerOperation, parameters);
            return false;
        }

        public override bool DeleteSoul(Answer answer, int soulID)
        {
            Dictionary<byte, object> operationDataParameters = new Dictionary<byte, object>
            {
                { (byte)DeleteSoulOperationParameterCode.SoulID, soulID }
            };
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)AnswerOperationParameterCode.AnswerID, answer.AnswerID },
                { (byte)AnswerOperationParameterCode.OperationCode, AnswerOperationCode.DeleteSoul },
                { (byte)AnswerOperationParameterCode.Parameters, operationDataParameters }
            };
            SendOperation(PlayerOperationCode.AnswerOperation, parameters);
            return false;
        }

        public override void ErrorInform(string title, string message)
        {
            if(onErrorInform != null)
            {
                onErrorInform(title, message);
            }
            else
            {
                SystemManager.Error("onErrorInform event is null");
            }
        }

        public override void FetchAnswer(out Answer answer)
        {
            answer = null;
            Dictionary<byte, object> fetchDataParameters = new Dictionary<byte, object>();
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)FetchDataParameterCode.FetchDataCode, PlayerFetchDataCode.Answer },
                { (byte)FetchDataParameterCode.Parameters, fetchDataParameters }
            };
            SendOperation(PlayerOperationCode.FetchData, parameters);
        }

        public override void FetchScene(int sceneID, out Scene scene)
        {
            scene = null;
            Dictionary<byte, object> fetchDataParameters = new Dictionary<byte, object>
            {
                { (byte)FetchSceneParameterCode.SceneID, sceneID }
            };
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)FetchDataParameterCode.FetchDataCode, PlayerFetchDataCode.Scene },
                { (byte)FetchDataParameterCode.Parameters, fetchDataParameters }
            };
            SendOperation(PlayerOperationCode.FetchData, parameters);
        }

        public override void FetchSceneResponse(int sceneID, string sceneName, int worldID)
        {
            Global.Global.Horizon.LoadScene(new Scene(sceneID, sceneName, worldID));
        }

        public override void FetchSystemVersion(out string serverVersion, out string clientVersion)
        {
            serverVersion = null;
            clientVersion = null;
            Dictionary<byte, object> fetchDataParameters = new Dictionary<byte, object>();
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)FetchDataParameterCode.FetchDataCode, PlayerFetchDataCode.SystemVersion },
                { (byte)FetchDataParameterCode.Parameters, fetchDataParameters }
            };
            SendOperation(PlayerOperationCode.FetchData, parameters);
        }

        public override void FetchSystemVersionResponse(string serverVersion, string clientVersion)
        {
            Global.Global.SystemManager.CurrentServerVersion = serverVersion;
            Global.Global.SystemManager.CurrentClientVersion = clientVersion;
        }

        public override bool Login(string account, string password, out string debugMessage, out ErrorCode errorCode)
        {
            debugMessage = null;
            errorCode = ErrorCode.NoError;
            var parameters = new Dictionary<byte, object>
            {
                { (byte)LoginParameterCode.Account, account },
                { (byte)LoginParameterCode.Password, password }
            };
            SendOperation(PlayerOperationCode.Login, parameters);
            return false;
        }

        public override void LoginResponse(int playerID, string account, string nickname, SupportLauguages usingLanguage, int answerID)
        {
            PlayerID = playerID;
            Account = account;
            Nickname = nickname;
            UsingLanguage = usingLanguage;
            AnswerID = answerID;
            if (onLogin != null)
            {
                onLogin(this);
            }
            else
            {
                LibraryLog.Error("onLogin event is null");
            }
        }

        public override void Logout()
        {
            var parameters = new Dictionary<byte, object>();
            SendOperation(PlayerOperationCode.Logout, parameters);
        }

        public override void LogoutResponse()
        {
            if(onLogout != null)
            {
                onLogout();
            }
            else
            {
                LibraryLog.Error("onLogout event is null");
            }
        }

        public override void SendEvent(PlayerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendOperation(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            Global.Global.PhotonService.SendPlayerOperation(PlayerID, operationCode, parameters);
        }

        public override void SendResponse(PlayerOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendWorldEvent(int worldID, WorldEventCode eventCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendWorldOperation(int worldID, WorldOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            Global.Global.PhotonService.SendWorldOperation(worldID, operationCode, parameters);
        }

        public override void SendWorldResponse(int worldID, WorldOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
