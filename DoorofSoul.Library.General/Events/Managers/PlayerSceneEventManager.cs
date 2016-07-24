using System;
using System.Collections.Generic;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventParameters;
using DoorofSoul.Protocol.Communication.InformDataParameters;

namespace DoorofSoul.Library.General.Events.Managers
{
    public class PlayerSceneEventManager
    {
        protected Player player;

        public PlayerSceneEventManager(Player player)
        {
            this.player = player;
        }

        public void OnSceneEntityEnter(Entity entity)
        {
            Dictionary<byte, object> result = new Dictionary<byte, object>
            {
                { (byte)InformEntityEnterParameterCode.EntityID, entity.EntityID },
                { (byte)InformEntityEnterParameterCode.EntityName, entity.EntityName },
                { (byte)InformEntityEnterParameterCode.LocatedSceneID, entity.LocatedSceneID },
                { (byte)InformEntityEnterParameterCode.PositionX, entity.Position.x },
                { (byte)InformEntityEnterParameterCode.PositionY, entity.Position.y },
                { (byte)InformEntityEnterParameterCode.PositionZ, entity.Position.z },
                { (byte)InformEntityEnterParameterCode.RotationX, entity.Rotation.x },
                { (byte)InformEntityEnterParameterCode.RotationY, entity.Rotation.y },
                { (byte)InformEntityEnterParameterCode.RotationZ, entity.Rotation.z },
                { (byte)InformEntityEnterParameterCode.ScaleX, entity.Scale.x },
                { (byte)InformEntityEnterParameterCode.ScaleY, entity.Scale.y },
                { (byte)InformEntityEnterParameterCode.ScaleZ, entity.Scale.z },
                { (byte)InformEntityEnterParameterCode.VelocityX, entity.Velocity.x },
                { (byte)InformEntityEnterParameterCode.VelocityY, entity.Velocity.y },
                { (byte)InformEntityEnterParameterCode.VelocityZ, entity.Velocity.z },
                { (byte)InformEntityEnterParameterCode.MaxVelocityX, entity.MaxVelocity.x },
                { (byte)InformEntityEnterParameterCode.MaxVelocityY, entity.MaxVelocity.y },
                { (byte)InformEntityEnterParameterCode.MaxVelocityZ, entity.MaxVelocity.z },
                { (byte)InformEntityEnterParameterCode.AngularVelocityX, entity.AngularVelocity.x },
                { (byte)InformEntityEnterParameterCode.AngularVelocityY, entity.AngularVelocity.y },
                { (byte)InformEntityEnterParameterCode.AngularVelocityZ, entity.AngularVelocity.z },
                { (byte)InformEntityEnterParameterCode.MaxAngularVelocityX, entity.MaxAngularVelocity.x },
                { (byte)InformEntityEnterParameterCode.MaxAngularVelocityY, entity.MaxAngularVelocity.y },
                { (byte)InformEntityEnterParameterCode.MaxAngularVelocityZ, entity.MaxAngularVelocity.z },
                { (byte)InformEntityEnterParameterCode.Mass, entity.Mass },
            };
            Dictionary<byte, object> parameter = new Dictionary<byte, object>
            {
                { (byte)InformDataEventParameterCode.InformCode, (byte)PlayerInformDataCode.SceneEntityEnter },
                { (byte)InformDataEventParameterCode.ReturnCode, ErrorCode.NoError },
                { (byte)InformDataEventParameterCode.Parameters, result }
            };
            player.SendEvent(EventCode.InformData, parameter);
        }
        public void OnSceneEntityExit(Entity entity)
        {
            Dictionary<byte, object> result = new Dictionary<byte, object>
            {
                { (byte)InformEntityExitParameterCode.EntityID, entity.EntityID },
                { (byte)InformEntityExitParameterCode.LocatedSceneID, entity.LocatedSceneID }
            };
            Dictionary<byte, object> parameter = new Dictionary<byte, object>
            {
                { (byte)InformDataEventParameterCode.InformCode, (byte)PlayerInformDataCode.SceneEntityExit },
                { (byte)InformDataEventParameterCode.ReturnCode, ErrorCode.NoError },
                { (byte)InformDataEventParameterCode.Parameters, result }
            };
            player.SendEvent(EventCode.InformData, parameter);
        }
    }
}
