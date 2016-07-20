using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Protocol.Communication.OperationParameters
{
    public enum ReportObserveEntityResultOperationParameterCode : byte
    {
        EntityID,
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
        AngularVelocityX,
        AngularVelocityY,
        AngularVelocityZ,
        Mass
    }
}
