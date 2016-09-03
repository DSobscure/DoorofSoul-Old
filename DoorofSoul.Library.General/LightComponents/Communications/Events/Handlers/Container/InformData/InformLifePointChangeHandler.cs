using DoorofSoul.Library.General.BasicTypeHelpers;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.InformDataParameters.Container;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Container.InformData
{
    internal class InformLifePointChangeHandler : InformDataHandler
    {
        internal InformLifePointChangeHandler(NatureComponents.Container container) : base(container, 2)
        {
        }

        internal override bool Handle(ContainerInformDataCode informCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(informCode, parameters))
            {
                try
                {
                    decimal newLifePoint = (decimal)(DSDecimal)parameters[(byte)InformLifePointChangeParameterCode.NewLifePoint];
                    container.Attributes.LifePoint = newLifePoint;
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("InformLifePointChange Parameter Cast Error");
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
