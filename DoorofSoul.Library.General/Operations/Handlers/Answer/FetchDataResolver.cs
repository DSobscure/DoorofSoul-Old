using DoorofSoul.Library.General.Operations.Handlers.Answer.FetchData;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataParameters;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Language;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers.Answer
{
    public class FetchDataResolver : AnswerOperationHandler
    {
        protected readonly Dictionary<AnswerFetchDataCode, FetchDataHandler> fetchTable;

        public FetchDataResolver(General.Answer answer) : base(answer)
        {
            fetchTable = new Dictionary<AnswerFetchDataCode, FetchDataHandler>
            {
                { AnswerFetchDataCode.Souls, new FetchSoulsHandler(answer) },
                { AnswerFetchDataCode.Containers, new FetchContainersHandler(answer) },
                { AnswerFetchDataCode.SoulContainerLinks, new FetchSoulContainerLinksHandler(answer) },
            };
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 2)
            {
                debugMessage = string.Format("Answer Fetch Data Operation Parameter Error Parameter Count: {0}", parameter.Count);
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
                    SendError(operationCode, ErrorCode.InvalidOperation, debugMessage, LauguageDictionarySelector.Instance[answer.Player.UsingLanguage]["Not Existed Fetch Operation"]);
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
