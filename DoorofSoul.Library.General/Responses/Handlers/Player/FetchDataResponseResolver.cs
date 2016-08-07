using DoorofSoul.Library.General.Responses.Handlers.Player.FetchData;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers.Player
{
    public class FetchDataResponseResolver : PlayerResponseHandler
    {
        protected readonly Dictionary<PlayerFetchDataCode, FetchDataResponseHandler> fetchResponseTable;

        public FetchDataResponseResolver(General.Player player) : base(player)
        {
            fetchResponseTable = new Dictionary<PlayerFetchDataCode, FetchDataResponseHandler>
            {
                { PlayerFetchDataCode.SystemVersion, new FetchSystemVersionResponseHandler(player) },
                { PlayerFetchDataCode.Answer, new FetchAnswerResponseHandler(player) },
                { PlayerFetchDataCode.Worlds, new FetchWorldsResponseHandler(player) },
                { PlayerFetchDataCode.Scene, new FetchSceneResponseHandler(player) },
            };
        }

        public override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            if (returnCode == ErrorCode.NoError)
            {
                if (parameters.Count != 4)
                {
                    LibraryInstance.ErrorFormat("Player Fetch Data Response Parameter Error Parameter Count: {0}", parameters.Count);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                LibraryInstance.ErrorFormat("Player Fetch Data Response Error ErrorCode: {0}, DebugMessage: {1}", returnCode, debugMessage);
                return false;
            }
        }

        public override bool Handle(PlayerOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, returnCode, debugMessage, parameters))
            {
                PlayerFetchDataCode fetchCode = (PlayerFetchDataCode)parameters[(byte)FetchDataResponseParameterCode.FetchCode];
                ErrorCode resolvedReturnCode = (ErrorCode)parameters[(byte)FetchDataResponseParameterCode.ReturnCode];
                string resolvedDebugMessage = (string)parameters[(byte)FetchDataResponseParameterCode.DebugMessage];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)FetchDataResponseParameterCode.Parameters];
                if (fetchResponseTable.ContainsKey(fetchCode))
                {
                    return fetchResponseTable[fetchCode].Handle(fetchCode, resolvedReturnCode, resolvedDebugMessage, resolvedParameters);
                }
                else
                {
                    LibraryInstance.ErrorFormat("Player FetchData Response Not Exist Fetch Code: {0}", fetchCode);
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
