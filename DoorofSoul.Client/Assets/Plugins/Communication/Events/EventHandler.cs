using System;
using System.Collections.Generic;
using DoorofSoul.Protocol.Communication.EventCodes;
using ExitGames.Client.Photon;

namespace DoorofSoul.Client.Communication.Events
{
    public abstract class EventHandler
    {
        protected PhotonService photonService;
        protected EventHandler(PhotonService photonService)
        {
            this.photonService = photonService;
        }

        public virtual bool Handle(EventCode eventCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                photonService.DebugReturn(DebugLevel.ERROR, string.Format("Event Parameter Error DebugMessage: {0}", debugMessage));
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameters, out string debugMessage);
    }
}
