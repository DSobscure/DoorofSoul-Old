using DoorofSoul.Library.General.Events.Handlers.Entity.InformData;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Entity
{
    public class InformDataResolver : EntityEventHandler
    {
        protected readonly Dictionary<EntityInformDataCode, InformDataHandler> informTable;

        public InformDataResolver(General.Entity entity) : base(entity)
        {
            informTable = new Dictionary<EntityInformDataCode, InformDataHandler>
            {
                { EntityInformDataCode.FetchDataError, new InformFetchDataErrorHandler(entity) },
            };
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 3)
            {
                debugMessage = string.Format("Entity Inform Data Event Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(EntityEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(eventCode, parameters))
            {
                EntityInformDataCode informCode = (EntityInformDataCode)parameters[(byte)InformDataEventParameterCode.InformCode];
                ErrorCode returnCode = (ErrorCode)parameters[(byte)InformDataEventParameterCode.ReturnCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)InformDataEventParameterCode.Parameters];
                if (informTable.ContainsKey(informCode))
                {
                    return informTable[informCode].Handle(informCode, returnCode, resolvedParameters);
                }
                else
                {
                    LibraryLog.ErrorFormat("Entity InformData Event Not Exist Inform Code: {0}", informCode);
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
