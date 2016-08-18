using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Container;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers.Container.FetchData
{
    internal class FetchEntityHandler : FetchDataHandler
    {
        internal FetchEntityHandler(General.Container container) : base(container, 0)
        {
            
        }

        internal override bool Handle(ContainerFetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            if (base.Handle(fetchCode, parameter))
            {
                try
                {
                    General.Entity entity = container.Entity;
                    var result = new Dictionary<byte, object>
                    {
                        { (byte)FetchEntityResponseParameterCode.EntityID, entity.EntityID },
                        { (byte)FetchEntityResponseParameterCode.EntityName, entity.EntityName },
                        { (byte)FetchEntityResponseParameterCode.LocatedSceneID, entity.LocatedSceneID },
                        { (byte)FetchEntityResponseParameterCode.EntitySpaceProperties, entity.SpaceProperties },
                    };        
                    SendResponse(fetchCode, result);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("FetchEntity Invalid Cast!");
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
