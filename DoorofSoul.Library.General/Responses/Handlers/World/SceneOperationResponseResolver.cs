using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.ResponseParameters.World;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers.World
{
    public class SceneOperationResponseResolver : WorldResponseHandler
    {
        public SceneOperationResponseResolver(General.World world) : base(world)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 5)
            {
                debugMessage = string.Format("Scene OperationResponse Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(WorldOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                try
                {
                    int sceneID = (int)parameters[(byte)SceneResponseParameterCode.SceneID];
                    SceneOperationCode resolvedOperationCode = (SceneOperationCode)parameters[(byte)SceneResponseParameterCode.OperationCode];
                    ErrorCode returnCode = (ErrorCode)parameters[(byte)SceneResponseParameterCode.ReturnCode];
                    string debugMessage = (string)parameters[(byte)SceneResponseParameterCode.DebugMessage];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)SceneResponseParameterCode.Parameters];
                    if (world.ContainsScene(sceneID))
                    {
                        world.FindScene(sceneID).SceneResponseManager.Operate(resolvedOperationCode, returnCode, debugMessage, resolvedParameters);
                        return true;
                    }
                    else
                    {
                        LibraryLog.ErrorFormat("SceneOperationResponse Error Scene ID: {0} Not in World ID: {1}", sceneID, world.WorldID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.ErrorFormat("SceneOperationResponse Parameter Cast Error");
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
