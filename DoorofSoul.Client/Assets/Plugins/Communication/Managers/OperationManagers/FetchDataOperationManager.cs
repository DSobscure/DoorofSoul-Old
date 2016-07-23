using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataParameters;
using DoorofSoul.Protocol.Communication.OperationParameters;
using System.Collections.Generic;

namespace DoorofSoul.Client.Communication.Managers.OperationManagers
{
    public class FetchDataOperationManager
    {
        public static void SendFetchOperation(FetchDataCode fetchDataCode, Dictionary<byte, object> parameters)
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
            SendFetchOperation(FetchDataCode.SystemVersion, new Dictionary<byte, object>());
        }
        public void FetchAnswer()
        {
            SendFetchOperation(FetchDataCode.Answer, new Dictionary<byte, object>());
        }
        public void FetchSouls()
        {
            SendFetchOperation(FetchDataCode.Souls, new Dictionary<byte, object>());
        }
        public void FetchContainers()
        {
            SendFetchOperation(FetchDataCode.Containers, new Dictionary<byte, object>());
        }
        public void FetchSoulContainerConnections()
        {
            SendFetchOperation(FetchDataCode.SoulContainerConnections, new Dictionary<byte, object>());
        }
        public void FetchScene(int sceneID)
        {
            var parameter = new Dictionary<byte, object>
            {
                {(byte)FetchSceneParameterCode.SceneID, sceneID }
            };
            SendFetchOperation(FetchDataCode.Scene, parameter);
        }
        public void FetchSceneEntitiesInformation(int sceneID)
        {
            var parameter = new Dictionary<byte, object>
            {
                {(byte)FetchSceneEntitiesInformationParameterCode.SceneID, sceneID }
            };
            SendFetchOperation(FetchDataCode.SceneEntitiesInformation, parameter);
        }
    }
}
