using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Protocol.Communication.OperationCodes;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Container
{
    internal class ShootABulletHandler : ContainerOperationHandler
    {
        internal ShootABulletHandler(NatureComponents.Container container) : base(container, 0)
        {
        }

        internal override bool Handle(ContainerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                return container.ShootABullet();
            }
            else
            {
                return false;
            }
        }
    }
}
