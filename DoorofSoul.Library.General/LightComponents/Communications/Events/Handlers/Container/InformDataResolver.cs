using DoorofSoul.Library.General.BasicTypeHelpers;
using DoorofSoul.Library.General.KnowledgeComponents.StatusEffects;
using DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Container.InformData;
using DoorofSoul.Library.General.NatureComponents.ContainerElements;
using DoorofSoul.Protocol;
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
                { ContainerInformDataCode.ContainerStatusEffectInfoChange, new InformContainerStatusEffectInfoChangeHandler(container) },
                { ContainerInformDataCode.InventoryItemIntoChange, new InformInventoryItemIntoChangeHandler(container) },

                { ContainerInformDataCode.BulletDamageChange, new InformBulletDamageChangeHandler(container) },
                { ContainerInformDataCode.MoveSpeedChange, new InformMoveSpeedChangeHandler(container) },
                { ContainerInformDataCode.BulletSpeedChange, new InformBulletSpeedChangeHandler(container) },
                { ContainerInformDataCode.TransparancyChange, new InformTransparancyChangeHandler(container) },
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
        public void InformContainerStatusEffectInfoChange(ContainerStatusEffectInfo info, DataChangeTypeCode changeTypeCode)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)InformContainerStatusEffectInfoChangeParameterCode.ContainerStatusEffectInfo, info },
                { (byte)InformContainerStatusEffectInfoChangeParameterCode.DataChangeType, (byte)changeTypeCode }
            };
            SendInform(ContainerInformDataCode.ContainerStatusEffectInfoChange, parameters);
        }
        public void InformInventoryItemInfoChange(InventoryItemInfo info)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)InformInventoryItemInfoChangeParameterCode.ItemInfoID, info.inventoryItemInfoID },
                { (byte)InformInventoryItemInfoChangeParameterCode.Item, info.item },
                { (byte)InformInventoryItemInfoChangeParameterCode.Count, info.count },
                { (byte)InformInventoryItemInfoChangeParameterCode.PositionIndex, info.positionIndex }
            };
            SendInform(ContainerInformDataCode.InventoryItemIntoChange, parameters);
        }

        public void InformBulletDamageChange(int damage)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)InformBulletDamageChangeParameterCode.Damage, damage },
            };
            SendInform(ContainerInformDataCode.BulletDamageChange, parameters);
        }
        public void InformMoveSpeedChange(int speed)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)InformMoveSpeedChangeParameterCode.MoveSpeed, speed },
            };
            SendInform(ContainerInformDataCode.MoveSpeedChange, parameters);
        }
        public void InformBulletSpeedChange(int speed)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)InformBulletSpeedChangeParameterCode.BulletSpeed, speed },
            };
            SendInform(ContainerInformDataCode.BulletSpeedChange, parameters);
        }
        public void InformTranspancyChange(int transpancy)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)InformTransparancyChangeParameterCode.Transparancy, transpancy },
            };
            SendInform(ContainerInformDataCode.TransparancyChange, parameters);
        }
    }
}
