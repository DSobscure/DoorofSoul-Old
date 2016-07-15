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
        private Dictionary<int, ServerPlayer> activatedPlayers;

        public PlayerFactory()
        {
            connectedPlayers = new Dictionary<Guid, ServerPlayer>();
            onlinedPlayers = new Dictionary<int, ServerPlayer>();
            activatedPlayers = new Dictionary<int, ServerPlayer>();
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
            PlayerOffline(player);
        }

        public bool PlayerOnline(ServerPlayer player)
        {
            if (player.IsOnline)
            {
                return false;
            }
            else if(player.IsActivated)
            {
                player.IsOnline = true;
                if(!onlinedPlayers.ContainsKey(player.PlayerID))
                {
                    onlinedPlayers.Add(player.PlayerID, player);
                }
                Application.Log.InfoFormat("PlayerID: {0} Return to World", player.PlayerID);
                return true;
            }
            else
            {
                player.IsOnline = true;
                if (!onlinedPlayers.ContainsKey(player.PlayerID))
                {
                    onlinedPlayers.Add(player.PlayerID, player);
                }
                Application.Log.InfoFormat("PlayerID: {0} Online", player.PlayerID);
                return PlayerActive(player);
            }
        }
        public void PlayerOffline(ServerPlayer player)
        {
            if (player.IsOnline)
            {
                player.IsOnline = false;
                if(onlinedPlayers.ContainsKey(player.PlayerID))
                {
                    onlinedPlayers.Remove(player.PlayerID);
                }
                Application.Log.InfoFormat("PlayerID: {0} Offline", player.PlayerID);
            }
        }
        public bool PlayerActive(ServerPlayer player)
        {
            if(player.IsActivated)
            {
                return false;
            }
            else
            {
                player.IsActivated = true;
                if(!activatedPlayers.ContainsKey(player.PlayerID))
                {
                    activatedPlayers.Add(player.PlayerID, player);
                }
                return false;
            }
        }
        public void PlayerDeactivate(ServerPlayer player)
        {
            if (player.IsActivated)
            {
                player.IsActivated = false;
                if(activatedPlayers.ContainsKey(player.PlayerID))
                {
                    activatedPlayers.Remove(player.PlayerID);
                }
                Application.Log.InfoFormat("PlayerID: {0} Deactivate", player.PlayerID);
                DataBase.Instance.RepositoryManager.PlayerRepository.Save(player);
            }
        }
    }
}
