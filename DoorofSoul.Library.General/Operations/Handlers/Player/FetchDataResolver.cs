using DoorofSoul.Library.General.Operations.Handlers.Player.FetchData;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataParameters;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers.Player
{
    public class FetchDataResolver : PlayerOperationHandler
    {
        protected readonly Dictionary<PlayerFetchDataCode, FetchDataHandler> fetchTable;

        public FetchDataResolver(General.Player player) : base(player)
        {
            fetchTable = new Dictionary<PlayerFetchDataCode, FetchDataHandler>
            {
                { PlayerFetchDataCode.SystemVersion, new FetchSystemVersionHandler(player) },
                { PlayerFetchDataCode.Answer, new FetchAnswerHandler(player) },
                { PlayerFetchDataCode.Worlds, new FetchWorldsHandler(player) },
            };
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 2)
            {
                debugMessage = string.Format("Player Fetch Data Operation Parameter Error Parameter Count: {0}", parameter.Count);
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
                string debugMessage;
                PlayerFetchDataCode fetchCode = (PlayerFetchDataCode)parameters[(byte)FetchDataParameterCode.FetchDataCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)FetchDataParameterCode.Parameters];
                if (fetchTable.ContainsKey(fetchCode))
                {
                    return fetchTable[fetchCode].Handle(fetchCode, resolvedParameters);
                }
                else
                {
                    debugMessage = string.Format("Player Fetch Operation Not Exist Fetch Code: {0}", fetchCode);
                    SendError(operationCode, ErrorCode.InvalidOperation, debugMessage);
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
