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
                { (byte)InformSceneEntityEnterParameterCode.EntityID, entity.EntityID },
                { (byte)InformSceneEntityEnterParameterCode.EntityName, entity.EntityName },
                { (byte)InformSceneEntityEnterParameterCode.LocatedSceneID, entity.LocatedSceneID },
                { (byte)InformSceneEntityEnterParameterCode.PositionX, entity.Position.x },
                { (byte)InformSceneEntityEnterParameterCode.PositionY, entity.Position.y },
                { (byte)InformSceneEntityEnterParameterCode.PositionZ, entity.Position.z },
                { (byte)InformSceneEntityEnterParameterCode.RotationX, entity.Rotation.x },
                { (byte)InformSceneEntityEnterParameterCode.RotationY, entity.Rotation.y },
                { (byte)InformSceneEntityEnterParameterCode.RotationZ, entity.Rotation.z },
                { (byte)InformSceneEntityEnterParameterCode.ScaleX, entity.Scale.x },
                { (byte)InformSceneEntityEnterParameterCode.ScaleY, entity.Scale.y },
                { (byte)InformSceneEntityEnterParameterCode.ScaleZ, entity.Scale.z },
                { (byte)InformSceneEntityEnterParameterCode.VelocityX, entity.Velocity.x },
                { (byte)InformSceneEntityEnterParameterCode.VelocityY, entity.Velocity.y },
                { (byte)InformSceneEntityEnterParameterCode.VelocityZ, entity.Velocity.z },
                { (byte)InformSceneEntityEnterParameterCode.MaxVelocityX, entity.MaxVelocity.x },
                { (byte)InformSceneEntityEnterParameterCode.MaxVelocityY, entity.MaxVelocity.y },
                { (byte)InformSceneEntityEnterParameterCode.MaxVelocityZ, entity.MaxVelocity.z },
                { (byte)InformSceneEntityEnterParameterCode.AngularVelocityX, entity.AngularVelocity.x },
                { (byte)InformSceneEntityEnterParameterCode.AngularVelocityY, entity.AngularVelocity.y },
                { (byte)InformSceneEntityEnterParameterCode.AngularVelocityZ, entity.AngularVelocity.z },
                { (byte)InformSceneEntityEnterParameterCode.MaxAngularVelocityX, entity.MaxAngularVelocity.x },
                { (byte)InformSceneEntityEnterParameterCode.MaxAngularVelocityY, entity.MaxAngularVelocity.y },
                { (byte)InformSceneEntityEnterParameterCode.MaxAngularVelocityZ, entity.MaxAngularVelocity.z },
                { (byte)InformSceneEntityEnterParameterCode.Mass, entity.Mass },
            };
            Dictionary<byte, object> parameter = new Dictionary<byte, object>
            {
                { (byte)InformDataEventParameterCode.InformCode, (byte)InformDataCode.SceneEntityEnter },
                { (byte)InformDataEventParameterCode.ReturnCode, ErrorCode.NoError },
                { (byte)InformDataEventParameterCode.Parameters, result }
            };
            player.SendEvent(EventCode.InformData, parameter);
        }
        public void OnSceneEntityExit(Entity entity)
        {
            Dictionary<byte, object> result = new Dictionary<byte, object>
            {
                { (byte)InformSceneEntityExitParameterCode.EntityID, entity.EntityID },
                { (byte)InformSceneEntityExitParameterCode.LocatedSceneID, entity.LocatedSceneID }
            };
            Dictionary<byte, object> parameter = new Dictionary<byte, object>
            {
                { (byte)InformDataEventParameterCode.InformCode, (byte)InformDataCode.SceneEntityExit },
                { (byte)InformDataEventParameterCode.ReturnCode, ErrorCode.NoError },
                { (byte)InformDataEventParameterCode.Parameters, result }
            };
            player.SendEvent(EventCode.InformData, parameter);
        }
    }
}
