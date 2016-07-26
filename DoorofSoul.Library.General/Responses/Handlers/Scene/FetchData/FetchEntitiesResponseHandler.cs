using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;

namespace DoorofSoul.Library.General.Responses.Handlers.Scene.FetchData
{
    public class FetchEntitiesResponseHandler : FetchDataResponseHandler
    {
        public FetchEntitiesResponseHandler(General.Scene scene) : base(scene)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            throw new NotImplementedException();
        }

        public override bool Handle(SceneFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameter)
        {
            return base.Handle(fetchCode, returnCode, fetchDebugMessage, parameter);
        }
    }
}
