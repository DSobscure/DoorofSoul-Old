using System;
using System.Collections.Generic;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Scene;

namespace DoorofSoul.Library.General.Responses.Handlers.Scene.FetchData
{
    public class FetchEntitiesResponseHandler : FetchDataResponseHandler
    {
        public FetchEntitiesResponseHandler(General.Scene scene) : base(scene)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 24)
            {
                debugMessage = string.Format("Fetch Entities Response Parameter Error, Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(SceneFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(fetchCode, returnCode, fetchDebugMessage, parameters))
            {
                try
                {
                    int entityID = (int)parameters[(byte)FetchEntitiesResponseParameterCode.EntityID];
                    string entityName = (string)parameters[(byte)FetchEntitiesResponseParameterCode.EntityName];
                    EntitySpaceProperties entitySpaceProperties = new EntitySpaceProperties
                    {
                        position = new DSVector3
                        {
                            x = (float)parameters[(byte)FetchEntitiesResponseParameterCode.PositionX],
                            y = (float)parameters[(byte)FetchEntitiesResponseParameterCode.PositionY],
                            z = (float)parameters[(byte)FetchEntitiesResponseParameterCode.PositionZ]
                        },
                        rotation = new DSVector3
                        {
                            x = (float)parameters[(byte)FetchEntitiesResponseParameterCode.RotationX],
                            y = (float)parameters[(byte)FetchEntitiesResponseParameterCode.RotationY],
                            z = (float)parameters[(byte)FetchEntitiesResponseParameterCode.RotationZ]
                        },
                        scale = new DSVector3
                        {
                            x = (float)parameters[(byte)FetchEntitiesResponseParameterCode.ScaleX],
                            y = (float)parameters[(byte)FetchEntitiesResponseParameterCode.ScaleY],
                            z = (float)parameters[(byte)FetchEntitiesResponseParameterCode.ScaleZ]
                        },
                        velocity = new DSVector3
                        {
                            x = (float)parameters[(byte)FetchEntitiesResponseParameterCode.VelocityX],
                            y = (float)parameters[(byte)FetchEntitiesResponseParameterCode.VelocityY],
                            z = (float)parameters[(byte)FetchEntitiesResponseParameterCode.VelocityZ]
                        },
                        maxVelocity = new DSVector3
                        {
                            x = (float)parameters[(byte)FetchEntitiesResponseParameterCode.MaxVelocityX],
                            y = (float)parameters[(byte)FetchEntitiesResponseParameterCode.MaxVelocityY],
                            z = (float)parameters[(byte)FetchEntitiesResponseParameterCode.MaxVelocityZ]
                        },
                        angularVelocity = new DSVector3
                        {
                            x = (float)parameters[(byte)FetchEntitiesResponseParameterCode.AngularVelocityX],
                            y = (float)parameters[(byte)FetchEntitiesResponseParameterCode.AngularVelocityY],
                            z = (float)parameters[(byte)FetchEntitiesResponseParameterCode.AngularVelocityZ]
                        },
                        maxAngularVelocity = new DSVector3
                        {
                            x = (float)parameters[(byte)FetchEntitiesResponseParameterCode.MaxAngularVelocityX],
                            y = (float)parameters[(byte)FetchEntitiesResponseParameterCode.MaxAngularVelocityY],
                            z = (float)parameters[(byte)FetchEntitiesResponseParameterCode.MaxAngularVelocityZ]
                        },
                        mass = (float)parameters[(byte)FetchEntitiesResponseParameterCode.Mass]
                    };
                    General.Entity entity = new General.Entity(entityID, entityName, scene.SceneID, entitySpaceProperties);
                    scene.EntityEnter(entity);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.Error("Fetch Entities Response Parameter Cast Error");
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
