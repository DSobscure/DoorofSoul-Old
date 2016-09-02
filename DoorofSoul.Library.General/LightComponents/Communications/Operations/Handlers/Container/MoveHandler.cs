using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Container;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Container
{
    internal class MoveHandler : ContainerOperationHandler
    {
        internal MoveHandler(NatureComponents.Container container) : base(container, 1)
        {
        }

        internal override bool Handle(ContainerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                int direction = (int)parameters[(byte)MoveParameterCode.Direction];
                direction = Math.Max(Math.Min(direction, 1), -1);
                container.Entity.EntityEventManager.StartMove(direction * 5 * (1 + container.ShooterAbilities.MoveSpeed * 0.3f));
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
