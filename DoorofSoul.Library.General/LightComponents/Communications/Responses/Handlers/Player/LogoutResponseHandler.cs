using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Language;

namespace DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers.Player
{
    public class LogoutResponseHandler : PlayerResponseHandler
    {
        public LogoutResponseHandler(General.Player player) : base(player)
        {
        }

        internal override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            switch (returnCode)
            {
                case ErrorCode.NoError:
                    {
                        if (parameters.Count != 0)
                        {
                            LibraryInstance.ErrorFormat(string.Format("Logout Operation Parameter Error Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                case ErrorCode.InvalidOperation:
                    {
                        LibraryInstance.ErrorFormat("Logout Error DebugMessage: {0}", debugMessage);
                        player.PlayerEventManager.ErrorInform(LauguageDictionarySelector.Instance[player.UsingLanguage]["Invalid Operation"], LauguageDictionarySelector.Instance[player.UsingLanguage]["Logout Fail"]);
                        return false;
                    }
                default:
                    {
                        LibraryInstance.ErrorFormat("Logout Error DebugMessage: {0}", debugMessage);
                        player.PlayerEventManager.ErrorInform(LauguageDictionarySelector.Instance[player.UsingLanguage]["Unknown Error"], LauguageDictionarySelector.Instance[player.UsingLanguage]["Logout Fail"]);
                        return false;
                    }
            }
        }

        internal override bool Handle(PlayerOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, returnCode, debugMessage, parameters))
            {
                player.PlayerResponseManager.Logout();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
