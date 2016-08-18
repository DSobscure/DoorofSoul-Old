using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers
{
    public abstract class ContainerEventHandler
    {
        protected NatureComponents.Container container;
        protected int correctParameterCount;

        protected ContainerEventHandler(NatureComponents.Container container, int correctParameterCount)
        {
            this.container = container;
            this.correctParameterCount = correctParameterCount;
        }

        internal virtual bool Handle(ContainerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryInstance.ErrorFormat("Container Event Parameter Error On {0} ContainerID: {1} Debug Message: {2}", eventCode, container.ContainerID, debugMessage);
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
