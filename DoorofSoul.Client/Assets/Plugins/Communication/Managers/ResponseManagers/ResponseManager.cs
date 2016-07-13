using System.Collections.Generic;
using DoorofSoul.Protocol.Communication;
using ExitGames.Client.Photon;
using DoorofSoul.Client.Communication.Handlers;

namespace DoorofSoul.Client.Communication.Managers.ResponseManagers
{
    public class ResponseManager
    {
        protected readonly Dictionary<OperationCode, ResponseHandler> responseTable;

        public ResponseManager()
        {
            responseTable = new Dictionary<OperationCode, ResponseHandler>
            {

            };
        }

        public void Operate(OperationResponse operationResponse)
        {
            OperationCode operationCode = (OperationCode)operationResponse.OperationCode;
            if(responseTable.ContainsKey(operationCode))
            {
                responseTable[operationCode].Handle(operationResponse);
            }
            else
            {
                Global.SystemManagers.DebugInformManager.DebugInform(string.Format("Unknow Response: {0}", operationCode));
            }
        }
    }
}

