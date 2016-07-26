﻿using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers.Container
{
    public abstract class FetchDataResponseHandler
    {
        protected General.Container container;

        protected FetchDataResponseHandler(General.Container container)
        {
            this.container = container;
        }

        public virtual bool Handle(ContainerFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameter)
        {
            string debugMessage;
            if (CheckParameter(parameter, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryLog.ErrorFormat("Container FetchData Parameter Error On {0} ContainerID: {1} Debug Message: {2}", fetchCode, container.ContainerID, debugMessage);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
