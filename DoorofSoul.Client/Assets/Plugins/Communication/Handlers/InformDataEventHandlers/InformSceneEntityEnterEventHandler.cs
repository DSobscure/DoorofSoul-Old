using DoorofSoul.Client.Library.General;
using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataParameters;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Client.Communication.Handlers.InformDataEventHandlers
{
    public class InformSceneEntityEnterEventHandler : InformDataEventHandler
    {
        public override bool CheckError(Dictionary<byte, object> parameters, out string debugMessage)
        {
            if (parameters.Count != 25)
            {
                debugMessage = string.Format("Inform SceneEntityEnter Event Parameter Error, Parameter Count: {0}", parameters.Count);
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
                    int entityID = (int)parameters[(byte)InformSceneEntityEnterParameterCode.EntityID];
                    string entityName = (string)parameters[(byte)InformSceneEntityEnterParameterCode.EntityName];
                    int locatedSceneID = (int)parameters[(byte)InformSceneEntityEnterParameterCode.LocatedSceneID];
                    EntitySpaceProperties entitySpaceProperties = new EntitySpaceProperties
                    {
                        position = new DSVector3
                        {
                            x = (float)parameters[(byte)InformSceneEntityEnterParameterCode.PositionX],
                            y = (float)parameters[(byte)InformSceneEntityEnterParameterCode.PositionY],
                            z = (float)parameters[(byte)InformSceneEntityEnterParameterCode.PositionZ]
                        },
                        rotation = new DSVector3
                        {
                            x = (float)parameters[(byte)InformSceneEntityEnterParameterCode.RotationX],
                            y = (float)parameters[(byte)InformSceneEntityEnterParameterCode.RotationY],
                            z = (float)parameters[(byte)InformSceneEntityEnterParameterCode.RotationZ]
                        },
                        scale = new DSVector3
                        {
                            x = (float)parameters[(byte)InformSceneEntityEnterParameterCode.ScaleX],
                            y = (float)parameters[(byte)InformSceneEntityEnterParameterCode.ScaleY],
                            z = (float)parameters[(byte)InformSceneEntityEnterParameterCode.ScaleZ]
                        },
                        velocity = new DSVector3
                        {
                            x = (float)parameters[(byte)InformSceneEntityEnterParameterCode.VelocityX],
                            y = (float)parameters[(byte)InformSceneEntityEnterParameterCode.VelocityY],
                            z = (float)parameters[(byte)InformSceneEntityEnterParameterCode.VelocityZ]
                        },
                        maxVelocity = new DSVector3
                        {
                            x = (float)parameters[(byte)InformSceneEntityEnterParameterCode.MaxVelocityX],
                            y = (float)parameters[(byte)InformSceneEntityEnterParameterCode.MaxVelocityY],
                            z = (float)parameters[(byte)InformSceneEntityEnterParameterCode.MaxVelocityZ]
                        },
                        angularVelocity = new DSVector3
                        {
                            x = (float)parameters[(byte)InformSceneEntityEnterParameterCode.AngularVelocityX],
                            y = (float)parameters[(byte)InformSceneEntityEnterParameterCode.AngularVelocityY],
                            z = (float)parameters[(byte)InformSceneEntityEnterParameterCode.AngularVelocityZ]
                        },
                        maxAngularVelocity = new DSVector3
                        {
                            x = (float)parameters[(byte)InformSceneEntityEnterParameterCode.MaxAngularVelocityX],
                            y = (float)parameters[(byte)InformSceneEntityEnterParameterCode.MaxAngularVelocityY],
                            z = (float)parameters[(byte)InformSceneEntityEnterParameterCode.MaxAngularVelocityZ]
                        },
                        mass = (float)parameters[(byte)InformSceneEntityEnterParameterCode.Mass]
                    };
                    Entity entity = new ClientEntity(entityID, entityName, locatedSceneID, entitySpaceProperties);
                    Global.ScenesManager.EntityEnter(entity);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    Global.SystemManagers.DebugInformManager.DebugInform("Inform SceneEntityEnter Event Parameter Cast Error");
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
