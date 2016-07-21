using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataParameters;
using System;
using System.Collections.Generic;
using DoorofSoul.Client.Library.General;

namespace DoorofSoul.Client.Communication.Handlers.InformDataEventHandlers
{
    public class InformAnswerEventHandler : InformDataEventHandler
    {
        public override bool CheckError(Dictionary<byte, object> parameters, out string debugMessage)
        {
            if (parameters.Count != 2)
            {
                debugMessage = string.Format("Inform Answer Event Parameter Error, Parameter Count: {0}", parameters.Count);
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
                    int answerID = (int)parameters[(byte)InformAnswerParameterCode.AnswerID];
                    int soulCountLimit = (int)parameters[(byte)InformAnswerParameterCode.SoulCountLimit];
                    Global.Player.ActiveAnswer(new ClientAnswer(answerID, soulCountLimit, Global.Player));
                    Global.OperationManagers.FetchDataOperationManager.FetchSouls();
                    Global.OperationManagers.FetchDataOperationManager.FetchContainers();
                    Global.OperationManagers.FetchDataOperationManager.FetchSoulContainerConnections();
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
