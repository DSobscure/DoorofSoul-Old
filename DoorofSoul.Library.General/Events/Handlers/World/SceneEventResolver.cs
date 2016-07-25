﻿using DoorofSoul.Protocol.Communication.EventCodes;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.World
{
    partial class SceneEventResolver : WorldEventHandler
    {
        public SceneEventResolver(General.World world) : base(world)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            throw new NotImplementedException();
        }

        public override bool Handle(WorldEventCode eventCode, Dictionary<byte, object> parameters)
        {
            return base.Handle(eventCode, parameters);
        }
    }
}
