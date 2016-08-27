using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Language;
using System.Collections.Generic;

namespace DoorofSoul.Hexagram
{
    public class HexagramWorldCommunicationInterface : WorldCommunicationInterface
    {
        public override SupportLauguages UsingLanguage
        {
            get
            {
                return SupportLauguages.Chinese_Traditional;
            }
        }

        public override void ErrorInform(string title, string message)
        {
            Hexagram.Log.ErrorFormat("WorldErrorInform WorldID: {0} Title: {1}, Message: {2}", world.WorldID, title, message);
        }

        public override void LoadScene(int sceneID, string sceneName)
        {
            world.LoadScenes(new List<Scene> { new Scene(sceneID, sceneName, world.WorldID) });
        }

        public override void SendEvent(WorldEventCode eventCode, Dictionary<byte, object> parameters)
        {
            foreach(Container container in world.Containers)
            {
                if(!container.IsEmptyContainer)
                {
                    container.FirstSoul.Answer.Player.PlayerCommunicationInterface.SendWorldEvent(world.WorldID, eventCode, parameters);
                }
            }
        }

        public override void SendOperation(WorldOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            foreach (Container container in world.Containers)
            {
                if (!container.IsEmptyContainer)
                {
                    container.FirstSoul.Answer.Player.PlayerCommunicationInterface.SendWorldOperation(world.WorldID, operationCode, parameters);
                }
            }
        }

        public override void SendResponse(WorldOperationCode operationCode, ErrorCode returnCode, string degugMessage, Dictionary<byte, object> parameters)
        {
            foreach (Container container in world.Containers)
            {
                if (!container.IsEmptyContainer)
                {
                    container.FirstSoul.Answer.Player.PlayerCommunicationInterface.SendWorldResponse(world.WorldID, operationCode, returnCode, degugMessage, parameters);
                }
            }
        }

        public override void SendSceneEvent(Scene scene, WorldEventCode eventCode, Dictionary<byte, object> parameters)
        {
            foreach (Container container in scene.Containers)
            {
                if (!container.IsEmptyContainer)
                {
                    container.FirstSoul.Answer.Player.PlayerCommunicationInterface.SendWorldEvent(world.WorldID, eventCode, parameters);
                }
            }
        }
    }
}
