using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Protocol.Communication.InformDataParameters
{
    public enum InformSceneEntityEnterParameterCode : byte
    {
        EntityID,
        EntityName,
        LocatedSceneID,
        PositionX,
        PositionY,
        PositionZ,
        RotationX,
        RotationY,
        RotationZ,
        ScaleX,
        ScaleY,
        ScaleZ,
        VelocityX,
        VelocityY,
        VelocityZ,
        MaxVelocityX,
        MaxVelocityY,
        MaxVelocityZ,
        AngularVelocityX,
        AngularVelocityY,
        AngularVelocityZ,
        MaxAngularVelocityX,
        MaxAngularVelocityY,
        MaxAngularVelocityZ,
        Mass
    }
}
