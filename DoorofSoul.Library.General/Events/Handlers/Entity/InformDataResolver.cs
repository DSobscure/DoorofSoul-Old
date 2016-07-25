using DoorofSoul.Protocol.Communication.EventCodes;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Entity
{
    public class InformDataResolver : EntityEventHandler
    {
        public InformDataResolver(General.Entity entity) : base(entity)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            throw new NotImplementedException();
        }

        public override bool Handle(EntityEventCode eventCode, Dictionary<byte, object> parameters)
        {
            return base.Handle(eventCode, parameters);
        }
    }
}
