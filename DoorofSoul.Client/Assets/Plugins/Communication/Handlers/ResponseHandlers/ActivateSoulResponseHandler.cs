using DoorofSoul.Client.Library.General;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.ResponseParameters;
using ExitGames.Client.Photon;
using System;

namespace DoorofSoul.Client.Communication.Handlers.ResponseHandlers
{
    public class ActivateSoulResponseHandler : ResponseHandler
    {
        public override bool CheckError(OperationResponse operationResponse)
        {
            if (operationResponse.ReturnCode == (short)ErrorCode.NoError)
            {
                if (operationResponse.Parameters.Count != 2)
                {
                    Global.SystemManagers.DebugInformManager.DebugInform(string.Format("ActivateSoulResponse Parameter Error, Parameter Count: {0}", operationResponse.Parameters.Count));
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
                    Global.SystemManagers.DebugInformManager.DebugInform(string.Format("ActivateSoulResponseError Parameter Error, Parameter Count: {0}", operationResponse.Parameters.Count));
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
                        Global.SystemManagers.DebugInformManager.DebugInform("ActivateSoulError Parameter Cast Error");
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
            if (base.Handle(operationResponse))
            {
                try
                {
                    int soulID = (int)operationResponse.Parameters[(byte)ActiveSoulResponseParameterCode.SoulID];
                    int sceneID = (int)operationResponse.Parameters[(byte)ActiveSoulResponseParameterCode.SceneID];
                    if (Global.Player.Answer.ContainsSoul(soulID))
                    {
                        Global.ScenesManager.EnterScene(sceneID);
                        return true;
                    }
                    else
                    {
                        Global.SystemManagers.DebugInformManager.DebugInform("ActiveSoulResponseError Soul Not Exist");
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    Global.SystemManagers.DebugInformManager.DebugInform("ActiveSoul Parameter Cast Error");
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
