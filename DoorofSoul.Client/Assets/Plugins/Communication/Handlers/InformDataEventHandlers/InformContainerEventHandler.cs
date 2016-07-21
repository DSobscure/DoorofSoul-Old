using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataParameters;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Client.Communication.Handlers.InformDataEventHandlers
{
    public class InformContainerEventHandler : InformDataEventHandler
    {
        public override bool CheckError(Dictionary<byte, object> parameters, out string debugMessage)
        {
            if (parameters.Count != 26)
            {
                debugMessage = string.Format("Inform Container Event Parameter Error, Parameter Count: {0}", parameters.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }
        public override bool Handle(InformDataCode informCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(informCode, parameters))
            {
                try
                {
                    int containerID = (int)parameters[(byte)InformContainerParameterCode.ContainerID];
                    int entityID = (int)parameters[(byte)InformContainerParameterCode.EntityID];
                    string entityName = (string)parameters[(byte)InformContainerParameterCode.EntityName];
                    int locatedSceneID = (int)parameters[(byte)InformContainerParameterCode.LocatedSceneID];
                    EntitySpaceProperties entitySpaceProperties = new EntitySpaceProperties
                    {
                        position = new DSVector3
                        {
                            x = (float)parameters[(byte)InformContainerParameterCode.PositionX],
                            y = (float)parameters[(byte)InformContainerParameterCode.PositionY],
                            z = (float)parameters[(byte)InformContainerParameterCode.PositionZ]
                        },
                        rotation = new DSVector3
                        {
                            x = (float)parameters[(byte)InformContainerParameterCode.RotationX],
                            y = (float)parameters[(byte)InformContainerParameterCode.RotationY],
                            z = (float)parameters[(byte)InformContainerParameterCode.RotationZ]
                        },
                        scale = new DSVector3
                        {
                            x = (float)parameters[(byte)InformContainerParameterCode.ScaleX],
                            y = (float)parameters[(byte)InformContainerParameterCode.ScaleY],
                            z = (float)parameters[(byte)InformContainerParameterCode.ScaleZ]
                        },
                        velocity = new DSVector3
                        {
                            x = (float)parameters[(byte)InformContainerParameterCode.VelocityX],
                            y = (float)parameters[(byte)InformContainerParameterCode.VelocityY],
                            z = (float)parameters[(byte)InformContainerParameterCode.VelocityZ]
                        },
                        maxVelocity = new DSVector3
                        {
                            x = (float)parameters[(byte)InformContainerParameterCode.MaxVelocityX],
                            y = (float)parameters[(byte)InformContainerParameterCode.MaxVelocityY],
                            z = (float)parameters[(byte)InformContainerParameterCode.MaxVelocityZ]
                        },
                        angularVelocity = new DSVector3
                        {
                            x = (float)parameters[(byte)InformContainerParameterCode.AngularVelocityX],
                            y = (float)parameters[(byte)InformContainerParameterCode.AngularVelocityY],
                            z = (float)parameters[(byte)InformContainerParameterCode.AngularVelocityZ]
                        },
                        maxAngularVelocity = new DSVector3
                        {
                            x = (float)parameters[(byte)InformContainerParameterCode.MaxAngularVelocityX],
                            y = (float)parameters[(byte)InformContainerParameterCode.MaxAngularVelocityY],
                            z = (float)parameters[(byte)InformContainerParameterCode.MaxAngularVelocityZ]
                        },
                        mass = (float)parameters[(byte)InformContainerParameterCode.Mass]
                };

                    Global.Player.Answer.LoadContainers(new List<Container> { new Container(containerID, entityID, entityName, locatedSceneID, entitySpaceProperties) });
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    Global.SystemManagers.DebugInformManager.DebugInform("Inform System Version Event Parameter Cast Error");
                    Global.SystemManagers.DebugInformManager.DebugInform(ex.Message);
                    Global.SystemManagers.DebugInformManager.DebugInform(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    Global.SystemManagers.DebugInformManager.DebugInform(ex.Message);
                    Global.SystemManagers.DebugInformManager.DebugInform(ex.StackTrace);
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
