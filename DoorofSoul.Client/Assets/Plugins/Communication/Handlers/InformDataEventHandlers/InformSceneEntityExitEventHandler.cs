using DoorofSoul.Client.Library.General;
using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataParameters;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Client.Communication.Handlers.InformDataEventHandlers
{
    public class InformSceneEntityExitEventHandler : InformDataEventHandler
    {
        public override bool CheckError(Dictionary<byte, object> parameters, out string debugMessage)
        {
            if (parameters.Count != 2)
            {
                debugMessage = string.Format("Inform SceneEntityExit Event Parameter Error, Parameter Count: {0}", parameters.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(InformDataCode informCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(informCode, parameters))
            {
                try
                {
                    int entityID = (int)parameters[(byte)InformSceneEntityExitParameterCode.EntityID];
                    int locatedSceneID = (int)parameters[(byte)InformSceneEntityExitParameterCode.LocatedSceneID];
                    Global.SystemManagers.DebugInformManager.DebugInform(string.Format("Exit! entityID: {0} sceneID: {1}", entityID, locatedSceneID));
                    Global.ScenesManager.EntityExit(entityID, locatedSceneID);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    Global.SystemManagers.DebugInformManager.DebugInform("Inform SceneEntityEnter Event Parameter Cast Error");
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
            else
            {
                return false;
            }
        }
    }
}
