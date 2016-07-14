using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoorofSoul.Server.DatabaseElements;
using System.Data.Common;

namespace DoorofSoul.Server
{
    public abstract class DataBase
    {
        protected static DataBase instance;
        public static DataBase Instance { get { return instance; } }
        public static void Initial(DataBase instance)
        {
            DataBase.instance = instance;
        }

        protected DbConnection connection;
        public DbConnection Connection { get { return connection; } }

        public abstract bool Connect(string hostName, string userName, string password, string database);

        public AuthenticationManager AuthenticationManager { get; protected set; }
        public RepositoryManager RepositoryManager { get; protected set; }
    }
}
