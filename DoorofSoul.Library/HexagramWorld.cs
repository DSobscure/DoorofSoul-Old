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

        public override void SendError(WorldOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            foreach(Player player in Players)
            {
                player.SendWorldError(operationCode, errorCode, debugMessage, parameters);
            }
        }

        public override void SendEvent(WorldEventCode eventCode, Dictionary<byte, object> parameters)
        {
            foreach (Player player in Players)
            {
                player.SendWorldEvent(eventCode, parameters);
            }
        }

        public override void SendResponse(WorldOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            foreach (Player player in Players)
            {
                player.SendWorldResponse(operationCode, parameters);
            }
        }
    }
}
