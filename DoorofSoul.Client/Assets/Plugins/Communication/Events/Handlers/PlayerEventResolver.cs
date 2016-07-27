using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Client.Communication.Events.Handlers
{
    public class PlayerEventResolver : EventHandler
    {
        public PlayerEventResolver(PhotonService photonService) : base(photonService)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameters, out string debugMessage)
        {
            if (parameters.Count != 3)
            {
                debugMessage = string.Format("Player Event Parameter Error Parameter Count: {0}", parameters.Count);
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
                    int playerID = (int)parameters[(byte)EventParameterCode.ID];
                    PlayerEventCode resolvedEventCode = (PlayerEventCode)parameters[(byte)EventParameterCode.EventCode];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)EventParameterCode.Parameters];
                    if (playerID == Global.Global.Player.PlayerID)
                    {
                        Global.Global.Player.PlayerEventManager.Operate(resolvedEventCode, resolvedParameters);
                        return true;
                    }
                    else
                    {
                        photonService.DebugReturn(DebugLevel.ERROR, string.Format("PlayerEvent Error PlayerID: {0} Not In Peer", playerID));
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    photonService.DebugReturn(DebugLevel.ERROR, "PlayerEvent Parameter Cast Error");
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
