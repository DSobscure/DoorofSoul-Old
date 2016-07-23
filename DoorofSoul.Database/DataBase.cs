using DoorofSoul.Database.DatabaseElements;
using System.Data.Common;
using System;
using ExitGames.Logging;

namespace DoorofSoul.Database
{
    public abstract class DataBase : IDisposable
    {
        protected static DataBase instance;
        public static DataBase Instance { get { return instance; } }
        public static void Initial(DataBase instance)
        {
            DataBase.instance = instance;
        }

        protected DbConnection connection;
        public DbConnection Connection
        {
            get
            {
                connection.Close();
                connection.Open();
                return connection;
            }
        }
        public AuthenticationManager AuthenticationManager { get; protected set; }
        public RepositoryManager RepositoryManager { get; protected set; }
        public RelationManager RelationManager { get; protected set; }
        public ILogger Log { get; protected set; }

        protected DataBase(ILogger log)
        {
            Log = log;
            AuthenticationManager = new AuthenticationManager();
            RepositoryManager = new RepositoryManager();
            RelationManager = new RelationManager();
        }
        public abstract bool Connect(string hostName, string userName, string password, string database);

        public void Dispose()
        {
            connection.Close();
        }

        
    }
}
