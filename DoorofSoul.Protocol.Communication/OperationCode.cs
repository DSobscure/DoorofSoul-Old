﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Protocol.Communication
{
    public enum OperationCode : byte
    {
        FetchData,
        PlayerLogin,
        PlayerLogout
    }
}
