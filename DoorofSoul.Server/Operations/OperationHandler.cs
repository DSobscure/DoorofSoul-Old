using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.ResponseParameters;

namespace DoorofSoul.Server.Operations
{
    public abstract class OperationHandler
    {
        protected Peer peer;

        protected OperationHandler(Peer peer)
        {
            this.peer = peer;
        }

        public virtual bool Handle(OperationRequest operationRequest)
        {
            string debugMessage;
            if(CheckParameter(operationRequest.Parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                SendError(operationRequest.OperationCode, ErrorCode.ParameterError, debugMessage, LauguageDictionarySelector.Instance[peer.UsingLanguage]["Operation Parameter Error"]);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
        public void SendError(byte operationCode, ErrorCode errorCode, string debugMessage, string errorMessage)
        {
            OperationResponse response = new OperationResponse(operationCode)
            {
                ReturnCode = (short)errorCode,
                DebugMessage = debugMessage,
                Parameters = new Dictionary<byte, object> { { (byte)OperationErrorResponseParameterCode.ErrorMessage, errorMessage } }
            };
            Application.Log.ErrorFormat("Error On Operation: {0}, ErrorCode:{1}, Debug Message: {2}", (OperationCode)operationCode, errorCode, debugMessage);
            peer.SendOperationResponse(response, new SendParameters());
        }
        public void SendResponse(byte operationCode, Dictionary<byte, object> parameter)
        {
            OperationResponse response = new OperationResponse(operationCode, parameter)
            {
                ReturnCode = (short)ErrorCode.NoError
            };
            peer.SendOperationResponse(response, new SendParameters());
        }
    }
}
