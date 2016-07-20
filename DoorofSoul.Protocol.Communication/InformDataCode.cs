using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Protocol.Communication
{
    public enum InformDataCode : byte
    {
        FetchDataError,
        SystemVersion,
        Soul,
        Container,
        SoulContainerConnection,
        SceneEntityInformation,
        EntityTransform,
        EntityVelocity
    }
}
