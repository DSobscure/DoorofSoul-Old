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
        public static Application Instance { get { return instance; } }

        public static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        protected override void Setup()
        {
            throw new NotImplementedException();
        }

        protected override void TearDown()
        {
            throw new NotImplementedException();
        }

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            throw new NotImplementedException();
        }
    }
}
