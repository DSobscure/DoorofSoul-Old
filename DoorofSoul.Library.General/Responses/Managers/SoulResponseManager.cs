using DoorofSoul.Library.General.Responses.Handlers;
using DoorofSoul.Library.General.Responses.Handlers.Soul;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.ResponseParameters.Answer;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Managers
{
    internal class SoulResponseManager
    {
        protected readonly Dictionary<SoulOperationCode, SoulResponseHandler> operationTable;
        protected readonly Soul soul;

        internal SoulResponseManager(Soul soul)
        {
            this.soul = soul;
            operationTable = new Dictionary<SoulOperationCode, SoulResponseHandler>
            {
                { SoulOperationCode.FetchData, new FetchDataResponseResolver(soul) },
                { SoulOperationCode.SkillOperation, new SkillOperationResponseHandler(soul) }
            };
        }

        internal void Operate(SoulOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, returnCode, debugMessage, parameters))
                {
                    LibraryInstance.ErrorFormat("Soul Response Error: {0} from AnswerID: {1}", operationCode, soul.SoulID);
                }
            }
            else
            {
                LibraryInstance.ErrorFormat("Unknow Soul Response:{0} from AnswerID: {1}", operationCode, soul.SoulID);
            }
        }

        internal void SendResponse(SoulOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> responseData = new Dictionary<byte, object>
            {
                { (byte)SoulResponseParameterCode.SoulID, soul.SoulID },
                { (byte)SoulResponseParameterCode.OperationCode, (byte)operationCode },
                { (byte)SoulResponseParameterCode.ReturnCode, (short)errorCode },
                { (byte)SoulResponseParameterCode.DebugMessage, debugMessage },
                { (byte)SoulResponseParameterCode.Parameters, parameters }
            };
            soul.Answer.AnswerResponseManager.SendResponse(AnswerOperationCode.SoulOperation, ErrorCode.NoError, null, responseData);
        }
    }
}
