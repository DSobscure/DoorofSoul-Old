﻿using System;
using System.Collections.Generic;
using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Language;

namespace DoorofSoul.Client.Library.General
{
    public class ClientWorld : World
    {
        public ClientWorld(int worldID, string worldName) : base(worldID, worldName)
        {
        }

        public override SupportLauguages UsingLanguage
        {
            get
            {
                return Global.Global.Player.UsingLanguage;
            }
        }

        public override void ErrorInform(string title, string message)
        {
            throw new NotImplementedException();
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
