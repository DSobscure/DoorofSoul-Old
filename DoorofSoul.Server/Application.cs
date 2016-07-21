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
        public PlayerFactory PlayerFactory { get; protected set; }


        protected override void Setup()
        {
            instance = this;
            SetupLog();
            SetupConfiguration();
            SetupDatabase();
            PlayerFactory = new PlayerFactory();
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
            while(!PlayerFactory.PlayerConnect(player))
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
    }
}
