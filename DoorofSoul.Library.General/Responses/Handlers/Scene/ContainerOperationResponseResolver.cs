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

        public override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            if (returnCode == ErrorCode.NoError)
            {
                if (parameters.Count != 5)
                {
                    LibraryLog.ErrorFormat("Container OperationResponse Parameter Error Parameter Count: {0}", parameters.Count);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                LibraryLog.ErrorFormat("ContainerOperationResponse Error ErrorCode: {0}, DebugMessage: {1}", returnCode, debugMessage);
                return false;
            }
        }

        public override bool Handle(SceneOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, returnCode, debugMessage, parameters))
            {
                try
                {
                    int containerID = (int)parameters[(byte)ContainerResponseParameterCode.ContainerID];
                    ContainerOperationCode resolvedOperationCode = (ContainerOperationCode)parameters[(byte)ContainerResponseParameterCode.OperationCode];
                    ErrorCode resolvedReturnCode = (ErrorCode)parameters[(byte)ContainerResponseParameterCode.ReturnCode];
                    string resolvedDebugMessage = (string)parameters[(byte)ContainerResponseParameterCode.DebugMessage];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)ContainerResponseParameterCode.Parameters];
                    if (scene.ContainsContainer(containerID))
                    {
                        scene.FindContainer(containerID).ContainerResponseManager.Operate(resolvedOperationCode, resolvedReturnCode, resolvedDebugMessage, resolvedParameters);
                        return true;
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
