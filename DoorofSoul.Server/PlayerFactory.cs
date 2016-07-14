using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoorofSoul.Library.General;

namespace DoorofSoul.Server
{
    public class PlayerFactory
    {
        private Dictionary<Guid, ServerPlayer> connectedPlayers;
        private Dictionary<int, ServerPlayer> onlinedPlayers;
        private Dictionary<int, ServerPlayer> activePlayers;

        public PlayerFactory()
        {
            connectedPlayers = new Dictionary<Guid, ServerPlayer>();
            onlinedPlayers = new Dictionary<int, ServerPlayer>();
            activePlayers = new Dictionary<int, ServerPlayer>();
        }

        public bool PlayerConnect(ServerPlayer player)
        {
            if(connectedPlayers.ContainsKey(player.Guid))
            {
                return false;
            }
            else
            {
                connectedPlayers.Add(player.Guid, player);
                Application.Log.InfoFormat("Player Guid: {0} Connect from {1}", player.Guid, player.RemoteIPAddress);
                return true;
            }
        }
        public void PlayerDisconnect(ServerPlayer player)
        {
            if(connectedPlayers.ContainsKey(player.Guid))
            {
                connectedPlayers.Remove(player.Guid);
                Application.Log.InfoFormat("Player Guid: {0} Disconnect from {1}", player.Guid, player.RemoteIPAddress);
            }
        }
    }
}
