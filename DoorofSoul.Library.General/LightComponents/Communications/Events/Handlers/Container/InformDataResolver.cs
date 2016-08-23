using DoorofSoul.Library.General.BasicTypeHelpers;
using DoorofSoul.Library.General.KnowledgeComponents.StatusEffects;
using DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Container.InformData;
using DoorofSoul.Protocol.Communication.Channels;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.InformDataParameters.Container;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Container
{
    public class InformDataResolver : ContainerEventHandler
    {
        private readonly Dictionary<ContainerInformDataCode, InformDataHandler> informTable;

        internal InformDataResolver(NatureComponents.Container container) : base(container, 2)
        {
            informTable = new Dictionary<ContainerInformDataCode, InformDataHandler>
            {
                { ContainerInformDataCode.LifePointChange, new InformLifePointChangeHandler(container) },
                { ContainerInformDataCode.EnergyPointChange, new InformEnergyPointChangeHandler(container) },
                { ContainerInformDataCode.ContainerStatusEffectInfoChange, new InformContainerStatusEffectInfoChangeHandler(container) }
            };
        }

        internal override bool Handle(ContainerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(eventCode, parameters))
            {
                ContainerInformDataCode informCode = (ContainerInformDataCode)parameters[(byte)InformDataEventParameterCode.InformCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)InformDataEventParameterCode.Parameters];
                if (informTable.ContainsKey(informCode))
                {
                    return informTable[informCode].Handle(informCode, resolvedParameters);
                }
                else
                {
                    LibraryInstance.ErrorFormat("Container InformData Event Not Exist Inform Code: {0}", informCode);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        internal void SendInform(ContainerInformDataCode informCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> informDataParameters = new Dictionary<byte, object>
            {
                { (byte)InformDataEventParameterCode.InformCode, (byte)informCode },
                { (byte)InformDataEventParameterCode.Parameters, parameters }
            };
            container.ContainerEventManager.SendEvent(ContainerEventCode.InformData, informDataParameters, ContainerCommunicationChannel.Answer);
        }
        public void InformLifePointChange(decimal lifePoint)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)InformLifePointChangeParameterCode.NewLifePoint, new DSDecimal { value = lifePoint } },
            };
            SendInform(ContainerInformDataCode.LifePointChange, parameters);
        }
        public void InformEnergyPointChange(decimal energyPoint)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)InformEnergyPointChangeParameterCode.NewEnergyPoint, new DSDecimal { value = energyPoint } },
            };
            SendInform(ContainerInformDataCode.EnergyPointChange, parameters);
        }
        public void InformLoadContainerStatusEffectInfo(ContainerStatusEffectInfo info, bool isLoad)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)InformContainerStatusEffectInfoChangeParameterCode.ContainerStatusEffectInfo, info },
                { (byte)InformContainerStatusEffectInfoChangeParameterCode.IsLoad, isLoad }
            };
            SendInform(ContainerInformDataCode.ContainerStatusEffectInfoChange, parameters);
        }
    }
}
