using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Protocol.Communication.InformDataParameters.Scene
{
    public enum InformEntityEnterParameterCode : byte
    {
        EntityID,
        EntityName,
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
