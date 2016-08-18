using DoorofSoul.Client.Communication;
using DoorofSoul.Client.Global;
using DoorofSoul.Library.General;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.ThroneComponents;
using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Client.Library.General
{
    public class ClientPlayerCommunicationInterface : PlayerCommunicationInterface
    {
        protected SystemManager systemManager;
        protected PhotonService photonService;
        protected Horizon horizon;

        public ClientPlayerCommunicationInterface(SystemManager systemManager, PhotonService photonService, Horizon horizon)
        {
            this.systemManager = systemManager;
            this.photonService = photonService;
            this.horizon = horizon;
        }

        public override bool ActiveSoul(Answer answer, int soulID)
        {
            throw new NotImplementedException();
        }

        public override bool CreateSoul(Answer answer, string soulName, SoulKernelTypeCode mainSoulType)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteSoul(Answer answer, int soulID)
        {
            throw new NotImplementedException();
        }

        public override void ErrorInform(string title, string message)
        {
            systemManager.ErrorInform(title, message);
        }

        public override void GetSystemVersion(out string serverVersion, out string clientVersion)
        {
            throw new NotImplementedException();
        }

        public override List<World> ListWorlds()
        {
            throw new NotImplementedException();
        }

        public override void LoadWorld(int worldID, string worldName)
        {
            ClientWorldCommunicationInterface communicationInterface = new ClientWorldCommunicationInterface(player, systemManager, horizon);
            World world = new World(communicationInterface, worldID, worldName);
            communicationInterface.BindWorld(world);
            horizon.LoadWorld(world);
        }

        public override bool Login(string account, string password, out string debugMessage, out ErrorCode errorCode)
        {
            throw new NotImplementedException();
        }

        public override void Logout()
        {
            throw new NotImplementedException();
        }

        public override void SendEvent(PlayerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendOperation(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            photonService.SendPlayerOperation(player.PlayerID, operationCode, parameters);
        }

        public override void SendResponse(PlayerOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendWorldEvent(int worldID, WorldEventCode eventCode, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void SendWorldOperation(int worldID, WorldOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            photonService.SendWorldOperation(worldID, operationCode, parameters);
        }

        public override void SendWorldResponse(int worldID, WorldOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void UpdateSystemVersion(string serverVersion, string clientVersion)
        {
            systemManager.CurrentServerVersion = serverVersion;
            systemManager.CurrentClientVersion = clientVersion;
        }

        public override Scene FindScene(int sceneID)
        {
            throw new NotImplementedException();
        }

        public override void LoadScene(int sceneID, string sceneName, int worldID)
        {
            horizon.LoadScene(new Scene(sceneID, sceneName, worldID));
        }
    }
}
