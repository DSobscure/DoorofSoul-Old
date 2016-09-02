using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.InformDataParameters.Container;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Container.InformData
{
    internal class InformBulletDamageChangeHandler : InformDataHandler
    {
        internal InformBulletDamageChangeHandler(NatureComponents.Container container) : base(container, 1)
        {
        }

        internal override bool Handle(ContainerInformDataCode informCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(informCode, parameters))
            {
                try
                {
                    int damage = (int)parameters[(byte)InformBulletDamageChangeParameterCode.Damage];
                    container.ShooterAbilities.Damage = damage;
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("InformBulletDamageChange Parameter Cast Error");
                    LibraryInstance.Error(ex.Message);
                    LibraryInstance.Error(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    LibraryInstance.Error(ex.Message);
                    LibraryInstance.Error(ex.StackTrace);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
