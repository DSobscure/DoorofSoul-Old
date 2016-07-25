using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Answer
{
    public abstract class InformDataHandler
    {
        protected General.Answer answer;

        protected InformDataHandler(General.Answer answer)
        {
            this.answer = answer;
        }

        public virtual bool Handle(AnswerInformDataCode informCode, ErrorCode returnCode, Dictionary<byte, object> parameter)
        {
            string debugMessage;
            if (CheckParameter(parameter, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryLog.ErrorFormat("Answer InformData Parameter Error On {0} AnswerID: {1} Debug Message: {2}", informCode, answer.AnswerID, debugMessage);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
