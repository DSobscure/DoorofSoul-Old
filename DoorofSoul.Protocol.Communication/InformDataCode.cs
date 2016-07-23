﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Protocol.Communication
{
    public enum InformDataCode : byte
    {
        FetchDataError,
        SystemVersion,
        Answer,
        Soul,
        Container,
        SoulContainerConnection,
        Scene,
        SceneEntityEnter,
        SceneEntityExit,
        EntityTransform,
        EntityVelocity
    }
}
