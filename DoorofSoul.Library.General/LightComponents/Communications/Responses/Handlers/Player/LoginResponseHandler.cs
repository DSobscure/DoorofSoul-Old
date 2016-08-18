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
    public class LoginResponseHandler : PlayerResponseHandler
    {
        public LoginResponseHandler(General.Player player) : base(player)
        {
        }

        public override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            switch (returnCode)
            {
                case ErrorCode.NoError:
                    {
                        if (parameters.Count != 5)
                        {
                            LibraryInstance.ErrorFormat(string.Format("LoginResponse Parameter Error, Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                case ErrorCode.Fail:
                    {
                        LibraryInstance.ErrorFormat("Login Error DebugMessage: {0}", debugMessage);
                        player.PlayerEventManager.ErrorInform(LauguageDictionarySelector.Instance[player.UsingLanguage]["Fail"], LauguageDictionarySelector.Instance[player.UsingLanguage]["Login Fail"]);
                        return false;
                    }
                case ErrorCode.InvalidOperation:
                    {
                        LibraryInstance.ErrorFormat("Login Error DebugMessage: {0}", debugMessage);
                        player.PlayerEventManager.ErrorInform(LauguageDictionarySelector.Instance[player.UsingLanguage]["Invalid Operation"], LauguageDictionarySelector.Instance[player.UsingLanguage]["Login InvalidOperation"]);
                        return false;
                    }
                default:
                    {
                        LibraryInstance.ErrorFormat("Login Error DebugMessage: {0}", debugMessage);
                        player.PlayerEventManager.ErrorInform(LauguageDictionarySelector.Instance[player.UsingLanguage]["Unknown Error"], LauguageDictionarySelector.Instance[player.UsingLanguage]["Login Error"]);
                        return false;
                    }
            }
        }

        public override bool Handle(PlayerOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, returnCode, debugMessage, parameters))
            {
                try
                {
                    int playerID = (int)parameters[(byte)LoginResponseParameterCode.PlayerID];
                    string account = (string)parameters[(byte)LoginResponseParameterCode.Account];
                    string nickname = (string)parameters[(byte)LoginResponseParameterCode.Nickname];
                    SupportLauguages usingLanguage = (SupportLauguages)(byte)parameters[(byte)LoginResponseParameterCode.UsingLanguageCode];
                    int answerID = (int)parameters[(byte)LoginResponseParameterCode.AnswerID];
                    player.LoadPlayer(playerID, account, nickname, usingLanguage, answerID);
                    player.IsOnline = true;
                    player.PlayerOperationManager.FetchDataResolver.FetchAnswer();
                    player.PlayerOperationManager.FetchDataResolver.FetchWorlds();
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    player.IsOnline = false;
                    LibraryInstance.Error("PlayerLogin Parameter Cast Error");
                    LibraryInstance.Error(ex.Message);
                    LibraryInstance.Error(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    player.IsOnline = false;
                    LibraryInstance.Error(ex.Message);
                    LibraryInstance.Error(ex.StackTrace);
                    return false;
                }
            }
            else
            {
                player.IsOnline = false;
                return false;
            }
        }
    }
}
