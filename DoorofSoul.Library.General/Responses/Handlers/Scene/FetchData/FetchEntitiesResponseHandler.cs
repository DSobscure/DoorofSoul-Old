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
                        if (parameters.Count != 3)
                        {
                            LibraryInstance.ErrorFormat(string.Format("Fetch Entities Response Parameter Error, Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                default:
                    {
                        LibraryInstance.ErrorFormat("Fetch Entities Response Error DebugMessage: {0}", fetchDebugMessage);
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
                    EntitySpaceProperties entitySpaceProperties = (EntitySpaceProperties)parameters[(byte)FetchEntitiesResponseParameterCode.EntitySpaceProperties];
                    General.Entity entity = new General.Entity(entityID, entityName, scene.SceneID, entitySpaceProperties);
                    scene.EntityEnter(entity);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("Fetch Entities Response Parameter Cast Error");
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
