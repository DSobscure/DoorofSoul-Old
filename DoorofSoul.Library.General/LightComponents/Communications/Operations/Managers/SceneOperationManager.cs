using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers;
using DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Scene;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.World;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Managers
{
    public class SceneOperationManager
    {
        private readonly Dictionary<SceneOperationCode, SceneOperationHandler> operationTable;
        protected readonly Scene scene;
        public FetchDataResolver FetchDataResolver { get; protected set; }

        internal SceneOperationManager(Scene scene)
        {
            this.scene = scene;
            FetchDataResolver = new FetchDataResolver(scene);
            operationTable = new Dictionary<SceneOperationCode, SceneOperationHandler>
            {
                { SceneOperationCode.ContainerOperation, new ContainerOperationResolver(scene) },
                { SceneOperationCode.EntityOperation, new EntityOperationResolver(scene) },
                { SceneOperationCode.FetchData, FetchDataResolver },
            };
        }

        internal void Operate(SceneOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, parameters))
                {
                    LibraryInstance.ErrorFormat("Scene Operation Error: {0} from SceneID: {1}", operationCode, scene.SceneID);
                }
            }
            else
            {
                LibraryInstance.ErrorFormat("Unknow Scene Operation:{0} from SceneID: {1}", operationCode, scene.SceneID);
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
    }
}
