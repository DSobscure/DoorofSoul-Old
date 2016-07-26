using DoorofSoul.Library.General.Responses.Handlers.Answer.FetchData;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers.Answer
{
    public class FetchDataResponseResolver : AnswerResponseHandler
    {
        protected readonly Dictionary<AnswerFetchDataCode, FetchDataResponseHandler> fetchResponseTable;

        public FetchDataResponseResolver(General.Answer answer) : base(answer)
        {
            fetchResponseTable = new Dictionary<AnswerFetchDataCode, FetchDataResponseHandler>
            {
                { AnswerFetchDataCode.Souls, new FetchSoulsResponseHandler(answer) },
                { AnswerFetchDataCode.Containers, new FetchContainersResponseHandler(answer) },
                { AnswerFetchDataCode.SoulContainerLinks, new FetchSoulContainerLinksResponseHandler(answer) },
            };
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 4)
            {
                debugMessage = string.Format("Answer Fetch Data Response Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(AnswerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                AnswerFetchDataCode fetchCode = (AnswerFetchDataCode)parameters[(byte)FetchDataResponseParameterCode.FetchCode];
                ErrorCode returnCode = (ErrorCode)parameters[(byte)FetchDataResponseParameterCode.ReturnCode];
                string debugMessage = (string)parameters[(byte)FetchDataResponseParameterCode.DebugMessage];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)FetchDataResponseParameterCode.Parameters];
                if (fetchResponseTable.ContainsKey(fetchCode))
                {
                    return fetchResponseTable[fetchCode].Handle(fetchCode, returnCode, debugMessage, resolvedParameters);
                }
                else
                {
                    LibraryLog.ErrorFormat("Answer FetchData Response Not Exist Fetch Code: {0}", fetchCode);
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
