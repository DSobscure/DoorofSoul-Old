using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Scene;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers.Scene.FetchData
{
    internal class FetchEntitiesHandler : FetchDataHandler
    {
        internal FetchEntitiesHandler(General.Scene scene) : base(scene)
        {
        }

        internal override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 0)
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

        internal override bool Handle(SceneFetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            if (base.Handle(fetchCode, parameter))
            {
                try
                {
                    foreach (General.Entity entity in scene.Entities)
                    {
                        var result = new Dictionary<byte, object>
                            {
                                { (byte)FetchEntitiesResponseParameterCode.EntityID, entity.EntityID },
                                { (byte)FetchEntitiesResponseParameterCode.EntityName, entity.EntityName },
                                { (byte)FetchEntitiesResponseParameterCode.PositionX, entity.Position.x },
                                { (byte)FetchEntitiesResponseParameterCode.PositionY, entity.Position.y },
                                { (byte)FetchEntitiesResponseParameterCode.PositionZ, entity.Position.z },
                                { (byte)FetchEntitiesResponseParameterCode.RotationX, entity.Rotation.x },
                                { (byte)FetchEntitiesResponseParameterCode.RotationY, entity.Rotation.y },
                                { (byte)FetchEntitiesResponseParameterCode.RotationZ, entity.Rotation.z },
                                { (byte)FetchEntitiesResponseParameterCode.ScaleX, entity.Scale.x },
                                { (byte)FetchEntitiesResponseParameterCode.ScaleY, entity.Scale.y },
                                { (byte)FetchEntitiesResponseParameterCode.ScaleZ, entity.Scale.z },
                                { (byte)FetchEntitiesResponseParameterCode.VelocityX, entity.Velocity.x },
                                { (byte)FetchEntitiesResponseParameterCode.VelocityY, entity.Velocity.y },
                                { (byte)FetchEntitiesResponseParameterCode.VelocityZ, entity.Velocity.z },
                                { (byte)FetchEntitiesResponseParameterCode.MaxVelocityX, entity.MaxVelocity.x },
                                { (byte)FetchEntitiesResponseParameterCode.MaxVelocityY, entity.MaxVelocity.y },
                                { (byte)FetchEntitiesResponseParameterCode.MaxVelocityZ, entity.MaxVelocity.z },
                                { (byte)FetchEntitiesResponseParameterCode.AngularVelocityX, entity.AngularVelocity.x },
                                { (byte)FetchEntitiesResponseParameterCode.AngularVelocityY, entity.AngularVelocity.y },
                                { (byte)FetchEntitiesResponseParameterCode.AngularVelocityZ, entity.AngularVelocity.z },
                                { (byte)FetchEntitiesResponseParameterCode.MaxAngularVelocityX, entity.MaxAngularVelocity.x },
                                { (byte)FetchEntitiesResponseParameterCode.MaxAngularVelocityY, entity.MaxAngularVelocity.y },
                                { (byte)FetchEntitiesResponseParameterCode.MaxAngularVelocityZ, entity.MaxAngularVelocity.z },
                                { (byte)FetchEntitiesResponseParameterCode.Mass, entity.Mass },
                            };
                        SendResponse(fetchCode, result);
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("FetchEntities Invalid Cast!");
                    LibraryInstance.Error(ex.Message);
                    LibraryInstance.Error(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    LibraryInstance.Error(ex.Message);
                    LibraryInstance.Error(ex.StackTrace);
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
