﻿using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers
{
    public abstract class EntityResponseHandler
    {
        protected NatureComponents.Entity entity;

        protected EntityResponseHandler(NatureComponents.Entity entity)
        {
            this.entity = entity;
        }

        public virtual bool Handle(EntityOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (CheckError(parameters, returnCode, debugMessage))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public abstract bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage);
    }
}
