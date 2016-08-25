using DoorofSoul.Database.DatabaseElements.Connections;
using DoorofSoul.Database.DatabaseElements.Connections.KnowledgeConnections;
using DoorofSoul.Database.MySQL.DatabaseElements.Connections.KnowledgeConnections;
using MySql.Data.MySqlClient;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Connections
{
    class MySQLKnowledgeConnection : KnowledgeConnection
    {
        private MySQLSkillsConnection skillsConnection;
        private MySQLStatusEffectsConnection statusEffectsConnection;

        public override SkillsConnection SkillsConnection { get { return skillsConnection; } }
        public override StatusEffectsConnection StatusEffectsConnection { get { return statusEffectsConnection; } }

        public MySQLKnowledgeConnection()
        {
            skillsConnection = new MySQLSkillsConnection();
            statusEffectsConnection = new MySQLStatusEffectsConnection();

            childConnections.Add(skillsConnection);
            childConnections.Add(statusEffectsConnection);
        }

        public override bool Connect(string hostName, string userName, string password, string database)
        {
            string databaseName = "Knowledge";
            string connectString = string.Format("server={0};uid={1};pwd={2};database={3}_{4}", hostName, userName, password, database, databaseName);
            connection = new MySqlConnection(connectString);

            childConnections.ForEach(x => x.Connect(hostName, userName, password, string.Format("{0}_{1}", database, databaseName)));

            return Connected;
        }
    }
}
