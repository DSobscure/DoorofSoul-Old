using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;

namespace DoorofSoul.Library.General.Responses.Handlers.Answer
{
    public class CreateSoulResponseHandler : AnswerResponseHandler
    {
        public CreateSoulResponseHandler(General.Answer answer) : base(answer)
        {
        }

        public override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            throw new NotImplementedException();
        }

        public override bool Handle(AnswerOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            return base.Handle(operationCode, returnCode, debugMessage, parameters);
        }
    }
}
