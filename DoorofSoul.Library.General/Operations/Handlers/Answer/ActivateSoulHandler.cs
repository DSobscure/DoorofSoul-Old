﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication.OperationCodes;

namespace DoorofSoul.Library.General.Operations.Handlers.Answer
{
    public class ActivateSoulHandler : AnswerOperationHandler
    {
        public ActivateSoulHandler(General.Answer answer) : base(answer)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            throw new NotImplementedException();
        }

        public override bool Handle(AnswerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            return base.Handle(operationCode, parameters);
        }
    }
}
