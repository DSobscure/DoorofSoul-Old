using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    bool isLoad = (bool)parameters[(byte)InformContainerStatusEffectInfoChangeParameterCode.IsLoad];
                    if (container.ContainerID == info.AffectedContainerID)
                    {
                        if(isLoad)
                        {
                            container.ContainerStatusEffectManager.LoadStatusEffectInfo(info);
                        }
                        else
                        {
                            container.ContainerStatusEffectManager.UnloadStatusEffectInfo(info);
                        }
                        return true;
                    }
                    else
                    {
                        LibraryInstance.ErrorFormat("InformLoadContainerStatusEffectInfo Error, AffectedContainerID: {0}, ContainerID: {1}", info.AffectedContainerID, container.ContainerID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("InformLoadContainerStatusEffectInfo Parameter Cast Error");
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
