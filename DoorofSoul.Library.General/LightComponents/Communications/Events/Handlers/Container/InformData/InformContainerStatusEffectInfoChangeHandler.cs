using System;
using System.Collections.Generic;
using System.Linq;
using DoorofSoul.Protocol;
using DoorofSoul.Library.General.KnowledgeComponents.StatusEffects;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.InformDataParameters.Container;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Container.InformData
{
    internal class InformContainerStatusEffectInfoChangeHandler : InformDataHandler
    {
        internal InformContainerStatusEffectInfoChangeHandler(NatureComponents.Container container) : base(container, 2)
        {
        }

        internal override bool Handle(ContainerInformDataCode informCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(informCode, parameters))
            {
                try
                {
                    ContainerStatusEffectInfo info = (ContainerStatusEffectInfo)parameters[(byte)InformContainerStatusEffectInfoChangeParameterCode.ContainerStatusEffectInfo];
                    DataChangeTypeCode changeTypeCode = (DataChangeTypeCode)parameters[(byte)InformContainerStatusEffectInfoChangeParameterCode.DataChangeType];
                    if (container.ContainerID == info.AffectedContainerID)
                    {
                        switch(changeTypeCode)
                        {
                            case DataChangeTypeCode.Load:
                                container.ContainerStatusEffectManager.LoadStatusEffectInfo(info);
                                break;
                            case DataChangeTypeCode.Unload:
                                container.ContainerStatusEffectManager.UnloadStatusEffectInfo(info);
                                break;
                            case DataChangeTypeCode.Update:
                                break;
                            case DataChangeTypeCode.Initial:
                                break;
                            case DataChangeTypeCode.ClearAll:
                                break;
                            default:
                                break;
                        }
                        return true;
                    }
                    else
                    {
                        LibraryInstance.ErrorFormat("InformContainerStatusEffectInfoChange Error, AffectedContainerID: {0}, ContainerID: {1}", info.AffectedContainerID, container.ContainerID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("InformContainerStatusEffectInfoChange Parameter Cast Error");
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
