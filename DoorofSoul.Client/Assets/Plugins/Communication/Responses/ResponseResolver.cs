using System;
using System.Collections.Generic;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Client.Communication.Responses.Handlers;
using ExitGames.Client.Photon;

namespace DoorofSoul.Client.Communication.Responses
{
    public class ResponseResolver
    {
        protected Dictionary<OperationCode, ResponseHandler> responseTable;
        protected readonly PhotonService photonService;

        public ResponseResolver(PhotonService photonService)
        {
            this.photonService = photonService;
            responseTable = new Dictionary<OperationCode, ResponseHandler>
            {
                { OperationCode.PlayerOperation, new PlayerOperationResponseResolver(photonService) },
                { OperationCode.WorldOperation, new WorldOperationResponseResolver(photonService) },
            };
        }

        public void Operate(OperationResponse operationResponse)
        {
            OperationCode operationCode = (OperationCode)operationResponse.OperationCode;
            if (responseTable.ContainsKey(operationCode))
            {
                if (!responseTable[operationCode].Handle(operationCode, (ErrorCode)operationResponse.ReturnCode, operationResponse.DebugMessage, operationResponse.Parameters))
                {
                    photonService.DebugReturn(DebugLevel.ERROR, string.Format("Response Error OperationCode: {0}", operationCode));
                }
            }
            else
            {
                photonService.DebugReturn(DebugLevel.ERROR, string.Format("Unknow Response OperationCode: {0}", operationCode));
            }
        }
    }
}
