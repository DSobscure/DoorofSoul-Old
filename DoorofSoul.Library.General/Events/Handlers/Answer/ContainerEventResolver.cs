using System;
using System.Collections.Generic;
using DoorofSoul.Protocol.Communication.EventCodes;

namespace DoorofSoul.Library.General.Events.Handlers.Answer
{
    public class ContainerEventResolver : AnswerEventHandler
    {
        public ContainerEventResolver(General.Answer answer) : base(answer)
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
