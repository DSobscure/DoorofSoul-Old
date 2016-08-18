using System;
using System.Collections.Generic;
using System.Linq;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Player;
using DoorofSoul.Protocol.Communication.ResponseParameters.Player;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Player
{
    internal class LoginHandler : PlayerOperationHandler
    {
        internal LoginHandler(General.Player player) : base(player, 2)
        {
        }

        internal override bool Handle(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                string debugMessage;
                ErrorCode errorCode;
                string account = (string)parameters[(byte)LoginParameterCode.Account];
                string password = (string)parameters[(byte)LoginParameterCode.Password];
                bool result = player.PlayerCommunicationInterface.Login(account, password, out debugMessage, out errorCode);
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
