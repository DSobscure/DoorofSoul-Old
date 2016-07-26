using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;

namespace DoorofSoul.Library.General.Responses.Handlers.Player.FetchData
{
    public class FetchSystemVersionResponseHandler : FetchDataResponseHandler
    {
        public FetchSystemVersionResponseHandler(General.Player player) : base(player)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            throw new NotImplementedException();
        }

        public override bool Handle(PlayerFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameter)
        {
            return base.Handle(fetchCode, returnCode, fetchDebugMessage, parameter);
        }
    }
}
