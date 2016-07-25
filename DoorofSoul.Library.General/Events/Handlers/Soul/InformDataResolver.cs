using DoorofSoul.Protocol.Communication.EventCodes;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Soul
{
    public class InformDataResolver : SoulEventHandler
    {
        public InformDataResolver(General.Soul soul) : base(soul)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            throw new NotImplementedException();
        }

        public override bool Handle(SoulEventCode eventCode, Dictionary<byte, object> parameters)
        {
            return base.Handle(eventCode, parameters);
        }
    }
}
