using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters.World;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.World
{
    partial class SceneEventResolver : WorldEventHandler
    {
        public SceneEventResolver(General.World world) : base(world)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 3)
            {
                debugMessage = string.Format("Scene Event Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(WorldEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(eventCode, parameters))
            {
                try
                {
                    int sceneID = (int)parameters[(byte)SceneEventParameterCode.SceneID];
                    SceneEventCode resolvedEventCode = (SceneEventCode)parameters[(byte)SceneEventParameterCode.EventCode];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)SceneEventParameterCode.Parameters];
                    if (world.ContainsScene(sceneID))
                    {
                        world.FindScene(sceneID).SceneEventManager.Operate(resolvedEventCode, resolvedParameters);
                        return true;
                    }
                    else
                    {
                        LibraryLog.ErrorFormat("SceneEvent Error Scene ID: {0} Not in World ID: {1}", sceneID, world.WorldID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.ErrorFormat("SceneEvent Parameter Cast Error");
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
