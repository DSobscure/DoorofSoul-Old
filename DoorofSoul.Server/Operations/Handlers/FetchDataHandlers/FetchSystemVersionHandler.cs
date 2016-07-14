using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataParameters;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Server.Operations.Handlers.FetchDataHandlers
{
    public class FetchSystemVersionHandler : FetchDataHandler
    {
        public FetchSystemVersionHandler(Peer peer) : base(peer)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if(parameter.Count != 0)
            {
                debugMessage = string.Format("fetch system version has {0} parameters!", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(FetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            if (base.Handle(fetchCode, parameter))
            {
                try
                {
                    var result = new Dictionary<byte, object>
                    {
                        { (byte)InformSystemVersionParameterCode.CurrentServerVersion, Application.ServerInstance.SystemConfiguration.ServerVersion },
                        { (byte)InformSystemVersionParameterCode.CurrentClientVersion, Application.ServerInstance.SystemConfiguration.ClientVersion }
                    };
                    SendEvent((byte)InformDataCode.SystemVersion, result);
                    return true;
                }
                catch(InvalidCastException ex)
                {
                    Application.Log.ErrorFormat("Fetch System Version Invalid Cast!");
                    Application.Log.Error(ex.Message);
                    Application.Log.Error(ex.StackTrace);
                    return false;
                }
                catch(Exception ex)
                {
                    Application.Log.Error(ex.Message);
                    Application.Log.Error(ex.StackTrace);
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
