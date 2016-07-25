using DoorofSoul.Library.General.Events.Handlers.Soul.InformData;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Soul
{
    public class InformDataResolver : SoulEventHandler
    {
        protected readonly Dictionary<SoulInformDataCode, InformDataHandler> informTable;

        public InformDataResolver(General.Soul soul) : base(soul)
        {
            informTable = new Dictionary<SoulInformDataCode, InformDataHandler>
            {
                { SoulInformDataCode.FetchDataError, new InformFetchDataErrorHandler(soul) },
            };
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 3)
            {
                debugMessage = string.Format("Soul Inform Data Event Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(SoulEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(eventCode, parameters))
            {
                SoulInformDataCode informCode = (SoulInformDataCode)parameters[(byte)InformDataEventParameterCode.InformCode];
                ErrorCode returnCode = (ErrorCode)parameters[(byte)InformDataEventParameterCode.ReturnCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)InformDataEventParameterCode.Parameters];
                if (informTable.ContainsKey(informCode))
                {
                    return informTable[informCode].Handle(informCode, returnCode, resolvedParameters);
                }
                else
                {
                    LibraryLog.ErrorFormat("Soul InformData Event Not Exist Inform Code: {0}", informCode);
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
