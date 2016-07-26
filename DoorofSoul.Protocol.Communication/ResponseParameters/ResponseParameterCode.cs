using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Protocol.Communication.ResponseParameters
{
    public enum ResponseParameterCode : byte
    {
        OperationCode,
        ID,
        ReturnCode,
        DebugMessage,
        Parameters
    }
}
