using DoorofSoul.Client.Library.General;
using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataParameters;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Client.Communication.Handlers.InformDataEventHandlers
{
    public class InformSoulContainerConnectionEventHandler : InformDataEventHandler
    {
        public override bool CheckError(Dictionary<byte, object> parameters, out string debugMessage)
        {
            if (parameters.Count != 2)
            {
                debugMessage = string.Format("Inform Soul Container Connection Event Parameter Error, Parameter Count: {0}", parameters.Count);
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
                    int soulID = (int)parameters[(byte)InformSoulContainerConnectionParameterCode.SoulID];
                    int containerID = (int)parameters[(byte)InformSoulContainerConnectionParameterCode.ContainerID];
                    if(Global.Player.Answer.ContainsSoul(soulID) && Global.Player.Answer.ContainsContainer(containerID))
                    {
                        Soul soul = Global.Player.Answer.FindSoul(soulID);
                        Container container = Global.Player.Answer.FindContainer(containerID);
                        soul.LinkContainer(container);
                        container.LinkSoul(soul);
                    }
                    else
                    {
                        (Global.Player.Answer as ClientAnswer).RecordIncompleteSoulIDContainerIDConnection(soulID, containerID);
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    Global.SystemManagers.DebugInformManager.DebugInform("Inform System Version Event Parameter Cast Error");
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
