using DoorofSoul.Library.General.Responses.Handlers;
using DoorofSoul.Library.General.Responses.Handlers.Scene;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Managers
{
    public class SceneResponseManager
    {
        protected readonly Dictionary<SceneOperationCode, SceneResponseHandler> operationTable;
        protected readonly Scene scene;

        public SceneResponseManager(Scene scene)
        {
            this.scene = scene;
            operationTable = new Dictionary<SceneOperationCode, SceneResponseHandler>
            {
                { SceneOperationCode.ContainerOperation, new ContainerOperationResponseResolver(scene) },
                { SceneOperationCode.EntityOperation, new EntityOperationResponseResolver(scene) },
                { SceneOperationCode.FetchData, new FetchDataResponseResolver(scene) },
            };
        }

        public void Operate(SceneOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, returnCode, debugMessage, parameters))
                {
                    LibraryLog.ErrorFormat("Scene Response Error: {0} from AnswerID: {1}", operationCode, scene.SceneID);
                }
            }
            else
            {
                LibraryLog.ErrorFormat("Unknow Scene Response:{0} from AnswerID: {1}", operationCode, scene.SceneID);
            }
        }
    }
}
