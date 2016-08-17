using System;
using System.Collections.Generic;
using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Language;
using DoorofSoul.Client.Global;

namespace DoorofSoul.Client.Library.General
{
    public class ClientWorldCommunicationInterface : WorldCommunicationInterface
    {
        protected Player player;
        protected SystemManager systemManager;
        protected Horizon horizon;

        public override SupportLauguages UsingLanguage
        {
            get
            {
                return player.UsingLanguage;
            }
        }

        public ClientWorldCommunicationInterface(Player player, SystemManager systemManager, Horizon horizon)
        {
            this.player = player;
            this.systemManager = systemManager;
            this.horizon = horizon;
        }

        public override void ErrorInform(string title, string message)
        {
            systemManager.ErrorInform(title, message);
        }

        public override void LoadScene(int sceneID, string sceneName)
        {
            horizon.LoadScene(new Scene(sceneID, sceneName, world.WorldID));
        }

        public override void SendEvent(WorldEventCode eventCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendOperation(WorldOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            player.PlayerCommunicationInterface.SendWorldOperation(world.WorldID, operationCode, parameters);
        }

        public override void SendResponse(WorldOperationCode operationCode, ErrorCode returnCode, string degugMessage, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendSceneEvent(Scene scene, WorldEventCode eventCode, Dictionary<byte, object> parameters)
        {
            
        }
    }
}
