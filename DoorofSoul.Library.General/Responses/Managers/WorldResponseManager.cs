using DoorofSoul.Library.General.Responses.Handlers;
using DoorofSoul.Library.General.Responses.Handlers.World;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Managers
{
    public class WorldResponseManager
    {
        protected readonly Dictionary<WorldOperationCode, WorldResponseHandler> operationTable;
        protected readonly World world;

        public WorldResponseManager(World world)
        {
            this.world = world;
            operationTable = new Dictionary<WorldOperationCode, WorldResponseHandler>
            {
                { WorldOperationCode.SceneOperation, new SceneOperationResponseResolver(world) },
                { WorldOperationCode.FetchData, new FetchDataResponseResolver(world) }
            };
        }

        public void Operate(WorldOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, returnCode, debugMessage, parameters))
                {
                    LibraryInstance.ErrorFormat("World Response Error: {0} from AnswerID: {1}", operationCode, world.WorldID);
                }
            }
            else
            {
                LibraryInstance.ErrorFormat("Unknow World Response:{0} from AnswerID: {1}", operationCode, world.WorldID);
            }
        }

        public void SendResponse(WorldOperationCode operationCode, ErrorCode returnCode, string degugMessage, Dictionary<byte, object> parameters)
        {
            world.WorldCommunicationInterface.SendResponse(operationCode, returnCode, degugMessage, parameters);
        }
    }
}
