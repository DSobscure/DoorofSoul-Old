using DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Scene.InformData;
using DoorofSoul.Library.General.NatureComponents.SceneElements;
using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.InformDataParameters.Scene;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Scene
{
    public class InformDataResolver : SceneEventHandler
    {
        protected readonly Dictionary<SceneInformDataCode, InformDataHandler> informTable;

        internal InformDataResolver(NatureComponents.Scene scene) : base(scene, 2)
        {
            informTable = new Dictionary<SceneInformDataCode, InformDataHandler>
            {
                { SceneInformDataCode.ItemEntityChange, new InformItemEntityChangeHandler(scene) },
            };
        }

        internal override bool Handle(SceneEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(eventCode, parameters))
            {
                SceneInformDataCode informCode = (SceneInformDataCode)parameters[(byte)InformDataEventParameterCode.InformCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)InformDataEventParameterCode.Parameters];
                if (informTable.ContainsKey(informCode))
                {
                    return informTable[informCode].Handle(informCode, resolvedParameters);
                }
                else
                {
                    LibraryInstance.ErrorFormat("Scene InformData Event Not Exist Inform Code: {0}", informCode);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        internal void SendInform(SceneInformDataCode informCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> informDataParameters = new Dictionary<byte, object>
            {
                { (byte)InformDataEventParameterCode.InformCode, (byte)informCode },
                { (byte)InformDataEventParameterCode.Parameters, parameters }
            };
            scene.SceneEventManager.SendEvent(SceneEventCode.InformData, informDataParameters);
        }
        public void InformItemEntityChange(ItemEntity itemEntity, DataChangeTypeCode changeTypeCode)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)InformItemEntityChangeParameterCode.ItemEntityID, itemEntity.ItemEntityID },
                { (byte)InformItemEntityChangeParameterCode.ItemID, itemEntity.ItemID },
                { (byte)InformItemEntityChangeParameterCode.Position, itemEntity.Position },
                { (byte)InformItemEntityChangeParameterCode.DataChangeType, changeTypeCode },
            };
            SendInform(SceneInformDataCode.ItemEntityChange, parameters);
        }
    }
}
