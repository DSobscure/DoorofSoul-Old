using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.ResponseParameters.Scene;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers.Scene
{
    internal class EntityOperationResponseResolver : SceneResponseHandler
    {
        internal EntityOperationResponseResolver(NatureComponents.Scene scene) : base(scene)
        {
        }

        internal override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            if (returnCode == ErrorCode.NoError)
            {
                if (parameters.Count != 5)
                {
                    LibraryInstance.ErrorFormat("Entity OperationResponse Parameter Error Parameter Count: {0}", parameters.Count);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                LibraryInstance.ErrorFormat("EntityOperationResponse Error ErrorCode: {0}, DebugMessage: {1}", returnCode, debugMessage);
                return false;
            }
        }

        internal override bool Handle(SceneOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, returnCode, debugMessage, parameters))
            {
                try
                {
                    int entityID = (int)parameters[(byte)EntityResponseParameterCode.EntityID];
                    EntityOperationCode resolvedOperationCode = (EntityOperationCode)parameters[(byte)EntityResponseParameterCode.OperationCode];
                    ErrorCode resolvedReturnCode = (ErrorCode)parameters[(byte)EntityResponseParameterCode.ReturnCode];
                    string resolvedDebugMessage = (string)parameters[(byte)EntityResponseParameterCode.DebugMessage];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)EntityResponseParameterCode.Parameters];
                    if (scene.ContainsEntity(entityID))
                    {
                        scene.FindEntity(entityID).EntityResponseManager.Operate(resolvedOperationCode, resolvedReturnCode, resolvedDebugMessage, resolvedParameters);
                        return true;
                    }
                    else
                    {
                        LibraryInstance.ErrorFormat("EntityOperationResponse Error Entity ID: {0} Not in Scene ID: {1}", entityID, scene.SceneID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("EntityOperationResponse Parameter Cast Error");
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
