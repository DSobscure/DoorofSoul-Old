using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationParameters;
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
                    debugMessage = "fetch operation not exist";
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
