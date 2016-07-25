using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters.Scene;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Scene
{
    public class EntityEventResolver : SceneEventHandler
    {
        public EntityEventResolver(General.Scene scene) : base(scene)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 3)
            {
                debugMessage = string.Format("Entity Event Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(SceneEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(eventCode, parameters))
            {
                try
                {
                    int entityID = (int)parameters[(byte)EntityEventParameterCode.EntityID];
                    EntityEventCode resolvedEventCode = (EntityEventCode)parameters[(byte)EntityEventParameterCode.EventCode];
                    Dictionary<byte, object> resolvedOperationParameters = (Dictionary<byte, object>)parameters[(byte)EntityEventParameterCode.Parameters];
                    if (scene.ContainsEntity(entityID))
                    {
                        scene.FindEntity(entityID).EntityEventManager.Operate(resolvedEventCode, resolvedOperationParameters);
                        return true;
                    }
                    else
                    {
                        LibraryLog.ErrorFormat("EntityEvent Error Entity ID: {0} Not in Scene ID: {1}", entityID, scene.SceneID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.ErrorFormat("EntityEvent Parameter Cast Error");
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
