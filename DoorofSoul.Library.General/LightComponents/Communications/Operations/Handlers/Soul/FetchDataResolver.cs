using DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Soul.FetchData;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataParameters;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Soul
{
    public class FetchDataResolver : SoulOperationHandler
    {
        private readonly Dictionary<SoulFetchDataCode, FetchDataHandler> fetchTable;

        internal FetchDataResolver(MindComponents.Soul soul) : base(soul, 2)
        {
            fetchTable = new Dictionary<SoulFetchDataCode, FetchDataHandler>
            {
                { SoulFetchDataCode.SkillInfos, new FetchSkillInfosHandler(soul) },
            };
        }

        internal override bool Handle(SoulOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                string debugMessage;
                SoulFetchDataCode fetchCode = (SoulFetchDataCode)parameters[(byte)FetchDataParameterCode.FetchDataCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)FetchDataParameterCode.Parameters];
                if (fetchTable.ContainsKey(fetchCode))
                {
                    return fetchTable[fetchCode].Handle(fetchCode, resolvedParameters);
                }
                else
                {
                    debugMessage = string.Format("Soul Fetch Operation Not Exist Fetch Code: {0}", fetchCode);
                    SendError(operationCode, ErrorCode.InvalidOperation, debugMessage);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        internal void SendOperation(SoulFetchDataCode fetchCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> fetchDataParameters = new Dictionary<byte, object>
            {
                { (byte)FetchDataParameterCode.FetchDataCode, (byte)fetchCode },
                { (byte)FetchDataParameterCode.Parameters, parameters }
            };
            soul.SoulOperationManager.SendOperation(SoulOperationCode.FetchData, fetchDataParameters);
        }
        public void FetchSkillInfos()
        {
            SendOperation(SoulFetchDataCode.SkillInfos, new Dictionary<byte, object>());
        }
    }
}
