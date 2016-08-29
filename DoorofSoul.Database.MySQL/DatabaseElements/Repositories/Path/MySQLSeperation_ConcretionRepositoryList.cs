using DoorofSoul.Database.DatabaseElements.Repositories.Path;
using DoorofSoul.Database.DatabaseElements.Repositories.Path.Seperation_ConcretionRepositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.Path.Seperation_ConcretionRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.Path
{
    class MySQLSeperation_ConcretionRepositoryList : Seperation_ConcretionRepositoryList
    {
        private MySQLConsumablesLifePointEffectorPossessionRepository consumablesLifePointEffectorPossessionRepository;
        public override ConsumablesLifePointEffectorPossessionRepository ConsumablesLifePointEffectorPossessionRepository { get { return consumablesLifePointEffectorPossessionRepository; } }

        public MySQLSeperation_ConcretionRepositoryList()
        {
            consumablesLifePointEffectorPossessionRepository = new MySQLConsumablesLifePointEffectorPossessionRepository();
        }
    }
}
