using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Client.Communication.Events.Handlers
{
    public class WorldEventResolver : EventHandler
    {
        public WorldEventResolver(PhotonService photonService) : base(photonService)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameters, out string debugMessage)
        {
            if (parameters.Count != 3)
            {
                debugMessage = string.Format("World Event Parameter Error Parameter Count: {0}", parameters.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(EventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(eventCode, parameters))
            {
                try
                {
                    int worldID = (int)parameters[(byte)EventParameterCode.ID];
                    WorldEventCode resolvedEventCode = (WorldEventCode)parameters[(byte)EventParameterCode.EventCode];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)EventParameterCode.Parameters];
                    if (worldID == Global.Global.World.WorldID)
                    {
                        Global.Global.World.WorldEventManager.Operate(resolvedEventCode, resolvedParameters);
                        return true;
                    }
                    else
                    {
                        photonService.DebugReturn(DebugLevel.ERROR, string.Format("WorldEvent Error WorldID: {0} Not In Peer", worldID));
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    photonService.DebugReturn(DebugLevel.ERROR, "WorldEvent Parameter Cast Error");
                    photonService.DebugReturn(DebugLevel.ERROR, ex.Message);
                    photonService.DebugReturn(DebugLevel.ERROR, ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    photonService.DebugReturn(DebugLevel.ERROR, ex.Message);
                    photonService.DebugReturn(DebugLevel.ERROR, ex.StackTrace);
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
