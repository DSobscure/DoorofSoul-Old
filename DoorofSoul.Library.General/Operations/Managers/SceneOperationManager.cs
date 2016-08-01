using DoorofSoul.Library.General.Operations.Handlers;
using DoorofSoul.Library.General.Operations.Handlers.Scene;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataParameters;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;
using DoorofSoul.Protocol.Communication.OperationParameters.World;

namespace DoorofSoul.Library.General.Operations.Managers
{
    public class SceneOperationManager
    {
        private readonly Dictionary<SceneOperationCode, SceneOperationHandler> operationTable;
        protected readonly Scene scene;

        internal SceneOperationManager(Scene scene)
        {
            this.scene = scene;
            operationTable = new Dictionary<SceneOperationCode, SceneOperationHandler>
            {
                { SceneOperationCode.ContainerOperation, new ContainerOperationResolver(scene) },
                { SceneOperationCode.EntityOperation, new EntityOperationResolver(scene) },
                { SceneOperationCode.FetchData, new FetchDataResolver(scene) },
            };
        }

        internal void Operate(SceneOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, parameters))
                {
                    LibraryLog.ErrorFormat("Scene Operation Error: {0} from SceneID: {1}", operationCode, scene.SceneID);
                }
            }
            else
            {
                LibraryLog.ErrorFormat("Unknow Scene Operation:{0} from SceneID: {1}", operationCode, scene.SceneID);
            }
        }

        internal void SendOperation(SceneOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> operationData = new Dictionary<byte, object>
            {
                { (byte)SceneOperationParameterCode.SceneID, scene.SceneID },
                { (byte)SceneOperationParameterCode.OperationCode, (byte)operationCode },
                { (byte)SceneOperationParameterCode.Parameters, parameters }
            };
            scene.World.WorldOperationManager.SendOperation(WorldOperationCode.SceneOperation, operationData);
        }

        public void FetchEntities()
        {
            Dictionary<byte, object> fetchDataParameters = new Dictionary<byte, object>
            {
                { (byte)FetchDataParameterCode.FetchDataCode, (byte)SceneFetchDataCode.Entities },
                { (byte)FetchDataParameterCode.Parameters, new Dictionary<byte, object>() }
            };
            SendOperation(SceneOperationCode.FetchData, fetchDataParameters);
        }
    }
}
