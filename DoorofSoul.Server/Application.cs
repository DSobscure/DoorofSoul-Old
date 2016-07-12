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

namespace DoorofSoul.Server
{
    public class Application : ApplicationBase
    {
        private static Application instance;
        public static Application ServerInstance { get { return instance; } }

        public static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        protected override void Setup()
        {
            instance = this;
            ///set log
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationPath, "log");
            FileInfo file = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));
            if (file.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
                XmlConfigurator.ConfigureAndWatch(file);
            }
        }

        protected override void TearDown()
        {
            
        }

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new Peer(initRequest);
        }
    }
}
