using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General
{
    public abstract class PlayerCommunicationInterface
    {
        protected Player player;
        public void BindPlayer(Player player)
        {
            this.player = player;
        }
        public abstract void SendEvent(PlayerEventCode eventCode, Dictionary<byte, object> parameters);
        public abstract void SendOperation(PlayerOperationCode operationCode, Dictionary<byte, object> parameters);
        public abstract void SendResponse(PlayerOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters);
        public abstract void SendWorldEvent(int worldID, WorldEventCode eventCode, Dictionary<byte, object> parameters);
        public abstract void SendWorldOperation(int worldID, WorldOperationCode operationCode, Dictionary<byte, object> parameters);
        public abstract void SendWorldResponse(int worldID, WorldOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters);
        public abstract void ErrorInform(string title, string message);

        public abstract void GetSystemVersion(out string serverVersion, out string clientVersion);
        public abstract void UpdateSystemVersion(string serverVersion, string clientVersion);
        public abstract List<World> ListWorlds();
        public abstract Scene FindScene(int sceneID);
        public abstract void LoadWorld(int worldID, string worldName);
        public abstract void LoadScene(int sceneID, string sceneName, int worldID);
        public abstract void Logout();
        public abstract bool Login(string account, string password, out string debugMessage, out ErrorCode errorCode);
        public abstract bool DeleteSoul(Answer answer, int soulID);
        public abstract bool CreateSoul(Answer answer, string soulName, SoulKernelTypeCode mainSoulType);
        public abstract bool ActiveSoul(Answer answer, int soulID);
    }
}
