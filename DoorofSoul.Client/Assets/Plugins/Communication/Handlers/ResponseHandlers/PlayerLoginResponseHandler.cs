using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.ResponseParameters;
using DoorofSoul.Library.General;
using System.Net;

namespace DoorofSoul.Client.Communication.Handlers.ResponseHandlers
{
    public class PlayerLoginResponseHandler : ResponseHandler
    {
        public override bool CheckError(OperationResponse operationResponse)
        {
            if (operationResponse.ReturnCode == (short)ErrorCode.NoError)
            {
                if (operationResponse.Parameters.Count != 5)
                {
                    Global.SystemManagers.DebugInformManager.DebugInform(string.Format("PlayerLoginResponse Parameter Error, Parameter Count: {0}", operationResponse.Parameters.Count));
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (operationResponse.Parameters.Count != 1)
                {
                    Global.SystemManagers.DebugInformManager.DebugInform(string.Format("PlayerLoginResponseError Parameter Error, Parameter Count: {0}", operationResponse.Parameters.Count));
                    return false;
                }
                else
                {
                    try
                    {
                        string debugMessage = operationResponse.DebugMessage;
                        string errorMessage = (string)operationResponse.Parameters[(byte)OperationErrorResponseParameterCode.ErrorMessage];
                        Global.SystemManagers.DebugInformManager.DebugInform(debugMessage);
                        Global.SystemManagers.SystemInformManager.ErrorInform(errorMessage);
                        return false;
                    }
                    catch (InvalidCastException ex)
                    {
                        Global.SystemManagers.DebugInformManager.DebugInform("PlayerLoginError Parameter Cast Error");
                        Global.SystemManagers.DebugInformManager.DebugInform(ex.Message);
                        Global.SystemManagers.DebugInformManager.DebugInform(ex.StackTrace);
                        return false;
                    }
                    catch (Exception ex)
                    {
                        Global.SystemManagers.DebugInformManager.DebugInform(ex.Message);
                        Global.SystemManagers.DebugInformManager.DebugInform(ex.StackTrace);
                        return false;
                    }
                }
            }
        }

        public override bool Handle(OperationResponse operationResponse)
        {
            Global.ResponseManagers.UIResponseManager.PlayerLoginResult();
            if(base.Handle(operationResponse))
            {
                try
                {
                    int playerID = (int)operationResponse.Parameters[(byte)PlayerLoginResponseParameterCode.PlayerID];
                    string account = (string)operationResponse.Parameters[(byte)PlayerLoginResponseParameterCode.Account];
                    string nickname = (string)operationResponse.Parameters[(byte)PlayerLoginResponseParameterCode.Nickname];
                    SupportLauguages usingLanguage = (SupportLauguages)(byte)operationResponse.Parameters[(byte)PlayerLoginResponseParameterCode.UsingLanguageCode];
                    IPAddress lastConnectedIPAddress = IPAddress.None;
                    int answerID = (int)operationResponse.Parameters[(byte)PlayerLoginResponseParameterCode.AnswerID];
                    Player player = new Player(playerID, account, nickname, usingLanguage, lastConnectedIPAddress, answerID);
                    Global.ResponseManagers.ResponseManager.PlayerLogin(player);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    Global.SystemManagers.DebugInformManager.DebugInform("PlayerLogin Parameter Cast Error");
                    Global.SystemManagers.DebugInformManager.DebugInform(ex.Message);
                    Global.SystemManagers.DebugInformManager.DebugInform(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    Global.SystemManagers.DebugInformManager.DebugInform(ex.Message);
                    Global.SystemManagers.DebugInformManager.DebugInform(ex.StackTrace);
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

