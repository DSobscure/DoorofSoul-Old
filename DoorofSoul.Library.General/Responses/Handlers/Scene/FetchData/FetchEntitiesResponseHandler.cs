using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Scene;
using DoorofSoul.Protocol.Language;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers.Scene.FetchData
{
    internal class FetchEntitiesResponseHandler : FetchDataResponseHandler
    {
        internal FetchEntitiesResponseHandler(General.Scene scene) : base(scene)
        {
        }

        internal override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string fetchDebugMessage)
        {
            switch (returnCode)
            {
                case ErrorCode.NoError:
                    {
                        if (parameters.Count != 24)
                        {
                            LibraryLog.ErrorFormat(string.Format("Fetch Entities Response Parameter Error, Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                default:
                    {
                        LibraryLog.ErrorFormat("Fetch Entities Response Error DebugMessage: {0}", fetchDebugMessage);
                        scene.SceneEventManager.ErrorInform(LauguageDictionarySelector.Instance[scene.UsingLanguage]["Unknown Error"], LauguageDictionarySelector.Instance[scene.UsingLanguage]["Fetch Entities Error"]);
                        return false;
                    }
            }
        }

        internal override bool Handle(SceneFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameters)
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
