﻿using DoorofSoul.Client.Communication;
using DoorofSoul.Client.Interfaces;
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
        public static Seat Seat { get; private set; }

        public static readonly string ServerName = "DoorofSoul.Server";
        public static readonly string ServerAddress = "doorofsoul.duckdns.org";
        public static readonly int ServerPort = 5055;

        public static readonly string AlphaServerName = "DoorofSoul.AlphaServer";
        public static readonly string AlphaServerAddress = "doorofsoul.duckdns.org";
        public static readonly int AlphaServerPort = 5056;

        public static bool IsObserver { get; set; }

        public static IInputScenario InputManager { get; set; }


        static Global()
        {
            LibraryInstance.Initial(SystemManager.Error, SystemManager.ErrorFormat, null);
            Horizon = new Horizon();
            PhotonService = new PhotonService();
            SystemManager = new SystemManager();

            ClientPlayerCommunicationInterface communicationInterface = new ClientPlayerCommunicationInterface(SystemManager, PhotonService, Horizon);
            Player = new Player(communicationInterface);
            communicationInterface.BindPlayer(Player);
            Seat = new Seat();
        }
    }
}

