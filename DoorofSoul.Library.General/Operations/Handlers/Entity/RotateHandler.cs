using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Entity;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers.Entity
{
    internal class RotateHandler : EntityOperationHandler
    {
        internal RotateHandler(General.Entity entity) : base(entity, 1)
        {
        }

        internal override bool Handle(EntityOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                int direction = (int)parameters[(byte)RotateParameterCode.Direction];
                direction = Math.Max(Math.Min(direction,1),-1);
                entity.EntityEventManager.StartRotate(direction * 1);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
