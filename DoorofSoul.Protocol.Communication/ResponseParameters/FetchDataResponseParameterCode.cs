using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Protocol.Communication.ResponseParameters
{
    public enum FetchDataResponseParameterCode : byte
    {
        FetchCode,
        ReturnCode,
        DebugMessage,
        Parameters
    }
}
