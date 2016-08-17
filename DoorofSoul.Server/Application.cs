using DoorofSoul.Database;
using DoorofSoul.Library;
using DoorofSoul.Library.General;
using DoorofSoul.Library.General.BasicTypeHelperFunctions;
using DoorofSoul.Library.General.ContainerElements;
using DoorofSoul.Library.General.EntityElements;
using DoorofSoul.Library.General.Skills;
using DoorofSoul.Library.General.SoulElements;
using DoorofSoul.Library.KnowledgeComponents;
using DoorofSoul.Protocol;
using DoorofSoul.Server.Config;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using Photon.SocketServer;
using System.IO;

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
            SetupHexagram();
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
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(ApplicationPath, "log");
            FileInfo file = new FileInfo(Path.Combine(BinaryPath, "log4net.config"));
            if (file.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
                XmlConfigurator.ConfigureAndWatch(file);
            }
            LibraryInstance.Initial(Log.Error, Log.ErrorFormat);
        }
        protected void SetupConfiguration()
        {
            SystemConfiguration = SystemConfiguration.Load(Path.Combine(ApplicationPath, "config", "system.config"));
            Photon.SocketServer.Protocol.TryRegisterCustomType(typeof(Item), (byte)SerializationClassTypeCode.Item, Item.Serialize, Item.Deserialize);
            Photon.SocketServer.Protocol.TryRegisterCustomType(typeof(EntitySpaceProperties), (byte)SerializationClassTypeCode.EntitySpaceProperties, EntitySpaceProperties.Serialize, EntitySpaceProperties.Deserialize);
            Photon.SocketServer.Protocol.TryRegisterCustomType(typeof(decimal), (byte)SerializationClassTypeCode.Decimal, DecimalHelperFunction.Serialize, DecimalHelperFunction.Deserialize);
            Photon.SocketServer.Protocol.TryRegisterCustomType(typeof(SoulAttributes), (byte)SerializationClassTypeCode.SoulAttributes, SoulAttributes.Serialize, SoulAttributes.Deserialize);
            Photon.SocketServer.Protocol.TryRegisterCustomType(typeof(ContainerAttributes), (byte)SerializationClassTypeCode.ContainerAttributes, ContainerAttributes.Serialize, ContainerAttributes.Deserialize);
            Photon.SocketServer.Protocol.TryRegisterCustomType(typeof(DSVector3), (byte)SerializationClassTypeCode.DSVector3, DSVector3.Serialize, DSVector3.Deserialize);
            Photon.SocketServer.Protocol.TryRegisterCustomType(typeof(SkillInfo), (byte)SerializationClassTypeCode.SkillInfo, SkillInfo.Serialize, SkillInfo.Deserialize);
        }

        protected void SetupDatabase()
        {
            DataBase.Initial(new MySQLDatabase(Log, new HexagramKnowledgeInterface()));
            DataBase.Instance.Connect(SystemConfiguration.DatabaseHostname, SystemConfiguration.DatabaseUsername, SystemConfiguration.DatabasePassword, SystemConfiguration.Database);
        }
        protected void SetupHexagram()
        {
            Hexagram.Initial(Log);
        }
    }
}
