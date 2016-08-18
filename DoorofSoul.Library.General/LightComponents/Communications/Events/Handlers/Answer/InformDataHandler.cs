using DoorofSoul.Protocol.Communication.InformDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Answer
{
    public abstract class InformDataHandler
    {
        protected ThroneComponents.Answer answer;
        protected int correctParameterCount;

        protected InformDataHandler(ThroneComponents.Answer answer, int correctParameterCount)
        {
            this.answer = answer;
            this.correctParameterCount = correctParameterCount;
        }

        public virtual bool Handle(AnswerInformDataCode informCode, Dictionary<byte, object> parameter)
        {
            string debugMessage;
            if (CheckParameter(parameter, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryInstance.ErrorFormat("Answer InformData Parameter Error On {0} AnswerID: {1} Debug Message: {2}", informCode, answer.AnswerID, debugMessage);
                return false;
            }
        }
        internal virtual bool CheckParameter(Dictionary<byte, object> parameters, out string debugMessage)
        {
            if (parameters.Count != correctParameterCount)
            {
                debugMessage = string.Format("Parameter Count: {0} Should be {1}", parameters.Count, correctParameterCount);
                return false;
            }
            else
            {
                debugMessage = "";
                return true;
            }
        }
    }
}
