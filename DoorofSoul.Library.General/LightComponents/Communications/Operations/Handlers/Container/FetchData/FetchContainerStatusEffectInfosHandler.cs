using DoorofSoul.Library.General.KnowledgeComponents.StatusEffects;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Container;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Container.FetchData
{
    internal class FetchContainerStatusEffectInfosHandler : FetchDataHandler
    {
        internal FetchContainerStatusEffectInfosHandler(NatureComponents.Container container) : base(container, 0)
        {
        }

        internal override bool Handle(ContainerFetchDataCode fetchCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(fetchCode, parameters))
            {
                try
                {
                    foreach (ContainerStatusEffectInfo info in container.ContainerStatusEffectManager.StatusEffectInfos)
                    {
                        var result = new Dictionary<byte, object>
                        {
                            { (byte)FetchContainerStatusEffectInfosResponseParameterCode.ContainerStatusEffectInfo, info },
                        };
                        SendResponse(fetchCode, result);
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("FetchContainerStatusEffectInfos Invalid Cast!");
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
