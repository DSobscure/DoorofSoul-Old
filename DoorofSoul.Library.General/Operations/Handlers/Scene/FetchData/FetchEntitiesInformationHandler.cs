﻿using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Scene;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers.Scene.FetchData
{
    internal class FetchEntitiesHandler : FetchDataHandler
    {
        internal FetchEntitiesHandler(General.Scene scene) : base(scene)
        {
        }

        internal override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 0)
            {
                debugMessage = string.Format("Scene Fetch Entities Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        internal override bool Handle(SceneFetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            if (base.Handle(fetchCode, parameter))
            {
                try
                {
                    foreach (General.Entity entity in scene.Entities)
                    {
                        var result = new Dictionary<byte, object>
                            {
                                { (byte)FetchEntitiesResponseParameterCode.EntityID, entity.EntityID },
                                { (byte)FetchEntitiesResponseParameterCode.EntityName, entity.EntityName },
                                { (byte)FetchEntitiesResponseParameterCode.EntitySpaceProperties, entity.SpaceProperties }
                            };
                        SendResponse(fetchCode, result);
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("FetchEntities Invalid Cast!");
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
