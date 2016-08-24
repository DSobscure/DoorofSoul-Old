using DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Scene.FetchData;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataParameters;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Scene
{
    public class FetchDataResolver : SceneOperationHandler
    {
        private readonly Dictionary<SceneFetchDataCode, FetchDataHandler> fetchTable;

        internal FetchDataResolver(NatureComponents.Scene scene) : base(scene, 2)
        {
            fetchTable = new Dictionary<SceneFetchDataCode, FetchDataHandler>
            {
                { SceneFetchDataCode.Entities, new FetchEntitiesHandler(scene) },
                { SceneFetchDataCode.ItemEntities, new FetchItemEntitiesHandler(scene) },
            };
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
        public void FetchItemEntities()
        {
            SendOperation(SceneFetchDataCode.ItemEntities, new Dictionary<byte, object>());
        }
    }
}
