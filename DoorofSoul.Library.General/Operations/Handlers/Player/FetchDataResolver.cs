using DoorofSoul.Library.General.Operations.Handlers.Player.FetchData;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataParameters;
using DoorofSoul.Protocol.Communication.FetchDataParameters.Player;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers.Player
{
    public class FetchDataResolver : PlayerOperationHandler
    {
        private readonly Dictionary<PlayerFetchDataCode, FetchDataHandler> fetchTable;

        internal FetchDataResolver(General.Player player) : base(player)
        {
            fetchTable = new Dictionary<PlayerFetchDataCode, FetchDataHandler>
            {
                { PlayerFetchDataCode.SystemVersion, new FetchSystemVersionHandler(player) },
                { PlayerFetchDataCode.Answer, new FetchAnswerHandler(player) },
                { PlayerFetchDataCode.Worlds, new FetchWorldsHandler(player) },
                { PlayerFetchDataCode.Scene, new FetchSceneHandler(player) },
            };
        }

        internal override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
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

        internal override bool Handle(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
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
        internal void SendOperation(PlayerFetchDataCode fetchCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> fetchDataParameters = new Dictionary<byte, object>
            {
                { (byte)FetchDataParameterCode.FetchDataCode, (byte)fetchCode },
                { (byte)FetchDataParameterCode.Parameters, parameters }
            };
            player.PlayerOperationManager.SendOperation(PlayerOperationCode.FetchData, fetchDataParameters);
        }

        public void FetchAnswer()
        {
            SendOperation(PlayerFetchDataCode.Answer, new Dictionary<byte, object>());
        }
        public void FetchWorlds()
        {
            SendOperation(PlayerFetchDataCode.Worlds, new Dictionary<byte, object>());
        }
        public void FetchScene(int sceneID)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)FetchSceneParameterCode.SceneID, sceneID }
            };
            SendOperation(PlayerFetchDataCode.Scene, parameters);
        }
    }
}
