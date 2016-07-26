using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.ResponseParameters.Scene;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers.Scene
{
    public class EntityOperationResponseResolver : SceneResponseHandler
    {
        public EntityOperationResponseResolver(General.Scene scene) : base(scene)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 5)
            {
                debugMessage = string.Format("Entity OperationResponse Parameter Error Parameter Count: {0}", parameter.Count);
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
                    int entityID = (int)parameters[(byte)EntityResponseParameterCode.EntityID];
                    EntityOperationCode resolvedOperationCode = (EntityOperationCode)parameters[(byte)EntityResponseParameterCode.OperationCode];
                    ErrorCode returnCode = (ErrorCode)parameters[(byte)EntityResponseParameterCode.ReturnCode];
                    string debugMessage = (string)parameters[(byte)EntityResponseParameterCode.DebugMessage];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)EntityResponseParameterCode.Parameters];
                    if (scene.ContainsEntity(entityID))
                    {
                        scene.FindEntity(entityID).EntityResponseManager.Operate(resolvedOperationCode, returnCode, debugMessage, resolvedParameters);
                        return true;
                    }
                    else
                    {
                        LibraryLog.ErrorFormat("EntityOperationResponse Error Entity ID: {0} Not in Scene ID: {1}", entityID, scene.SceneID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.ErrorFormat("EntityOperationResponse Parameter Cast Error");
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
