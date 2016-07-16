using DoorofSoul.Server.DatabaseElements;
using System.Data.Common;
using System;

namespace DoorofSoul.Server
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
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                return connection;
            }
        }

        protected DataBase()
        {
            AuthenticationManager = new AuthenticationManager();
            RepositoryManager = new RepositoryManager();
        }
        public abstract bool Connect(string hostName, string userName, string password, string database);

        public void Dispose()
        {
            connection.Close();
        }

        public AuthenticationManager AuthenticationManager { get; protected set; }
        public RepositoryManager RepositoryManager { get; protected set; }
    }
}
