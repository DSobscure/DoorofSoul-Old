using DoorofSoul.Library.General.Responses.Handlers.Player.FetchData;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.ResponseParameters;
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
                { PlayerFetchDataCode.Scene, new FetchSceneResponseHandler(player) },
            };
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 4)
            {
                debugMessage = string.Format("Player Fetch Data Response Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                PlayerFetchDataCode fetchCode = (PlayerFetchDataCode)parameters[(byte)FetchDataResponseParameterCode.FetchCode];
                ErrorCode returnCode = (ErrorCode)parameters[(byte)FetchDataResponseParameterCode.ReturnCode];
                string debugMessage = (string)parameters[(byte)FetchDataResponseParameterCode.DebugMessage];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)FetchDataResponseParameterCode.Parameters];
                if (fetchResponseTable.ContainsKey(fetchCode))
                {
                    return fetchResponseTable[fetchCode].Handle(fetchCode, returnCode, debugMessage, resolvedParameters);
                }
                else
                {
                    LibraryLog.ErrorFormat("Player FetchData Response Not Exist Fetch Code: {0}", fetchCode);
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
