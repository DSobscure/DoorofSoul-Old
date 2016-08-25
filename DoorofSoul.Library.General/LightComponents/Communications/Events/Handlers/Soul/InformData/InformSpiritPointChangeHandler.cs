﻿using System;
using System.Collections.Generic;
using DoorofSoul.Protocol.Communication.InformDataParameters.Soul;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Library.General.BasicTypeHelpers;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Soul.InformData
{
    internal class InformSpiritPointChangeHandler : InformDataHandler
    {
        internal InformSpiritPointChangeHandler(ThroneComponents.Soul soul) : base(soul, 1)
        {
        }

        internal override bool Handle(SoulInformDataCode informCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(informCode, parameters))
            {
                try
                {
                    decimal newSpiritPoint = (decimal)(DSDecimal)parameters[(byte)InformSpiritPointChangeParameterCode.NewSpiritPoint];
                    soul.Attributes.SpiritPoint = newSpiritPoint;
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("InformSpiritPointChange Parameter Cast Error");
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