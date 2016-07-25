using DoorofSoul.Library.General.Events.Handlers.Answer.InformData;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Answer
{
    public class InformDataResolver : AnswerEventHandler
    {
        protected readonly Dictionary<AnswerInformDataCode, InformDataHandler> informTable;

        public InformDataResolver(General.Answer answer) : base(answer)
        {
            informTable = new Dictionary<AnswerInformDataCode, InformDataHandler>
            {
                { AnswerInformDataCode.FetchDataError, new InformFetchDataErrorHandler(answer) },
                { AnswerInformDataCode.Soul, new InformSoulHandler(answer) },
                { AnswerInformDataCode.Container, new InformContainerHandler(answer) },
                { AnswerInformDataCode.SoulContainerLink, new InformSoulContainerLinkHandler(answer) },
            };
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 3)
            {
                debugMessage = string.Format("Answer Inform Data Event Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(AnswerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(eventCode, parameters))
            {
                AnswerInformDataCode informCode = (AnswerInformDataCode)parameters[(byte)InformDataEventParameterCode.InformCode];
                ErrorCode returnCode = (ErrorCode)parameters[(byte)InformDataEventParameterCode.ReturnCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)InformDataEventParameterCode.Parameters];
                if (informTable.ContainsKey(informCode))
                {
                    return informTable[informCode].Handle(informCode, returnCode, resolvedParameters);
                }
                else
                {
                    LibraryLog.ErrorFormat("Answer InformData Event Not Exist Inform Code: {0}", informCode);
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
