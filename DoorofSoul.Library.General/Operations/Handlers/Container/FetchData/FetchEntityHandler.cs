using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Container;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers.Container.FetchData
{
    public class FetchEntityHandler : FetchDataHandler
    {
        public FetchEntityHandler(General.Container container) : base(container)
        {
            
        }

        public override bool CheckParameter(Dictionary<byte, object> parameters, out string debugMessage)
        {
            if (parameters.Count != 0)
            {
                debugMessage = string.Format("Container Fetch Entity Parameter Error Parameter Count: {0}", parameters.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(ContainerFetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            if (base.Handle(fetchCode, parameter))
            {
                try
                {
                    General.Entity entity = container.Entity;
                    var result = new Dictionary<byte, object>
                    {
                        { (byte)FetchEntityResponseParameterCode.EntityID, entity.EntityID },
                        { (byte)FetchEntityResponseParameterCode.EntityName, entity.EntityName },
                        { (byte)FetchEntityResponseParameterCode.LocatedSceneID, entity.LocatedSceneID },
                        { (byte)FetchEntityResponseParameterCode.PositionX, entity.Position.x },
                        { (byte)FetchEntityResponseParameterCode.PositionY, entity.Position.y },
                        { (byte)FetchEntityResponseParameterCode.PositionZ, entity.Position.z },
                        { (byte)FetchEntityResponseParameterCode.RotationX, entity.Rotation.x },
                        { (byte)FetchEntityResponseParameterCode.RotationY, entity.Rotation.y },
                        { (byte)FetchEntityResponseParameterCode.RotationZ, entity.Rotation.z },
                        { (byte)FetchEntityResponseParameterCode.ScaleX, entity.Scale.x },
                        { (byte)FetchEntityResponseParameterCode.ScaleY, entity.Scale.y },
                        { (byte)FetchEntityResponseParameterCode.ScaleZ, entity.Scale.z },
                        { (byte)FetchEntityResponseParameterCode.VelocityX, entity.Velocity.x },
                        { (byte)FetchEntityResponseParameterCode.VelocityY, entity.Velocity.y },
                        { (byte)FetchEntityResponseParameterCode.VelocityZ, entity.Velocity.z },
                        { (byte)FetchEntityResponseParameterCode.MaxVelocityX, entity.MaxVelocity.x },
                        { (byte)FetchEntityResponseParameterCode.MaxVelocityY, entity.MaxVelocity.y },
                        { (byte)FetchEntityResponseParameterCode.MaxVelocityZ, entity.MaxVelocity.z },
                        { (byte)FetchEntityResponseParameterCode.AngularVelocityX, entity.AngularVelocity.x },
                        { (byte)FetchEntityResponseParameterCode.AngularVelocityY, entity.AngularVelocity.y },
                        { (byte)FetchEntityResponseParameterCode.AngularVelocityZ, entity.AngularVelocity.z },
                        { (byte)FetchEntityResponseParameterCode.MaxAngularVelocityX, entity.MaxAngularVelocity.x },
                        { (byte)FetchEntityResponseParameterCode.MaxAngularVelocityY, entity.MaxAngularVelocity.y },
                        { (byte)FetchEntityResponseParameterCode.MaxAngularVelocityZ, entity.MaxAngularVelocity.z },
                        { (byte)FetchEntityResponseParameterCode.Mass, entity.Mass },
                    };        
                    SendResponse(fetchCode, result);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.ErrorFormat("FetchEntity Invalid Cast!");
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
