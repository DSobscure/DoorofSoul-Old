using DoorofSoul.Library.General.Operations.Handlers.Scene.FetchData;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataParameters;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.World;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers.Scene
{
    public class FetchDataResolver : SceneOperationHandler
    {
        private readonly Dictionary<SceneFetchDataCode, FetchDataHandler> fetchTable;

        internal FetchDataResolver(General.Scene scene) : base(scene)
        {
            fetchTable = new Dictionary<SceneFetchDataCode, FetchDataHandler>
            {
                { SceneFetchDataCode.Entities, new FetchEntitiesHandler(scene) },
            };
        }

        internal override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 2)
            {
                debugMessage = string.Format("Scene Fetch Data Operation Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        internal override bool Handle(SceneOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                string debugMessage;
                SceneFetchDataCode fetchCode = (SceneFetchDataCode)parameters[(byte)FetchDataParameterCode.FetchDataCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)FetchDataParameterCode.Parameters];
                if (fetchTable.ContainsKey(fetchCode))
                {
                    return fetchTable[fetchCode].Handle(fetchCode, resolvedParameters);
                }
                else
                {
                    debugMessage = string.Format("Scene Fetch Operation Not Exist Fetch Code: {0}", fetchCode);
                    SendError(operationCode, ErrorCode.InvalidOperation, debugMessage);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        internal void SendOperation(SceneFetchDataCode fetchCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> fetchDataParameters = new Dictionary<byte, object>
            {
                { (byte)FetchDataParameterCode.FetchDataCode, (byte)fetchCode },
                { (byte)FetchDataParameterCode.Parameters, parameters }
            };
            scene.SceneOperationManager.SendOperation(SceneOperationCode.FetchData, fetchDataParameters);
        }

        public void FetchEntities()
        {
            SendOperation(SceneFetchDataCode.Entities, new Dictionary<byte, object>());
        }
    }
}
