using DoorofSoul.Database.MySQL.DatabaseElements.Connections;
using DoorofSoul.Database.DatabaseElements;
using MySql.Data.MySqlClient;

namespace DoorofSoul.Database.MySQL.DatabaseElements
{
    class MySQLConnectionList : ConnectionList
    {
        public MySQLConnectionList()
        {
            KnowledgeConnection = new MySQLKnowledgeConnection();
            ElementConnection = new MySQLElementConnection();
            LoveConnection = new MySQLLoveConnection();
            NatureConnection = new MySQLNatureConnection();
            ThroneConnection = new MySQLThroneConnection();

            childConnections.Add(KnowledgeConnection);
            childConnections.Add(ElementConnection);
            childConnections.Add(LoveConnection);
            childConnections.Add(NatureConnection);
            childConnections.Add(ThroneConnection);
        }

        public override bool Connect(string hostName, string userName, string password, string database)
        {
            string connectString = string.Format("server={0};uid={1};pwd={2};database={3}", hostName, userName, password, database);
            connection = new MySqlConnection(connectString);

            childConnections.ForEach(x => x.Connect(hostName, userName, password, database));

            return Connected;
        }
    }
}
