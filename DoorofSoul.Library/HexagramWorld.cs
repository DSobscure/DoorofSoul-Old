using System;
using DoorofSoul.Library.General;
using DoorofSoul.Database.Library;
using System.Collections.Generic;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.OperationCodes;

namespace DoorofSoul.Library
{
    public class HexagramWorld : World
    {
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
        public HexagramWorld(DatabaseWorld world) : base(world.WorldID, world.WorldName)
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
    }
}
