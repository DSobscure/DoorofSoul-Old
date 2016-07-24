using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Scene;

namespace DoorofSoul.Library.General.Operations.Handlers.Scene
{
    public class EntityOperationResolver : SceneOperationHandler
    {
        public EntityOperationResolver(General.Scene scene) : base(scene)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 3)
            {
                debugMessage = string.Format("Entity Operation Parameter Error Parameter Count: {0}", parameter.Count);
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
                    int entityID = (int)parameters[(byte)EntityOperationParameterCode.EntityID];
                    EntityOperationCode resolvedOperationCode = (EntityOperationCode)parameters[(byte)EntityOperationParameterCode.OperationCode];
                    Dictionary<byte, object> resolvedOperationParameters = (Dictionary<byte, object>)parameters[(byte)EntityOperationParameterCode.Parameters];
                    if (scene.ContainsEntity(entityID))
                    {
                        scene.FindEntity(entityID).EntityOperationManager.Operate(resolvedOperationCode, resolvedOperationParameters);
                        return true;
                    }
                    else
                    {
                        LibraryLog.ErrorFormat("EntityOperation Error Entity ID: {0} Not in Scene ID: {1}", entityID, scene.SceneID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.ErrorFormat("EntityOperation Parameter Cast Error");
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
