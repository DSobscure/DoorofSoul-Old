using System;
using System.Collections.Generic;
using DoorofSoul.Protocol.Communication.OperationCodes;

namespace DoorofSoul.Server.Operations
{
    public abstract class OperationHandler
    {
        protected Peer peer;

        protected OperationHandler(Peer peer)
        {
            this.peer = peer;
        }

        public virtual bool Handle(OperationCode operationCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                Application.Log.ErrorFormat("Operation Parameter Error DebugMessage: {0}", debugMessage);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
    }
}
