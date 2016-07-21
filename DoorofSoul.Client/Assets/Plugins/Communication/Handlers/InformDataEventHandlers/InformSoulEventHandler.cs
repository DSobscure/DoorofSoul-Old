using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataParameters;
using System;
using System.Collections.Generic;
using DoorofSoul.Library.General;

namespace DoorofSoul.Client.Communication.Handlers.InformDataEventHandlers
{
    public class InformSoulEventHandler : InformDataEventHandler
    {
        public override bool CheckError(Dictionary<byte, object> parameters, out string debugMessage)
        {
            if (parameters.Count != 3)
            {
                debugMessage = string.Format("Inform Soul Event Parameter Error, Parameter Count: {0}", parameters.Count);
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
                    int soulID = (int)parameters[(byte)InformSoulParameterCode.SoulID];
                    int answerID = (int)parameters[(byte)InformSoulParameterCode.AnswerID];
                    string soulName = (string)parameters[(byte)InformSoulParameterCode.SoulName];
                    Global.Player.Answer.LoadSouls(new List<Soul> { new Soul(soulID, answerID, soulName) });
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
