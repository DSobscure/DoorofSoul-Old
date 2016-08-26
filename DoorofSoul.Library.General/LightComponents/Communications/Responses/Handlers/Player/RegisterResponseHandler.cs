using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Language;
using DoorofSoul.Protocol.Communication.ResponseParameters.Player;

namespace DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers.Player
{
    internal class RegisterResponseHandler : PlayerResponseHandler
    {
        internal RegisterResponseHandler(General.Player player) : base(player)
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
                            LibraryInstance.ErrorFormat(string.Format("LoginResponse Parameter Error, Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                case ErrorCode.AlreadyExisted:
                    {
                        LibraryInstance.ErrorFormat("Register Error DebugMessage: {0}", debugMessage);
                        player.PlayerEventManager.ErrorInform(LauguageDictionarySelector.Instance[player.UsingLanguage]["Fail"], LauguageDictionarySelector.Instance[player.UsingLanguage]["Account Used"]);
                        return false;
                    }
                case ErrorCode.ParameterError:
                    {
                        LibraryInstance.ErrorFormat("Register Error DebugMessage: {0}", debugMessage);
                        player.PlayerEventManager.ErrorInform(LauguageDictionarySelector.Instance[player.UsingLanguage]["Fail"], LauguageDictionarySelector.Instance[player.UsingLanguage]["AccountOrPasswordInvalid"]);
                        return false;
                    }
                default:
                    {
                        LibraryInstance.ErrorFormat("Register Error DebugMessage: {0}", debugMessage);
                        player.PlayerEventManager.ErrorInform(LauguageDictionarySelector.Instance[player.UsingLanguage]["Unknown Error"], LauguageDictionarySelector.Instance[player.UsingLanguage]["Register Error"]);
                        return false;
                    }
            }
        }

        internal override bool Handle(PlayerOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, returnCode, debugMessage, parameters))
            {
                try
                {
                    player.PlayerEventManager.ErrorInform("完成","註冊成功");
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("PlayerRegister Parameter Cast Error");
                    LibraryInstance.Error(ex.Message);
                    LibraryInstance.Error(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    LibraryInstance.Error(ex.Message);
                    LibraryInstance.Error(ex.StackTrace);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
