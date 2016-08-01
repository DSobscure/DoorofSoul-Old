using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Container;
using DoorofSoul.Protocol.Language;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers.Container.FetchData
{
    internal class FetchEntityResponseHandler : FetchDataResponseHandler
    {
        internal FetchEntityResponseHandler(General.Container container) : base(container)
        {
        }

        internal override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            switch (returnCode)
            {
                case ErrorCode.NoError:
                    {
                        if (parameters.Count != 25)
                        {
                            LibraryLog.ErrorFormat(string.Format("Fetch Entity Response Parameter Error, Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                default:
                    {
                        LibraryLog.ErrorFormat("Fetch Entity Response Error DebugMessage: {0}", debugMessage);
                        container.ContainerEventManager.ErrorInform(LauguageDictionarySelector.Instance[container.UsingLanguage]["Unknown Error"], LauguageDictionarySelector.Instance[container.UsingLanguage]["Fetch Entity Error"], Protocol.Communication.Channels.ContainerCommunicationChannel.Answer);
                        return false;
                    }
            }
        }

        internal override bool Handle(ContainerFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(fetchCode, returnCode, fetchDebugMessage, parameters))
            {
                try
                {
                    int entityID = (int)parameters[(byte)FetchEntityResponseParameterCode.EntityID];
                    string entityName = (string)parameters[(byte)FetchEntityResponseParameterCode.EntityName];
                    int locatedSceneID = (int)parameters[(byte)FetchEntityResponseParameterCode.LocatedSceneID];
                    EntitySpaceProperties entitySpaceProperties = new EntitySpaceProperties
                    {
                        position = new DSVector3
                        {
                            x = (float)parameters[(byte)FetchEntityResponseParameterCode.PositionX],
                            y = (float)parameters[(byte)FetchEntityResponseParameterCode.PositionY],
                            z = (float)parameters[(byte)FetchEntityResponseParameterCode.PositionZ]
                        },
                        rotation = new DSVector3
                        {
                            x = (float)parameters[(byte)FetchEntityResponseParameterCode.RotationX],
                            y = (float)parameters[(byte)FetchEntityResponseParameterCode.RotationY],
                            z = (float)parameters[(byte)FetchEntityResponseParameterCode.RotationZ]
                        },
                        scale = new DSVector3
                        {
                            x = (float)parameters[(byte)FetchEntityResponseParameterCode.ScaleX],
                            y = (float)parameters[(byte)FetchEntityResponseParameterCode.ScaleY],
                            z = (float)parameters[(byte)FetchEntityResponseParameterCode.ScaleZ]
                        },
                        velocity = new DSVector3
                        {
                            x = (float)parameters[(byte)FetchEntityResponseParameterCode.VelocityX],
                            y = (float)parameters[(byte)FetchEntityResponseParameterCode.VelocityY],
                            z = (float)parameters[(byte)FetchEntityResponseParameterCode.VelocityZ]
                        },
                        maxVelocity = new DSVector3
                        {
                            x = (float)parameters[(byte)FetchEntityResponseParameterCode.MaxVelocityX],
                            y = (float)parameters[(byte)FetchEntityResponseParameterCode.MaxVelocityY],
                            z = (float)parameters[(byte)FetchEntityResponseParameterCode.MaxVelocityZ]
                        },
                        angularVelocity = new DSVector3
                        {
                            x = (float)parameters[(byte)FetchEntityResponseParameterCode.AngularVelocityX],
                            y = (float)parameters[(byte)FetchEntityResponseParameterCode.AngularVelocityY],
                            z = (float)parameters[(byte)FetchEntityResponseParameterCode.AngularVelocityZ]
                        },
                        maxAngularVelocity = new DSVector3
                        {
                            x = (float)parameters[(byte)FetchEntityResponseParameterCode.MaxAngularVelocityX],
                            y = (float)parameters[(byte)FetchEntityResponseParameterCode.MaxAngularVelocityY],
                            z = (float)parameters[(byte)FetchEntityResponseParameterCode.MaxAngularVelocityZ]
                        },
                        mass = (float)parameters[(byte)FetchEntityResponseParameterCode.Mass]
                    };
                    General.Entity entity = new General.Entity(entityID, entityName, locatedSceneID, entitySpaceProperties);
                    container.BindEntity(entity);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.Error("Fetch Entity Response Parameter Cast Error");
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
