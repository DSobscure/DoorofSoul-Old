using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationParameters;
using System.Collections.Generic;

namespace DoorofSoul.Client.Communication.Managers.OperationManagers
{
    public class FetchDataOperationManager
    {
        public static void SendOperation(FetchDataCode fetchDataCode, Dictionary<byte, object> parameters)
        {
            var parameter = new Dictionary<byte, object>
            {
                {(byte)FetchDataOperationParameterCode.FetchDataCode, fetchDataCode },
                {(byte)FetchDataOperationParameterCode.Parameters, parameters }
            };
            Global.PhotonService.SendOperation(OperationCode.FetchData, parameter);
        }

        public void FetchSystemVersion()
        {
            SendOperation(FetchDataCode.SystemVersion, new Dictionary<byte, object>());
        }
    }
}
