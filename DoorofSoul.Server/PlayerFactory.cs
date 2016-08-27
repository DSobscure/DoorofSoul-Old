using DoorofSoul.Database;
using DoorofSoul.Library;
using DoorofSoul.Protocol.Communication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                Application.Log.InfoFormat("Player Guid: {0} Connect from {1}", player.Guid, player.LastConnectedIPAddress);
                return true;
            }
        }
        public async void PlayerDisconnect(ServerPlayer player)
        {
            if(connectedPlayers.ContainsKey(player.Guid))
            {
                connectedPlayers.Remove(player.Guid);
                Application.Log.InfoFormat("Player Guid: {0} Disconnect from {1}", player.Guid, player.LastConnectedIPAddress);
            }
            PlayerOffline(player);
            await Task.Delay(600);
            if (!player.IsOnline)
            {
                PlayerDeactivate(player);
            }
        }

        public bool PlayerLogin(ServerPlayer player, string account, string password, out string debugMessage, out ErrorCode errorCode)
        {
            int playerID;
            if (Database.Database.RepositoryList.PlayerRepository.Contains(account, out playerID))
            {
                if (Database.Database.RepositoryList.PlayerRepository.LoginCheck(account, password))
                {
                    debugMessage = null;
                    errorCode = ErrorCode.NoError;
                    player.LoadPlayer(Database.Database.RepositoryList.PlayerRepository.Find(playerID));
                    if(PlayerOnline(player))
                    {
                        return true;
                    }
                    else
                    {
                        player.IsOnline = false;
                        debugMessage = string.Format("Account:{0} already Logined from IP: {1}", account ?? "", player.LastConnectedIPAddress?.ToString() ?? "");
                        errorCode = ErrorCode.Fail;
                        return false;
                    }
                }
                else
                {
                    debugMessage = string.Format("Account:{0} PasswordError from IP: {1}", account ?? "", player.LastConnectedIPAddress?.ToString() ?? "");
                    errorCode = ErrorCode.InvalidOperation;
                    return false;
                }
            }
            else
            {
                debugMessage = string.Format("Account:{0} Not Exist from IP: {1}", account ?? "", player?.LastConnectedIPAddress?.ToString() ?? "");
                errorCode = ErrorCode.InvalidOperation;
                return false;
            }
        }
        public void PlayerLogout(ServerPlayer player)
        {
            PlayerDisconnect(player);
            PlayerDeactivate(player);
        }

        public bool PlayerOnline(ServerPlayer player)
        {
            if(onlinedPlayers.ContainsKey(player.PlayerID))
            {
                return false;
            }
            else
            {
                if (activatedPlayers.ContainsKey(player.PlayerID))
                {
                    ServerPlayer originPlayer = activatedPlayers[player.PlayerID];
                    originPlayer.RelifeWithNewPlayer(player);
                    connectedPlayers.Remove(player.Guid);
                    connectedPlayers.Add(originPlayer.Guid, originPlayer);
                    player = originPlayer;
                    player.IsOnline = true;
                    if (!onlinedPlayers.ContainsKey(player.PlayerID))
                    {
                        onlinedPlayers.Add(player.PlayerID, player);
                    }
                    Application.Log.InfoFormat("PlayerID: {0} Return to World", player.PlayerID);
                    return true;
                }
                else
                {
                    onlinedPlayers.Add(player.PlayerID, player);
                    player.IsOnline = true;
                    Application.Log.InfoFormat("PlayerID: {0} Online", player.PlayerID);
                    return PlayerActive(player);
                }
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
                Hexagram.Hexagram.Throne.ProjectPlayer(player);
                return true;
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
                Hexagram.Hexagram.Throne.ExtractPlayer(player);
            }
        }
    }
}
