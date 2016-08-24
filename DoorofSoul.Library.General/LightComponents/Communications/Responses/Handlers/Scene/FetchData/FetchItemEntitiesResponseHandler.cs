using System;
using System.Collections.Generic;
using System.Linq;
using DoorofSoul.Protocol.Language;
using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Library.General.NatureComponents.SceneElements;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Scene;

namespace DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers.Scene.FetchData
{
    internal class FetchItemEntitiesResponseHandler : FetchDataResponseHandler
    {
        internal FetchItemEntitiesResponseHandler(NatureComponents.Scene scene) : base(scene)
        {
        }

        internal override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            switch (returnCode)
            {
                case ErrorCode.NoError:
                    {
                        if (parameters.Count != 3)
                        {
                            LibraryInstance.ErrorFormat(string.Format("Fetch ItemEntities Response Parameter Error, Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                default:
                    {
                        LibraryInstance.ErrorFormat("Fetch ItemEntities Response Error DebugMessage: {0}", debugMessage);
                        scene.SceneEventManager.ErrorInform(LauguageDictionarySelector.Instance[scene.UsingLanguage]["Unknown Error"], LauguageDictionarySelector.Instance[scene.UsingLanguage]["Fetch ItemEntities Error"]);
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
                    int itemEntityID = (int)parameters[(byte)FetchItemEntitiesResponseParameterCode.ItemEntityID];
                    int itemID = (int)parameters[(byte)FetchItemEntitiesResponseParameterCode.ItemID];
                    DSVector3 position = (DSVector3)parameters[(byte)FetchItemEntitiesResponseParameterCode.Position];
                    ItemEntity itemEntity = new ItemEntity(itemEntityID, itemID, scene.SceneID, position);
                    scene.ItemEntityManager.LoadItemEntity(itemEntity);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("Fetch ItemEntities Response Parameter Cast Error");
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
