using DoorofSoul.Library.General.Responses.Handlers.Answer.FetchData;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers.Answer
{
    internal class FetchDataResponseResolver : AnswerResponseHandler
    {
        protected readonly Dictionary<AnswerFetchDataCode, FetchDataResponseHandler> fetchResponseTable;

        internal FetchDataResponseResolver(General.Answer answer) : base(answer)
        {
            fetchResponseTable = new Dictionary<AnswerFetchDataCode, FetchDataResponseHandler>
            {
                { AnswerFetchDataCode.Souls, new FetchSoulsResponseHandler(answer) },
                { AnswerFetchDataCode.Containers, new FetchContainersResponseHandler(answer) },
                { AnswerFetchDataCode.SoulContainerLinks, new FetchSoulContainerLinksResponseHandler(answer) },
            };
        }

        internal override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            if (returnCode == ErrorCode.NoError)
            {
                if (parameters.Count != 4)
                {
                    LibraryInstance.ErrorFormat("Answer Fetch Data Response Parameter Error Parameter Count: {0}", parameters.Count);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                LibraryInstance.ErrorFormat("Answer FetchDataResponse Error ErrorCode: {0}, DebugMessage: {1}", returnCode, debugMessage);
                return false;
            }
        }

        internal override bool Handle(AnswerOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, returnCode, debugMessage, parameters))
            {
                AnswerFetchDataCode fetchCode = (AnswerFetchDataCode)parameters[(byte)FetchDataResponseParameterCode.FetchCode];
                ErrorCode resolvedReturnCode = (ErrorCode)parameters[(byte)FetchDataResponseParameterCode.ReturnCode];
                string resolvedDebugMessage = (string)parameters[(byte)FetchDataResponseParameterCode.DebugMessage];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)FetchDataResponseParameterCode.Parameters];
                if (fetchResponseTable.ContainsKey(fetchCode))
                {
                    return fetchResponseTable[fetchCode].Handle(fetchCode, resolvedReturnCode, resolvedDebugMessage, resolvedParameters);
                }
                else
                {
                    LibraryInstance.ErrorFormat("Answer FetchData Response Not Exist Fetch Code: {0}", fetchCode);
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
