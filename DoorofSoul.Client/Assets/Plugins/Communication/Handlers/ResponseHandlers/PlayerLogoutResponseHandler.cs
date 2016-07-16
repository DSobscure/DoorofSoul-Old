using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.ResponseParameters;

namespace DoorofSoul.Client.Communication.Handlers.ResponseHandlers
{
    public class PlayerLogoutResponseHandler : ResponseHandler
    {
        public override bool CheckError(OperationResponse operationResponse)
        {
            if (operationResponse.ReturnCode == (short)ErrorCode.NoError)
            {
                if (operationResponse.Parameters.Count != 0)
                {
                    Global.SystemManagers.DebugInformManager.DebugInform(string.Format("PlayerLogoutResponse Parameter Error, Parameter Count: {0}", operationResponse.Parameters.Count));
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
                    Global.SystemManagers.DebugInformManager.DebugInform(string.Format("PlayerLogoutResponseError Parameter Error, Parameter Count: {0}", operationResponse.Parameters.Count));
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
                        Global.SystemManagers.DebugInformManager.DebugInform("PlayerLogoutError Parameter Cast Error");
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
            if(base.Handle(operationResponse))
            {
                Global.ResponseManagers.ResponseManager.PlayerLogout();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
