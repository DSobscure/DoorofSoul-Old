using System;
using System.Collections.Generic;
using DoorofSoul.Protocol.Communication.EventCodes;

namespace DoorofSoul.Library.General.Events.Handlers.Player
{
    public class InformDataResolver : PlayerEventHandler
    {
        public InformDataResolver(General.Player player) : base(player)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            throw new NotImplementedException();
        }

        public override bool Handle(PlayerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            return base.Handle(eventCode, parameters);
        }
    }
}
