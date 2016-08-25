﻿using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.ResponseParameters.World;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers.World
{
    public class SceneOperationResponseResolver : WorldResponseHandler
    {
        public SceneOperationResponseResolver(NatureComponents.World world) : base(world)
        {

        }

        public override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            if (returnCode == ErrorCode.NoError)
            {
                if (parameters.Count != 5)
                {
                    LibraryInstance.ErrorFormat("Scene OperationResponse Parameter Error Parameter Count: {0}", parameters.Count);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                LibraryInstance.ErrorFormat("SceneOperationResponse Error ErrorCode: {0}, DebugMessage: {1}", returnCode, debugMessage);
                return false;
            }
        }

        public override bool Handle(WorldOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, returnCode, debugMessage, parameters))
            {
                try
                {
                    int sceneID = (int)parameters[(byte)SceneResponseParameterCode.SceneID];
                    SceneOperationCode resolvedOperationCode = (SceneOperationCode)parameters[(byte)SceneResponseParameterCode.OperationCode];
                    ErrorCode resolvedReturnCode = (ErrorCode)parameters[(byte)SceneResponseParameterCode.ReturnCode];
                    string resolvedDebugMessage = (string)parameters[(byte)SceneResponseParameterCode.DebugMessage];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)SceneResponseParameterCode.Parameters];
                    if (world.ContainsScene(sceneID))
                    {
                        world.FindScene(sceneID).SceneResponseManager.Operate(resolvedOperationCode, resolvedReturnCode, resolvedDebugMessage, resolvedParameters);
                        return true;
                    }
                    else
                    {
                        LibraryInstance.ErrorFormat("SceneOperationResponse Error Scene ID: {0} Not in World ID: {1}", sceneID, world.WorldID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("SceneOperationResponse Parameter Cast Error");
                    LibraryInstance.ErrorFormat(ex.Message);
                    LibraryInstance.ErrorFormat(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    LibraryInstance.ErrorFormat(ex.Message);
                    LibraryInstance.ErrorFormat(ex.StackTrace);
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