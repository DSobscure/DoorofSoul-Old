using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.InformDataParameters.Scene;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Scene.InformData
{
    public class InformEntityEnterHandler : InformDataHandler
    {
        public InformEntityEnterHandler(General.Scene scene) : base(scene)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameters, out string debugMessage)
        {
            if (parameters.Count != 24)
            {
                debugMessage = string.Format("Inform EntityEnter Event Parameter Error, Parameter Count: {0}", parameters.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(SceneInformDataCode informCode, ErrorCode returnCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(informCode, returnCode, parameters))
            {
                try
                {
                    int entityID = (int)parameters[(byte)InformEntityEnterParameterCode.EntityID];
                    string entityName = (string)parameters[(byte)InformEntityEnterParameterCode.EntityName];
                    EntitySpaceProperties entitySpaceProperties = new EntitySpaceProperties
                    {
                        position = new DSVector3
                        {
                            x = (float)parameters[(byte)InformEntityEnterParameterCode.PositionX],
                            y = (float)parameters[(byte)InformEntityEnterParameterCode.PositionY],
                            z = (float)parameters[(byte)InformEntityEnterParameterCode.PositionZ]
                        },
                        rotation = new DSVector3
                        {
                            x = (float)parameters[(byte)InformEntityEnterParameterCode.RotationX],
                            y = (float)parameters[(byte)InformEntityEnterParameterCode.RotationY],
                            z = (float)parameters[(byte)InformEntityEnterParameterCode.RotationZ]
                        },
                        scale = new DSVector3
                        {
                            x = (float)parameters[(byte)InformEntityEnterParameterCode.ScaleX],
                            y = (float)parameters[(byte)InformEntityEnterParameterCode.ScaleY],
                            z = (float)parameters[(byte)InformEntityEnterParameterCode.ScaleZ]
                        },
                        velocity = new DSVector3
                        {
                            x = (float)parameters[(byte)InformEntityEnterParameterCode.VelocityX],
                            y = (float)parameters[(byte)InformEntityEnterParameterCode.VelocityY],
                            z = (float)parameters[(byte)InformEntityEnterParameterCode.VelocityZ]
                        },
                        maxVelocity = new DSVector3
                        {
                            x = (float)parameters[(byte)InformEntityEnterParameterCode.MaxVelocityX],
                            y = (float)parameters[(byte)InformEntityEnterParameterCode.MaxVelocityY],
                            z = (float)parameters[(byte)InformEntityEnterParameterCode.MaxVelocityZ]
                        },
                        angularVelocity = new DSVector3
                        {
                            x = (float)parameters[(byte)InformEntityEnterParameterCode.AngularVelocityX],
                            y = (float)parameters[(byte)InformEntityEnterParameterCode.AngularVelocityY],
                            z = (float)parameters[(byte)InformEntityEnterParameterCode.AngularVelocityZ]
                        },
                        maxAngularVelocity = new DSVector3
                        {
                            x = (float)parameters[(byte)InformEntityEnterParameterCode.MaxAngularVelocityX],
                            y = (float)parameters[(byte)InformEntityEnterParameterCode.MaxAngularVelocityY],
                            z = (float)parameters[(byte)InformEntityEnterParameterCode.MaxAngularVelocityZ]
                        },
                        mass = (float)parameters[(byte)InformEntityEnterParameterCode.Mass]
                    };
                    General.Entity entity = new General.Entity(entityID, entityName, scene.SceneID, entitySpaceProperties);
                    scene.EntityEnter(entity);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.Error("Inform SceneEntityEnter Event Parameter Cast Error");
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
