using DoorofSoul.Client.Library.General;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.ResponseParameters;
using ExitGames.Client.Photon;
using System;

namespace DoorofSoul.Client.Communication.Handlers.ResponseHandlers
{
    public class DeleteSoulResponseHandler : ResponseHandler
    {
        public override bool CheckError(OperationResponse operationResponse)
        {
            if (operationResponse.ReturnCode == (short)ErrorCode.NoError)
            {
                if (operationResponse.Parameters.Count != 0)
                {
                    Global.SystemManagers.DebugInformManager.DebugInform(string.Format("DeleteSoulResponse Parameter Error, Parameter Count: {0}", operationResponse.Parameters.Count));
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
                    Global.SystemManagers.DebugInformManager.DebugInform(string.Format("DeleteSoulResponseError Parameter Error, Parameter Count: {0}", operationResponse.Parameters.Count));
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
                        Global.SystemManagers.DebugInformManager.DebugInform("DeleteSoulError Parameter Cast Error");
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
                (Global.Player.Answer as ClientAnswer).ClearIncompleteSoulIDContainerIDConnection();
                Global.Player.Answer.ClearContainers();
                Global.Player.Answer.ClearSouls();
                Global.OperationManagers.FetchDataOperationManager.FetchSouls();
                Global.OperationManagers.FetchDataOperationManager.FetchContainers();
                Global.OperationManagers.FetchDataOperationManager.FetchSoulContainerConnections();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
