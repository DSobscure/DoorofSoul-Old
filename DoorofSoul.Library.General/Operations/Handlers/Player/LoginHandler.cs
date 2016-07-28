using System;
using System.Collections.Generic;
using System.Linq;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Player;
using DoorofSoul.Protocol.Communication.ResponseParameters.Player;

namespace DoorofSoul.Library.General.Operations.Handlers.Player
{
    public class LoginHandler : PlayerOperationHandler
    {
        public LoginHandler(General.Player player) : base(player)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 2)
            {
                debugMessage = string.Format("Login Operation Parameter Erro Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                string debugMessage;
                ErrorCode errorCode;
                string account = (string)parameters[(byte)LoginParameterCode.Account];
                string password = (string)parameters[(byte)LoginParameterCode.Password];
                bool result = player.Login(account, password, out debugMessage, out errorCode);
                if (result)
                {
                    Dictionary<byte, object> responseParameters = new Dictionary<byte, object>
                    {
                        { (byte)LoginResponseParameterCode.PlayerID, player.PlayerID },
                        { (byte)LoginResponseParameterCode.Account, player.Account },
                        { (byte)LoginResponseParameterCode.Nickname, player.Nickname },
                        { (byte)LoginResponseParameterCode.UsingLanguageCode, (byte)player.UsingLanguage },
                        { (byte)LoginResponseParameterCode.AnswerID, player.AnswerID }
                    };
                    SendResponse(operationCode, responseParameters);
                }
                else
                {
                    SendError(operationCode, errorCode, debugMessage);
                }
                return result;
            }
            else
            {
                return false;
            }
        }
    }
}
