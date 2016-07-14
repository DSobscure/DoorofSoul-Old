using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using log4net.Config;
using ExitGames.Logging.Log4Net;
using ExitGames.Logging;
using System.IO;
using DoorofSoul.Server.Config;
using DoorofSoul.Server.Databases;

namespace DoorofSoul.Server
{
    public class Application : ApplicationBase
    {
        private static Application instance;
        public static Application ServerInstance { get { return instance; } }
        public static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public SystemConfiguration SystemConfiguration { get; set; }
        private PlayerFactory playerFactory;

        protected override void Setup()
        {
            instance = this;
            SetupLog();
            SetupConfiguration();
            SetupDatabase();
            playerFactory = new PlayerFactory();

            Log.Info("Server Setup Successiful.......");
        }

        protected override void TearDown()
        {
            
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
            DataBase.Initial(new MySQLDatabase());
            DataBase.Instance.Connect(SystemConfiguration.DatabaseHostname, SystemConfiguration.DatabaseUsername, SystemConfiguration.DatabasePassword, SystemConfiguration.Database);
        }
    }
}
