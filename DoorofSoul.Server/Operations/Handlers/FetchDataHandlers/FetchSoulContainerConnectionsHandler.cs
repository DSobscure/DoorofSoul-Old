using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataParameters;
using DoorofSoul.Library.General;

namespace DoorofSoul.Server.Operations.Handlers.FetchDataHandlers
{
    public class FetchSoulContainerConnectionsHandler : FetchDataHandler
    {
        public FetchSoulContainerConnectionsHandler(Peer peer) : base(peer)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 0)
            {
                debugMessage = string.Format("fetch soul container connection has {0} parameters!", parameter.Count);
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
                    foreach(Soul soul in peer.Player.Answer.Souls)
                    {
                        foreach(Container container in soul.Containers)
                        {
                            var result = new Dictionary<byte, object>
                            {
                                { (byte)InformSoulContainerConnectionParameterCode.SoulID, soul.SoulID },
                                { (byte)InformSoulContainerConnectionParameterCode.ContainerID, container.ContainerID }
                            };
                            SendEvent((byte)InformDataCode.SoulContainerConnection, result);
                        }
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    Application.Log.ErrorFormat("Fetch Soul Container Connections Invalid Cast!");
                    Application.Log.Error(ex.Message);
                    Application.Log.Error(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
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
