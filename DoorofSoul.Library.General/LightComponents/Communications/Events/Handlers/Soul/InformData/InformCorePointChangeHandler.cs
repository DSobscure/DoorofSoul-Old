using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.InformDataParameters.Soul;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Soul.InformData
{
    internal class InformCorePointChangeHandler : InformDataHandler
    {
        internal InformCorePointChangeHandler(ThroneComponents.Soul soul) : base(soul, 1)
        {
        }

        internal override bool Handle(SoulInformDataCode informCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(informCode, parameters))
            {
                try
                {
                    decimal newCorePoint = (decimal)parameters[(byte)InformCorePointChangeParameterCode.NewCorePoint];
                    soul.Attributes.CorePoint = newCorePoint;
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("InformCorePointChange Parameter Cast Error");
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
