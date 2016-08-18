using DoorofSoul.Library.General.BasicTypeHelpers;
using DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Soul.InformData;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.InformDataParameters.Soul;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Soul
{
    public class InformDataResolver : SoulEventHandler
    {
        private readonly Dictionary<SoulInformDataCode, InformDataHandler> informTable;

        internal InformDataResolver(ThroneComponents.Soul soul) : base(soul, 2)
        {
            informTable = new Dictionary<SoulInformDataCode, InformDataHandler>
            {
                { SoulInformDataCode.CorePointChange, new InformCorePointChangeHandler(soul) },
                { SoulInformDataCode.SpiritPointChange, new InformSpiritPointChangeHandler(soul) }
            };
        }

        internal override bool Handle(SoulEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(eventCode, parameters))
            {
                SoulInformDataCode informCode = (SoulInformDataCode)parameters[(byte)InformDataEventParameterCode.InformCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)InformDataEventParameterCode.Parameters];
                if (informTable.ContainsKey(informCode))
                {
                    return informTable[informCode].Handle(informCode, resolvedParameters);
                }
                else
                {
                    LibraryInstance.ErrorFormat("Soul InformData Event Not Exist Inform Code: {0}", informCode);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        internal void SendInform(SoulInformDataCode informCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> informDataParameters = new Dictionary<byte, object>
            {
                { (byte)InformDataEventParameterCode.InformCode, (byte)informCode },
                { (byte)InformDataEventParameterCode.Parameters, parameters }
            };
            soul.SoulEventManager.SendEvent(SoulEventCode.InformData, informDataParameters);
        }
        public void InformCorePointChange(decimal corePoint)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)InformCorePointChangeParameterCode.NewCorePoint, new DSDecimal { value = corePoint } },
            };
            SendInform(SoulInformDataCode.CorePointChange, parameters);
        }
        public void InformSpiritPointChange(decimal spiritPoint)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)InformSpiritPointChangeParameterCode.NewSpiritPoint, new DSDecimal { value = spiritPoint } },
            };
            SendInform(SoulInformDataCode.SpiritPointChange, parameters);
        }
    }
}
