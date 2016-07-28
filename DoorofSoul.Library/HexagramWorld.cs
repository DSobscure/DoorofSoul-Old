using System;
using DoorofSoul.Library.General;
using DoorofSoul.Database.Library;
using System.Collections.Generic;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Language;

namespace DoorofSoul.Library
{
    public class HexagramWorld : World
    {
        public override SupportLauguages UsingLanguage
        {
            get
            {
                return SupportLauguages.Chinese_Traditional;
            }
        }

        protected HashSet<Player> Players
        {
            get
            {
                HashSet<Player> players = new HashSet<Player>();
                foreach (Container container in Containers)
                {
                    foreach (Soul soul in container.Souls)
                    {
                        players.Add(soul.Answer.Player);
                    }
                }
                return players;
            }
        }
        public HexagramWorld(WorldData world) : base(world.worldID, world.worldName)
        {
        }

        public override void SendEvent(WorldEventCode eventCode, Dictionary<byte, object> parameters)
        {
            foreach (Player player in Players)
            {
                player.SendWorldEvent(WorldID, eventCode, parameters);
            }
        }

        public override void SendOperation(WorldOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            foreach (Player player in Players)
            {
                player.SendWorldOperation(WorldID, operationCode, parameters);
            }
        }

        public override void SendResponse(WorldOperationCode operationCode, ErrorCode returnCode, string degugMessage, Dictionary<byte, object> parameters)
        {
            foreach (Player player in Players)
            {
                player.SendWorldResponse(WorldID, operationCode, returnCode, degugMessage, parameters);
            }
        }

        public override void ErrorInform(string title, string message)
        {
            throw new NotImplementedException();
        }

        public override void FetchScene(int sceneID, out Scene scene)
        {
            scene = FindScene(sceneID);
        }

        public override void FetchSceneResponse(int sceneID, string sceneName)
        {
            throw new NotImplementedException();
        }
    }
}
