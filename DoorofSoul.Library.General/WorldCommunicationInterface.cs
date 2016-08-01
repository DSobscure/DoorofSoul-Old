using System.Collections.Generic;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Language;

namespace DoorofSoul.Library.General
{
    public abstract class WorldCommunicationInterface
    {
        protected World world;
        public void BindWorld(World world)
        {
            this.world = world;
        }
        public abstract SupportLauguages UsingLanguage { get; }
        public abstract void SendEvent(WorldEventCode eventCode, Dictionary<byte, object> parameters);
        public abstract void SendSceneEvent(Scene scene, WorldEventCode eventCode, Dictionary<byte, object> parameters);
        public abstract void SendOperation(WorldOperationCode operationCode, Dictionary<byte, object> parameters);
        public abstract void SendResponse(WorldOperationCode operationCode, ErrorCode returnCode, string degugMessage, Dictionary<byte, object> parameters);
        public abstract void ErrorInform(string title, string message);

        public abstract void LoadScene(int sceneID, string sceneName);
    }
}
