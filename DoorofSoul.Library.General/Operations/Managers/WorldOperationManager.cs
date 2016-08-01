using DoorofSoul.Library.General.Operations.Handlers;
using DoorofSoul.Library.General.Operations.Handlers.World;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataParameters;
using DoorofSoul.Protocol.Communication.FetchDataParameters.World;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Managers
{
    public class WorldOperationManager
    {
        protected readonly Dictionary<WorldOperationCode, WorldOperationHandler> operationTable;
        protected readonly World world;

        public WorldOperationManager(World world)
        {
            this.world = world;
            operationTable = new Dictionary<WorldOperationCode, WorldOperationHandler>
            {
                { WorldOperationCode.SceneOperation, new SceneOperationResolver(world) },
                { WorldOperationCode.FetchData, new FetchDataResolver(world) },
            };
        }

        public void Operate(WorldOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, parameters))
                {
                    LibraryLog.ErrorFormat("World Operation Error: {0} from WorldID: {1}", operationCode, world.WorldID);
                }
            }
            else
            {
                LibraryLog.ErrorFormat("Unknow World Operation:{0} from WorldID: {1}", operationCode, world.WorldID);
            }
        }

        public void SendOperation(WorldOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            world.WorldCommunicationInterface.SendOperation(operationCode, parameters);
        }

        public void FetchScene(int sceneID)
        {
            Dictionary<byte, object> fetchDataParameters = new Dictionary<byte, object>
            {
                { (byte)FetchSceneParameterCode.SceneID, sceneID }
            };
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)FetchDataParameterCode.FetchDataCode, WorldFetchDataCode.Scene },
                { (byte)FetchDataParameterCode.Parameters, fetchDataParameters }
            };
            SendOperation(WorldOperationCode.FetchData, parameters);
        }
    }
}
