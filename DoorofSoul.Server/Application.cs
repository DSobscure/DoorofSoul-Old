using DoorofSoul.Library;
using DoorofSoul.Server.Config;
using DoorofSoul.Database;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using Photon.SocketServer;
using System.IO;
using System.Threading.Tasks;

namespace DoorofSoul.Server
{
    public class Application : ApplicationBase
    {
        private static Application instance;
        public static Application ServerInstance { get { return instance; } }
        public static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public SystemConfiguration SystemConfiguration { get; set; }
        private PlayerFactory playerFactory;
        private Hexagram hexagram;

        protected override void Setup()
        {
            instance = this;
            SetupLog();
            SetupConfiguration();
            SetupDatabase();
            playerFactory = new PlayerFactory();
            hexagram = new Hexagram();
            Log.Info("Server Setup Successiful.......");
        }

        protected override void TearDown()
        {
            DataBase.Instance.Dispose();
        }

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            ServerPlayer player;
            Peer peer = new Peer(initRequest, out player);
            while(!playerFactory.PlayerConnect(player))
            {
                peer = new Peer(initRequest, out player);
            }
            return peer;
        }

        protected void SetupLog()
        {
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationPath, "log");
            FileInfo file = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));
            if (file.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
                XmlConfigurator.ConfigureAndWatch(file);
            }
        }
        protected void SetupConfiguration()
        {
            SystemConfiguration = SystemConfiguration.Load(Path.Combine(this.ApplicationPath, "config", "system.config"));
        }

        protected void SetupDatabase()
        {
            DataBase.Initial(new MySQLDatabase(Log));
            DataBase.Instance.Connect(SystemConfiguration.DatabaseHostname, SystemConfiguration.DatabaseUsername, SystemConfiguration.DatabasePassword, SystemConfiguration.Database);
        }

        public bool PlayerLogin(ServerPlayer player, string account, string password, out string debugMessage, out string errorMessage)
        {
            int playerID;
            if(DataBase.Instance.RepositoryManager.PlayerRepository.Contains(account, out playerID))
            {
                if(DataBase.Instance.AuthenticationManager.PlayerAuthentication.LoginCheck(account, password))
                {
                    debugMessage = null;
                    errorMessage = null;
                    player.LoadPlayer(DataBase.Instance.RepositoryManager.PlayerRepository.Find(playerID));
                    return playerFactory.PlayerOnline(player);
                }
                else
                {
                    debugMessage = string.Format("Account:{0} PasswordError from IP: {1}", account ?? "", player.LastConnectedIPAddress?.ToString() ?? "");
                    errorMessage = LauguageDictionarySelector.Instance[player.UsingLanguage]["Account or Password Error"];
                    return false;
                }
            }
            else
            {
                debugMessage = string.Format("Account:{0} Not Exist from IP: {1}", account ?? "", player?.LastConnectedIPAddress?.ToString() ?? "");
                errorMessage = LauguageDictionarySelector.Instance[player.UsingLanguage]["Account or Password Error"];
                return false;
            }
        }
        public void PlayerLogout(ServerPlayer player)
        {
            playerFactory.PlayerDisconnect(player);
            playerFactory.PlayerDeactivate(player);
        }
        public async void PlayerDisconnect(ServerPlayer player)
        {
            playerFactory.PlayerDisconnect(player);
            await Task.Delay(60000);
            if(!player.IsOnline)
            {
                playerFactory.PlayerDeactivate(player);
            }
        }
    }
}
