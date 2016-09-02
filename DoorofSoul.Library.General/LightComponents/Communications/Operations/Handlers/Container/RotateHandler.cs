using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Container;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Container
{
    internal class RotateHandler : ContainerOperationHandler
    {
        internal RotateHandler(NatureComponents.Container container) : base(container, 1)
        {
        }

        internal override bool Handle(ContainerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                int direction = (int)parameters[(byte)RotateParameterCode.Direction];
                direction = Math.Max(Math.Min(direction,1),-1);
                container.Entity.EntityEventManager.StartRotate(direction * 1);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
