using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;

namespace DoorofSoul.Library.General.Responses.Handlers.Answer.FetchData
{
    public class FetchSoulsResponseHandler : FetchDataResponseHandler
    {
        public FetchSoulsResponseHandler(General.Answer answer) : base(answer)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            throw new NotImplementedException();
        }

        public override bool Handle(AnswerFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameter)
        {
            return base.Handle(fetchCode, returnCode, fetchDebugMessage, parameter);
        }
    }
}
