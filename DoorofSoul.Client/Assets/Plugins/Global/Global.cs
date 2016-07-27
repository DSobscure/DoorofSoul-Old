using DoorofSoul.Client.Communication;
using DoorofSoul.Client.Scene;
using DoorofSoul.Library.General;

namespace DoorofSoul.Client.Global
{
    public static class Global
    {
        public static readonly PhotonService PhotonService;
        public static readonly SystemManager SystemManager;

        public static Player Player { get; private set; }
        public static World World { get; private set; }

        static Global()
        {
            PhotonService = new PhotonService("DoorofSoul.Server", "doorofsoul.duckdns.org", 5055);
            SystemManager = new SystemManager();
        }
    }
}

