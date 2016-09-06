using DoorofSoul.Database.DatabaseElements.Repositories.LightRepositories;
using DoorofSoul.Database.DatabaseElements.Repositories.LightRepositories.EffectsRepositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.LightRepositories.EffectsRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.LightRepositories
{
    class MySQLEffectsRepositoryList : EffectsRepositoryList
    {
        private MySQLLifePointEffectorRepository containerLifePointEffectorRepository;

        public override LifePointEffectorRepository ContainerLifePointEffectorRepository { get { return containerLifePointEffectorRepository; } }

        public MySQLEffectsRepositoryList()
        {
            containerLifePointEffectorRepository = new MySQLLifePointEffectorRepository();
        }
    }
}
