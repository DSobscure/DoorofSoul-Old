using DoorofSoul.Library.General.Operations.Handlers;
using DoorofSoul.Library.General.Operations.Handlers.Scene;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Managers
{
    public class SceneOperationManager
    {
        protected readonly Dictionary<SceneOperationCode, SceneOperationHandler> operationTable;
        protected readonly Scene scene;

        public SceneOperationManager(Scene scene)
        {
            this.scene = scene;
            operationTable = new Dictionary<SceneOperationCode, SceneOperationHandler>
            {
                { SceneOperationCode.ContainerOperation, new ContainerOperationResolver(scene) },
                { SceneOperationCode.EntityOperation, new EntityOperationResolver(scene) },
                { SceneOperationCode.FetchData, new FetchDataHandler(scene) },
            };
        }

        public void Operate(SceneOperationCode operationCode, Dictionary<byte, object> parameters)
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
    }
}
