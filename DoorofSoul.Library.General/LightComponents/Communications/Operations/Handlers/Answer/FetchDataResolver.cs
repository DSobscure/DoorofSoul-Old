using DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Answer.FetchData;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataParameters;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Answer
{
    public class FetchDataResolver : AnswerOperationHandler
    {
        private readonly Dictionary<AnswerFetchDataCode, FetchDataHandler> fetchTable;

        internal FetchDataResolver(ThroneComponents.Answer answer) : base(answer, 2)
        {
            fetchTable = new Dictionary<AnswerFetchDataCode, FetchDataHandler>
            {
                { AnswerFetchDataCode.Souls, new FetchSoulsHandler(answer) },
                { AnswerFetchDataCode.Containers, new FetchContainersHandler(answer) },
                { AnswerFetchDataCode.SoulContainerLinks, new FetchSoulContainerLinksHandler(answer) },
            };
        }

        internal override bool Handle(AnswerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                string debugMessage;
                AnswerFetchDataCode fetchCode = (AnswerFetchDataCode)parameters[(byte)FetchDataParameterCode.FetchDataCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)FetchDataParameterCode.Parameters];
                if (fetchTable.ContainsKey(fetchCode))
                {
                    return fetchTable[fetchCode].Handle(fetchCode, resolvedParameters);
                }
                else
                {
                    debugMessage = string.Format("Answer Fetch Operation Not Exist Fetch Code: {0}", fetchCode);
                    SendError(operationCode, ErrorCode.InvalidOperation, debugMessage);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        internal void SendOperation(AnswerFetchDataCode fetchCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> fetchDataParameters = new Dictionary<byte, object>
            {
                { (byte)FetchDataParameterCode.FetchDataCode, (byte)fetchCode },
                { (byte)FetchDataParameterCode.Parameters, parameters }
            };
            answer.AnswerOperationManager.SendOperation(AnswerOperationCode.FetchData, fetchDataParameters);
        }

        public void FetchSouls()
        {
            SendOperation(AnswerFetchDataCode.Souls, new Dictionary<byte, object>());
        }
        public void FetchContainers()
        {
            SendOperation(AnswerFetchDataCode.Containers, new Dictionary<byte, object>());
        }
        public void FetchSoulContainerLinks()
        {
            SendOperation(AnswerFetchDataCode.SoulContainerLinks, new Dictionary<byte, object>());
        }
    }
}
