using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataParameters;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Client.Communication.Handlers.InformDataEventHandlers
{
    public class InformSystemVersionEventHandler : InformDataEventHandler
    {
        public override bool CheckError(Dictionary<byte, object> parameters, out string debugMessage)
        {
            if(parameters.Count != 2)
            {
                debugMessage = string.Format("Inform System Version Event Parameter Error, Parameter Count: {0}", parameters.Count);
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
            if(base.Handle(informCode, parameters))
            {
                try
                {
                    string currentServerVersion = (string)parameters[(byte)InformSystemVersionParameterCode.CurrentServerVersion];
                    string currentClientVersion = (string)parameters[(byte)InformSystemVersionParameterCode.CurrentClientVersion];
                    Global.VersionManager.CurrentServerVersion = currentServerVersion;
                    Global.VersionManager.CurrentClientVersion = currentClientVersion;
                    return true;
                }
                catch(InvalidCastException ex)
                {
                    Global.SystemManagers.DebugInformManager.DebugInform("Inform System Version Event Parameter Cast Error");
                    Global.SystemManagers.DebugInformManager.DebugInform(ex.Message);
                    Global.SystemManagers.DebugInformManager.DebugInform(ex.StackTrace);
                    return false;
                }
                catch(Exception ex)
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
