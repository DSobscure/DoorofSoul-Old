using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.ResponseParameters.Scene;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers.Scene
{
    public class ContainerOperationResponseResolver : SceneResponseHandler
    {
        public ContainerOperationResponseResolver(General.Scene scene) : base(scene)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 5)
            {
                debugMessage = string.Format("Container OperationResponse Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(SceneOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                try
                {
                    int containerID = (int)parameters[(byte)ContainerResponseParameterCode.ContainerID];
                    ContainerOperationCode resolvedOperationCode = (ContainerOperationCode)parameters[(byte)ContainerResponseParameterCode.OperationCode];
                    ErrorCode returnCode = (ErrorCode)parameters[(byte)ContainerResponseParameterCode.ReturnCode];
                    string debugMessage = (string)parameters[(byte)ContainerResponseParameterCode.DebugMessage];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)ContainerResponseParameterCode.Parameters];
                    if (scene.ContainsContainer(containerID))
                    {
                        if (returnCode == ErrorCode.NoError)
                        {
                            scene.FindContainer(containerID).ContainerResponseManager.Operate(resolvedOperationCode, resolvedParameters);
                            return true;
                        }
                        else
                        {
                            LibraryLog.ErrorFormat("ContainerOperationResponse Error Container ID: {0} ErrorCode: {1}, DebugMessage: {2}", containerID, returnCode, debugMessage);
                            return false;
                        }
                    }
                    else
                    {
                        LibraryLog.ErrorFormat("ContainerOperationResponse Error Container ID: {0} Not in Scene ID: {1}", containerID, scene.SceneID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.ErrorFormat("ContainerOperationResponse Parameter Cast Error");
                    LibraryLog.ErrorFormat(ex.Message);
                    LibraryLog.ErrorFormat(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    LibraryLog.ErrorFormat(ex.Message);
                    LibraryLog.ErrorFormat(ex.StackTrace);
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
