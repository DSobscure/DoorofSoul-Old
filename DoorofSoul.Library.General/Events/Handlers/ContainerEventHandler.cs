using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers
{
    internal abstract class ContainerEventHandler
    {
        protected General.Container container;

        protected ContainerEventHandler(General.Container container)
        {
            this.container = container;
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
                LibraryLog.ErrorFormat("Container Event Parameter Error On {0} ContainerID: {1} Debug Message: {2}", eventCode, container.ContainerID, debugMessage);
                return false;
            }
        }
        internal abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
