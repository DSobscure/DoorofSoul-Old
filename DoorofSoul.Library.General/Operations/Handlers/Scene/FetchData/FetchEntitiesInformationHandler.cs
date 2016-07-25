using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.InformDataParameters.Scene;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers.Scene.FetchData
{
    public class FetchEntitiesHandler : FetchDataHandler
    {
        public FetchEntitiesHandler(General.Scene scene) : base(scene)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 1)
            {
                debugMessage = string.Format("Scene Fetch Entities Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(SceneFetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            if (base.Handle(fetchCode, parameter))
            {
                try
                {
                    foreach (General.Entity entity in scene.Entities)
                    {
                        var result = new Dictionary<byte, object>
                            {
                                { (byte)InformEntityParameterCode.EntityID, entity.EntityID },
                                { (byte)InformEntityParameterCode.EntityName, entity.EntityName },
                                { (byte)InformEntityParameterCode.PositionX, entity.Position.x },
                                { (byte)InformEntityParameterCode.PositionY, entity.Position.y },
                                { (byte)InformEntityParameterCode.PositionZ, entity.Position.z },
                                { (byte)InformEntityParameterCode.RotationX, entity.Rotation.x },
                                { (byte)InformEntityParameterCode.RotationY, entity.Rotation.y },
                                { (byte)InformEntityParameterCode.RotationZ, entity.Rotation.z },
                                { (byte)InformEntityParameterCode.ScaleX, entity.Scale.x },
                                { (byte)InformEntityParameterCode.ScaleY, entity.Scale.y },
                                { (byte)InformEntityParameterCode.ScaleZ, entity.Scale.z },
                                { (byte)InformEntityParameterCode.VelocityX, entity.Velocity.x },
                                { (byte)InformEntityParameterCode.VelocityY, entity.Velocity.y },
                                { (byte)InformEntityParameterCode.VelocityZ, entity.Velocity.z },
                                { (byte)InformEntityParameterCode.MaxVelocityX, entity.MaxVelocity.x },
                                { (byte)InformEntityParameterCode.MaxVelocityY, entity.MaxVelocity.y },
                                { (byte)InformEntityParameterCode.MaxVelocityZ, entity.MaxVelocity.z },
                                { (byte)InformEntityParameterCode.AngularVelocityX, entity.AngularVelocity.x },
                                { (byte)InformEntityParameterCode.AngularVelocityY, entity.AngularVelocity.y },
                                { (byte)InformEntityParameterCode.AngularVelocityZ, entity.AngularVelocity.z },
                                { (byte)InformEntityParameterCode.MaxAngularVelocityX, entity.MaxAngularVelocity.x },
                                { (byte)InformEntityParameterCode.MaxAngularVelocityY, entity.MaxAngularVelocity.y },
                                { (byte)InformEntityParameterCode.MaxAngularVelocityZ, entity.MaxAngularVelocity.z },
                                { (byte)InformEntityParameterCode.Mass, entity.Mass },
                            };
                        SendEvent(SceneInformDataCode.Entity, result);
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.ErrorFormat("FetchEntities Invalid Cast!");
                    LibraryLog.Error(ex.Message);
                    LibraryLog.Error(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    LibraryLog.Error(ex.Message);
                    LibraryLog.Error(ex.StackTrace);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
