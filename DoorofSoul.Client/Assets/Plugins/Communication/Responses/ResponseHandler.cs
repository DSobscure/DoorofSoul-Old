using System;
using System.Collections.Generic;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using ExitGames.Client.Photon;

namespace DoorofSoul.Client.Communication.Responses
{
    public abstract class ResponseHandler
    {
        protected PhotonService photonService;
        protected ResponseHandler(PhotonService photonService)
        {
            this.photonService = photonService;
        }

        public virtual bool Handle(OperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (CheckError(parameters, returnCode, debugMessage))
            {
                return true;
            }
            else
            {
                photonService.DebugReturn(DebugLevel.ERROR, string.Format("Response Parameter Error DebugMessage: {0}", debugMessage));
                return false;
            }
        }
        public abstract bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage);
    }
}
