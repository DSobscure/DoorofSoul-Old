using DoorofSoul.Database.MySQL.DatabaseElements.Connections.Path;
using DoorofSoul.Database.DatabaseElements.Connections;
using DoorofSoul.Database.DatabaseElements.Connections.Path;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Connections
{
    class MySQLPathConnectionList : PathConnectionList
    {
        private MySQLEgo_CognitionConnection ego_CognitionConnection;
        private MySQLSeperation_ConcretionConnection seperation_ConcretionConnection;

        public override Ego_CognitionConnection Ego_CognitionConnection { get { return ego_CognitionConnection; } }
        public override Seperation_ConcretionConnection Seperation_ConcretionConnection { get { return seperation_ConcretionConnection; } }

        public MySQLPathConnectionList()
        {
            ego_CognitionConnection = new MySQLEgo_CognitionConnection();
            seperation_ConcretionConnection = new MySQLSeperation_ConcretionConnection();

            childConnections.Add(ego_CognitionConnection);
            childConnections.Add(seperation_ConcretionConnection);
        }
        public override bool Connect(string hostName, string userName, string password, string database)
        {
            string databaseName = "Path";
            childConnections.ForEach(x => x.Connect(hostName, userName, password, string.Format("{0}_{1}", database, databaseName)));

            return Connected;
        }
    }
}
