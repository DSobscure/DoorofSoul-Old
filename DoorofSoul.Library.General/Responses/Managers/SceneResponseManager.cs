using DoorofSoul.Library.General.Responses.Handlers;
using DoorofSoul.Library.General.Responses.Handlers.Scene;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;
using DoorofSoul.Protocol.Communication.ResponseParameters.World;

namespace DoorofSoul.Library.General.Responses.Managers
{
    internal class SceneResponseManager
    {
        protected readonly Dictionary<SceneOperationCode, SceneResponseHandler> operationTable;
        protected readonly Scene scene;

        internal SceneResponseManager(Scene scene)
        {
            this.scene = scene;
            operationTable = new Dictionary<SceneOperationCode, SceneResponseHandler>
            {
                { SceneOperationCode.ContainerOperation, new ContainerOperationResponseResolver(scene) },
                { SceneOperationCode.EntityOperation, new EntityOperationResponseResolver(scene) },
                { SceneOperationCode.FetchData, new FetchDataResponseResolver(scene) },
            };
        }

        internal void Operate(SceneOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
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
        internal void SendResponse(SceneOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> responseData = new Dictionary<byte, object>
            {
                { (byte)SceneResponseParameterCode.SceneID, scene.SceneID },
                { (byte)SceneResponseParameterCode.OperationCode, (byte)operationCode },
                { (byte)SceneResponseParameterCode.ReturnCode, (short)errorCode },
                { (byte)SceneResponseParameterCode.DebugMessage, debugMessage },
                { (byte)SceneResponseParameterCode.Parameters, parameters }
            };
            scene.World.WorldResponseManager.SendResponse(WorldOperationCode.SceneOperation, ErrorCode.NoError, null, responseData);
        }
    }
}
