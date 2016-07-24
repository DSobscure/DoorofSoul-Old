using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationParameters;
using DoorofSoul.Server.Operations.Handlers.FetchDataHandlers;
using Photon.SocketServer;
using System.Collections.Generic;

namespace DoorofSoul.Server.Operations.Handlers
{
    public class FetchDataManager : OperationHandler
    {
        protected readonly Dictionary<PlayerFetchDataCode, FetchDataHandler> fetchTable;
        public FetchDataManager(Peer peer) : base(peer)
        {
            fetchTable = new Dictionary<PlayerFetchDataCode, FetchDataHandler>
            {
                { PlayerFetchDataCode.SystemVersion, new FetchSystemVersionHandler(peer) },
                { PlayerFetchDataCode.Answer, new FetchAnswerHandler(peer) },
                { PlayerFetchDataCode.Souls, new FetchSoulsHandler(peer) },
                { PlayerFetchDataCode.Containers, new FetchContainersHandler(peer) },
                { PlayerFetchDataCode.SoulContainerConnections, new FetchSoulContainerConnectionsHandler(peer) },
                { PlayerFetchDataCode.Scene, new FetchSceneHandler(peer) },
                { PlayerFetchDataCode.SceneEntitiesInformation, new FetchSceneEntitiesInformationHandler(peer) },
            };
        }

        public override bool Handle(OperationRequest operationRequest)
        {
            if(base.Handle(operationRequest))
            {
                string debugMessage;
                PlayerFetchDataCode fetchCode = (PlayerFetchDataCode)operationRequest.Parameters[(byte)FetchDataParameterCode.FetchDataCode];
                Dictionary<byte, object> parameters = (Dictionary<byte, object>)operationRequest.Parameters[(byte)FetchDataParameterCode.Parameters];
                if (fetchTable.ContainsKey(fetchCode))
                {
                    return fetchTable[fetchCode].Handle(fetchCode, parameters);
                }
                else
                {
                    debugMessage = string.Format("fetch operation not exist fetch code: {0}", fetchCode);
                    SendError(operationRequest.OperationCode, ErrorCode.InvalidOperation, debugMessage, LauguageDictionarySelector.Instance[peer.UsingLanguage]["Not Existed Fetch Operation"]);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if(parameter.Count != 2)
            {
                debugMessage = "Fetch Data Operation Parameter Error";
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }
    }
}
