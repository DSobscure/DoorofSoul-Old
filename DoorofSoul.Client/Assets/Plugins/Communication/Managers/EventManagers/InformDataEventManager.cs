using DoorofSoul.Client.Communication.Handlers;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventParameters;
using DoorofSoul.Protocol.Communication.InformDataParameters;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Client.Communication.Managers.EventManagers
{
    public class InformDataEventManager : Handlers.EventHandler
    {
        protected readonly Dictionary<InformDataCode, InformDataEventHandler> informTable;

        public InformDataEventManager()
        {
            informTable = new Dictionary<InformDataCode, InformDataEventHandler>
            {

            };
        }
        public override bool CheckParameter(EventData eventData, out string debugMessage)
        {
            if (eventData.Parameters.Count != 3)
            {
                debugMessage = "Inform Data Operation Parameter Error";
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(EventData eventData)
        {
            if(base.Handle(eventData))
            {
                try
                {
                    InformDataCode informCode = (InformDataCode)eventData.Parameters[(byte)InformDataEventParameterCode.InformCode];
                    ErrorCode returnCode = (ErrorCode)eventData.Parameters[(byte)InformDataEventParameterCode.ReturnCode];
                    Dictionary<byte, object> parameters = (Dictionary<byte, object>)eventData.Parameters[(byte)InformDataEventParameterCode.Parameters];
                    if (returnCode == ErrorCode.NoError)
                    {
                        if (informTable.ContainsKey(informCode))
                        {
                            return informTable[informCode].Handle(informCode, parameters);
                        }
                        else
                        {
                            Global.SystemManagers.DebugInformManager.DebugInform(string.Format("inform data event not exist"));
                            return false;
                        }
                    }
                    else if (informCode == InformDataCode.FetchDataError)
                    {
                        FetchDataCode fetchCode = (FetchDataCode)parameters[(byte)InformFetchDataErrorParameterCode.FetchDataCode];
                        string debugMessage = (string)parameters[(byte)InformFetchDataErrorParameterCode.DebugMessage];
                        string errorMessage = (string)parameters[(byte)InformFetchDataErrorParameterCode.ErrorMessage];
                        Global.SystemManagers.DebugInformManager.DebugInform(string.Format("Fetch Data Error Code: {0}, DebugMessage", fetchCode, debugMessage));
                        Global.SystemManagers.SystemInformManager.ErrorInform(errorMessage);
                        return false;
                    }
                    else
                    {
                        Global.SystemManagers.DebugInformManager.DebugInform(string.Format("Fatal Error Inform Code:{0}", informCode));
                        return false;
                    }
                }
                catch(InvalidCastException ex)
                {
                    Global.SystemManagers.DebugInformManager.DebugInform("Inform Data Event Cast Error");
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
