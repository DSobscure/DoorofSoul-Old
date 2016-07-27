using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters;
using System.Collections.Generic;
namespace DoorofSoul.Library.General.Responses.Handlers.Entity
{
    public class FetchDataResponseResolver : EntityResponseHandler
    {
        protected readonly Dictionary<EntityFetchDataCode, FetchDataResponseHandler> fetchResponseTable;

        public FetchDataResponseResolver(General.Entity entity) : base(entity)
        {
            fetchResponseTable = new Dictionary<EntityFetchDataCode, FetchDataResponseHandler>
            {

            };
        }

        public override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            if (returnCode == ErrorCode.NoError)
            {
                if (parameters.Count != 4)
                {
                    LibraryLog.ErrorFormat("Entity Fetch Data Response Parameter Error Parameter Count: {0}", parameters.Count);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                LibraryLog.ErrorFormat("Entity Fetch Data Response Error ErrorCode: {0}, DebugMessage: {1}", returnCode, debugMessage);
                return false;
            }
        }

        public override bool Handle(EntityOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, returnCode, debugMessage, parameters))
            {
                EntityFetchDataCode fetchCode = (EntityFetchDataCode)parameters[(byte)FetchDataResponseParameterCode.FetchCode];
                ErrorCode resolvedReturnCode = (ErrorCode)parameters[(byte)FetchDataResponseParameterCode.ReturnCode];
                string resolvedDebugMessage = (string)parameters[(byte)FetchDataResponseParameterCode.DebugMessage];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)FetchDataResponseParameterCode.Parameters];
                if (fetchResponseTable.ContainsKey(fetchCode))
                {
                    return fetchResponseTable[fetchCode].Handle(fetchCode, resolvedReturnCode, resolvedDebugMessage, resolvedParameters);
                }
                else
                {
                    LibraryLog.ErrorFormat("Entity FetchData Response Not Exist Fetch Code: {0}", fetchCode);
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
