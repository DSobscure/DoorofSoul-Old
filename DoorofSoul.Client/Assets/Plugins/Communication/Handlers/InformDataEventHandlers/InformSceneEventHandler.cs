using DoorofSoul.Client.Library.General;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataParameters;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Client.Communication.Handlers.InformDataEventHandlers
{
    public class InformSceneEventHandler : InformDataEventHandler
    {
        public override bool CheckError(Dictionary<byte, object> parameters, out string debugMessage)
        {
            if (parameters.Count != 3)
            {
                debugMessage = string.Format("Inform Scene Event Parameter Error, Parameter Count: {0}", parameters.Count);
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
                    int sceneID = (int)parameters[(byte)InformSceneParameterCode.SceneID];
                    string sceneName = (string)parameters[(byte)InformSceneParameterCode.SceneName];
                    int worldID = (int)parameters[(byte)InformSceneParameterCode.WorldID];
                    Global.ScenesManager.LoadScene(new ClientScene(sceneID, sceneName, worldID));
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    Global.SystemManagers.DebugInformManager.DebugInform("Inform Scene Event Parameter Cast Error");
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
