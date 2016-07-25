using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Player.InformData
{
    public class InformSystemVersionHandler : InformDataHandler
    {
        public InformSystemVersionHandler(General.Player player) : base(player)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            throw new NotImplementedException();
        }

        public override bool Handle(PlayerInformDataCode informCode, ErrorCode returnCode, Dictionary<byte, object> parameter)
        {
            return base.Handle(informCode, returnCode, parameter);
        }
    }
}
