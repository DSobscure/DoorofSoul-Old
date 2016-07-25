using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Entity.InformData
{
    public class InformFetchDataErrorHandler : InformDataHandler
    {
        public InformFetchDataErrorHandler(General.Entity entity) : base(entity)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            throw new NotImplementedException();
        }

        public override bool Handle(EntityInformDataCode informCode, ErrorCode returnCode, Dictionary<byte, object> parameter)
        {
            return base.Handle(informCode, returnCode, parameter);
        }
    }
}
