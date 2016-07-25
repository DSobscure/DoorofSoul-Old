using DoorofSoul.Protocol.Communication.EventCodes;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Answer
{
    public class SoulEventResolver : AnswerEventHandler
    {
        public SoulEventResolver(General.Answer answer) : base(answer)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            throw new NotImplementedException();
        }

        public override bool Handle(AnswerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            return base.Handle(eventCode, parameters);
        }
    }
}
