using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Player;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Player
{
    internal class RegisterHandler : PlayerOperationHandler
    {
        internal RegisterHandler(General.Player player) : base(player, 2)
        {
        }

        internal override bool Handle(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                string debugMessage;
                ErrorCode errorCode;
                string account = (string)parameters[(byte)RegisterParameterCode.Account];
                string password = (string)parameters[(byte)RegisterParameterCode.Password];
                if(account.Length < 2 || password.Length < 2)
                {
                    SendError(operationCode, ErrorCode.ParameterError, "account or password too short");
                    return false;
                }
                else
                {
                    bool result = player.PlayerCommunicationInterface.Register(account, password, out debugMessage, out errorCode);
                    if (result)
                    {
                        Dictionary<byte, object> responseParameters = new Dictionary<byte, object>();
                        SendResponse(operationCode, responseParameters);
                    }
                    else
                    {
                        SendError(operationCode, errorCode, debugMessage);
                    }
                    return result;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
