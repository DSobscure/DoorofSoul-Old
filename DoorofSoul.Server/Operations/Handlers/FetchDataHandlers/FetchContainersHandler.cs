using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataParameters;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Server.Operations.Handlers.FetchDataHandlers
{
    public class FetchContainersHandler : FetchDataHandler
    {
        public FetchContainersHandler(Peer peer) : base(peer)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 0)
            {
                debugMessage = string.Format("fetch containers has {0} parameters!", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(FetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            if (base.Handle(fetchCode, parameter))
            {
                try
                {
                    foreach (Container container in peer.Player.Answer.Containers)
                    {
                        var result = new Dictionary<byte, object>
                        {
                            { (byte)InformContainerParameterCode.ContainerID, container.ContainerID },
                            { (byte)InformContainerParameterCode.EntityID, container.EntityID },
                            { (byte)InformContainerParameterCode.EntityName, container.EntityName },
                            { (byte)InformContainerParameterCode.LocatedSceneID, container.LocatedSceneID },
                            { (byte)InformContainerParameterCode.PositionX, container.Position.x },
                            { (byte)InformContainerParameterCode.PositionY, container.Position.y },
                            { (byte)InformContainerParameterCode.PositionZ, container.Position.z },
                            { (byte)InformContainerParameterCode.RotationX, container.Rotation.x },
                            { (byte)InformContainerParameterCode.RotationY, container.Rotation.y },
                            { (byte)InformContainerParameterCode.RotationZ, container.Rotation.z },
                            { (byte)InformContainerParameterCode.ScaleX, container.Scale.x },
                            { (byte)InformContainerParameterCode.ScaleY, container.Scale.y },
                            { (byte)InformContainerParameterCode.ScaleZ, container.Scale.z },
                            { (byte)InformContainerParameterCode.VelocityX, container.Velocity.x },
                            { (byte)InformContainerParameterCode.VelocityY, container.Velocity.y },
                            { (byte)InformContainerParameterCode.VelocityZ, container.Velocity.z },
                            { (byte)InformContainerParameterCode.MaxVelocityX, container.MaxVelocity.x },
                            { (byte)InformContainerParameterCode.MaxVelocityY, container.MaxVelocity.y },
                            { (byte)InformContainerParameterCode.MaxVelocityZ, container.MaxVelocity.z },
                            { (byte)InformContainerParameterCode.AngularVelocityX, container.AngularVelocity.x },
                            { (byte)InformContainerParameterCode.AngularVelocityY, container.AngularVelocity.y },
                            { (byte)InformContainerParameterCode.AngularVelocityZ, container.AngularVelocity.z },
                            { (byte)InformContainerParameterCode.MaxAngularVelocityX, container.MaxAngularVelocity.x },
                            { (byte)InformContainerParameterCode.MaxAngularVelocityY, container.MaxAngularVelocity.y },
                            { (byte)InformContainerParameterCode.MaxAngularVelocityZ, container.MaxAngularVelocity.z },
                            { (byte)InformContainerParameterCode.Mass, container.Mass },
                        };
                        SendEvent((byte)InformDataCode.Container, result);
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    Application.Log.ErrorFormat("Fetch Souls Invalid Cast!");
                    Application.Log.Error(ex.Message);
                    Application.Log.Error(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    Application.Log.Error(ex.Message);
                    Application.Log.Error(ex.StackTrace);
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
