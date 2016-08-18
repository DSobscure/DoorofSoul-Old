using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataParameters;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.World
{
    public class FetchDataResolver : WorldOperationHandler
    {
        protected readonly Dictionary<WorldFetchDataCode, FetchDataHandler> fetchTable;

        public FetchDataResolver(NatureComponents.World world) : base(world, 2)
        {
            fetchTable = new Dictionary<WorldFetchDataCode, FetchDataHandler>
            {
                
            };
        }

        public override bool Handle(WorldOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                string debugMessage;
                WorldFetchDataCode fetchCode = (WorldFetchDataCode)parameters[(byte)FetchDataParameterCode.FetchDataCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)FetchDataParameterCode.Parameters];
                if (fetchTable.ContainsKey(fetchCode))
                {
                    return fetchTable[fetchCode].Handle(fetchCode, resolvedParameters);
                }
                else
                {
                    debugMessage = string.Format("World Fetch Operation Not Exist Fetch Code: {0}", fetchCode);
                    SendError(operationCode, ErrorCode.InvalidOperation, debugMessage);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        internal void SendOperation(WorldFetchDataCode fetchCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> fetchDataParameters = new Dictionary<byte, object>
            {
                { (byte)FetchDataParameterCode.FetchDataCode, (byte)fetchCode },
                { (byte)FetchDataParameterCode.Parameters, parameters }
            };
            world.WorldOperationManager.SendOperation(WorldOperationCode.FetchData, fetchDataParameters);
        }
    }
}
