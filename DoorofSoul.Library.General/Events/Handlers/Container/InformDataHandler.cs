using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Container
{
    public abstract class InformDataHandler
    {
        protected General.Container container;

        protected InformDataHandler(General.Container container)
        {
            this.container = container;
        }

        public virtual bool Handle(ContainerInformDataCode informCode, ErrorCode returnCode, Dictionary<byte, object> parameter)
        {
            string debugMessage;
            if (CheckParameter(parameter, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryInstance.ErrorFormat("Container InformData Parameter Error On {0} ContainerID: {1} Debug Message: {2}", informCode, container.ContainerID, debugMessage);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
