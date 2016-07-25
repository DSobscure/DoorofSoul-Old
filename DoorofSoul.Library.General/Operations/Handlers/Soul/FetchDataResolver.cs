﻿using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataParameters;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Language;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers.Soul
{
    public class FetchDataResolver : SoulOperationHandler
    {
        protected readonly Dictionary<SoulFetchDataCode, FetchDataHandler> fetchTable;

        public FetchDataResolver(General.Soul soul) : base(soul)
        {
            fetchTable = new Dictionary<SoulFetchDataCode, FetchDataHandler>
            {

            };
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 2)
            {
                debugMessage = string.Format("Soul Fetch Data Operation Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(SoulOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                string debugMessage;
                SoulFetchDataCode fetchCode = (SoulFetchDataCode)parameters[(byte)FetchDataParameterCode.FetchDataCode];
                Dictionary<byte, object> fetchParameters = (Dictionary<byte, object>)parameters[(byte)FetchDataParameterCode.Parameters];
                if (fetchTable.ContainsKey(fetchCode))
                {
                    return fetchTable[fetchCode].Handle(fetchCode, parameters);
                }
                else
                {
                    debugMessage = string.Format("Soul Fetch Operation Not Exist Fetch Code: {0}", fetchCode);
                    SendError(operationCode, ErrorCode.InvalidOperation, debugMessage, LauguageDictionarySelector.Instance[soul.Answer.Player.UsingLanguage]["Not Existed Fetch Operation"]);
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
