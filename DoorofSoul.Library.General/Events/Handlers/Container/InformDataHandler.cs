using DoorofSoul.Protocol.Communication.InformDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Container
{
    internal abstract class InformDataHandler
    {
        protected General.Container container;
        protected int correctParameterCount;

        protected InformDataHandler(General.Container container, int correctParameterCount)
        {
            this.container = container;
            this.correctParameterCount = correctParameterCount;
        }

        internal virtual bool Handle(ContainerInformDataCode informCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryInstance.ErrorFormat("Container InformData Parameter Error On {0} ContainerID: {1} Debug Message: {2}", informCode, container.ContainerID, debugMessage);
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
