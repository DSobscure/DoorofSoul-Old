using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Protocol.Communication.FetchDataResponseParameters
{
    public enum FetchDataResponseParameterCode : byte
    {
        FetchCode,
        ReturnCode,
        DebugMessage,
        Parameters
    }
}
