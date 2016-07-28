using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataParameters;
using DoorofSoul.Protocol.Communication.FetchDataParameters.World;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Language;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Client.Library.General
{
    public class ClientWorld : World
    {
        public ClientWorld(int worldID, string worldName) : base(worldID, worldName)
        {
        }

        public override SupportLauguages UsingLanguage
        {
            get
            {
                return Global.Global.Player.UsingLanguage;
            }
        }

        public override void ErrorInform(string title, string message)
        {
            Global.Global.Player.ErrorInform(title, message);
        }

        public override void FetchScene(int sceneID, out Scene scene)
        {
            scene = null;
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

        public override void FetchSceneResponse(int sceneID, string sceneName)
        {
            Global.Global.Horizon.LoadScene(new Scene(sceneID, sceneName, WorldID));
        }

        public override void SendEvent(WorldEventCode eventCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendOperation(WorldOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            Global.Global.Player.SendWorldOperation(WorldID, operationCode, parameters);
        }

        public override void SendResponse(WorldOperationCode operationCode, ErrorCode returnCode, string degugMessage, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
