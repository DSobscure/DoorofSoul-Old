using DoorofSoul.Library.General.Operations.Handlers;
using DoorofSoul.Library.General.Operations.Handlers.World;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataParameters;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Managers
{
    public class WorldOperationManager
    {
        protected readonly Dictionary<WorldOperationCode, WorldOperationHandler> operationTable;
        protected readonly World world;
        public FetchDataResolver FetchDataResolver { get; protected set; }

        public WorldOperationManager(World world)
        {
            this.world = world;
            FetchDataResolver = new FetchDataResolver(world);
            operationTable = new Dictionary<WorldOperationCode, WorldOperationHandler>
            {
                { WorldOperationCode.SceneOperation, new SceneOperationResolver(world) },
                { WorldOperationCode.FetchData, FetchDataResolver },
            };
        }

        public void Operate(WorldOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, parameters))
                {
                    LibraryInstance.ErrorFormat("World Operation Error: {0} from WorldID: {1}", operationCode, world.WorldID);
                }
            }
            else
            {
                LibraryInstance.ErrorFormat("Unknow World Operation:{0} from WorldID: {1}", operationCode, world.WorldID);
            }
        }

        public void SendOperation(WorldOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            world.WorldCommunicationInterface.SendOperation(operationCode, parameters);
        }
    }
}
