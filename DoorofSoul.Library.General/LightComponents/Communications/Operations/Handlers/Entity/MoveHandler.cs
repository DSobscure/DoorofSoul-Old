using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Entity;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Entity
{
    internal class MoveHandler : EntityOperationHandler
    {
        internal MoveHandler(NatureComponents.Entity entity) : base(entity, 1)
        {
        }

        internal override bool Handle(EntityOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                int direction = (int)parameters[(byte)MoveParameterCode.Direction];
                direction = Math.Max(Math.Min(direction, 1), -1);
                entity.EntityEventManager.StartMove(direction * 5);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
