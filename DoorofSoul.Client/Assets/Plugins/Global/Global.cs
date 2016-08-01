using DoorofSoul.Client.Communication;
using DoorofSoul.Client.Library.General;
using DoorofSoul.Library.General;

namespace DoorofSoul.Client.Global
{
    public static class Global
    {
        public static readonly PhotonService PhotonService;
        public static readonly SystemManager SystemManager;

        public static Player Player { get; private set; }
        public static Horizon Horizon { get; private set; }

        static Global()
        {
            LibraryLog.Initial(SystemManager.Error, SystemManager.ErrorFormat);
            Horizon = new Horizon();
            PhotonService = new PhotonService("DoorofSoul.Server", "doorofsoul.duckdns.org", 5055);
            SystemManager = new SystemManager();

            ClientPlayerCommunicationInterface communicationInterface = new ClientPlayerCommunicationInterface(SystemManager, PhotonService, Horizon);
            Player = new Player(communicationInterface);
            communicationInterface.BindPlayer(Player);
            
        }
    }
}

