using DoorofSoul.Database.DatabaseElements;
using ExitGames.Logging;
using System;

namespace DoorofSoul.Database
{
    public abstract class Database
    {
        protected static Database instance;
        public static Database Instance { get { return instance; } }
        public static ConnectionList ConnectionList { get { return instance.connectionList; } }
        public static RepositoryList RepositoryList { get { return instance.repositoryList; } }
        public static ILogger Log { get { return instance.log; } }
        public static void Initial(Database instance)
        {
            Database.instance = instance;
        }
        protected ConnectionList connectionList;
        protected RepositoryList repositoryList;
        protected ILogger log;
        public KnowledgeInterface KnowledgeInterface { get; protected set; }

        protected Database(ILogger log, KnowledgeInterface knowledgeInterface)
        {
            this.log = log;
            KnowledgeInterface = knowledgeInterface;
        }
        public static bool Connect(string hostName, string userName, string password, string database)
        {
            ConnectionList.Connect(hostName, userName, password, database);
            return ConnectionList.Connected;
        }

        public static void Dispose()
        {
            ConnectionList.Dispose();
        }
    }
}
