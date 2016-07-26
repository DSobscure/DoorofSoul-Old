﻿using System;
using System.Collections.Generic;
using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.OperationCodes;

namespace DoorofSoul.Database.Library
{
    public class DatabaseWorld : World
    {
        public DatabaseWorld(int worldID, string worldName) : base(worldID, worldName)
        {
        }

        public override void SendEvent(WorldEventCode eventCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendOperation(WorldOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendResponse(WorldOperationCode operationCode, ErrorCode returnCode, string degugMessage, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
