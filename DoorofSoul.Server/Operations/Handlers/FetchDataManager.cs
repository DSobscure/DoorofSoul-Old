using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationParameters;
using DoorofSoul.Server.Operations.Handlers.FetchDataHandlers;
using Photon.SocketServer;
using System.Collections.Generic;

namespace DoorofSoul.Server.Operations.Handlers
{
    public class FetchDataManager : OperationHandler
    {
        protected readonly Dictionary<FetchDataCode, FetchDataHandler> fetchTable;
        public FetchDataManager(Peer peer) : base(peer)
        {
            fetchTable = new Dictionary<FetchDataCode, FetchDataHandler>
            {
                { FetchDataCode.SystemVersion, new FetchSystemVersionHandler(peer) },
                { FetchDataCode.Answer, new FetchAnswerHandler(peer) },
                { FetchDataCode.Souls, new FetchSoulsHandler(peer) },
                { FetchDataCode.Containers, new FetchContainersHandler(peer) },
                { FetchDataCode.SoulContainerConnections, new FetchSoulContainerConnectionsHandler(peer) },
                { FetchDataCode.Scene, new FetchSceneHandler(peer) },
                { FetchDataCode.SceneEntitiesInformation, new FetchSceneEntitiesInformationHandler(peer) },
            };
        }

        public override bool Handle(OperationRequest operationRequest)
        {
            if(base.Handle(operationRequest))
            {
                string debugMessage;
                FetchDataCode fetchCode = (FetchDataCode)operationRequest.Parameters[(byte)FetchDataOperationParameterCode.FetchDataCode];
                Dictionary<byte, object> parameters = (Dictionary<byte, object>)operationRequest.Parameters[(byte)FetchDataOperationParameterCode.Parameters];
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
