using DoorofSoul.Database.DatabaseElements.Repositories.LightRepositories;
using DoorofSoul.Database.DatabaseElements.Repositories.LightRepositories.EffectsRepositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.LightRepositories.EffectsRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.LightRepositories
{
    class MySQLEffectsRepositoryList : EffectsRepositoryList
    {
        private MySQLLifePointEffectorRepository containerLifePointEffectorRepository;
        private MySQLShooterAbilitiesEffectorRepository shooterAbilitiesEffectorRepository;

        public override LifePointEffectorRepository ContainerLifePointEffectorRepository { get { return containerLifePointEffectorRepository; } }
        public override ShooterAbilitiesEffectorRepository ShooterAbilitiesEffectorRepository { get { return shooterAbilitiesEffectorRepository; } }

        public MySQLEffectsRepositoryList()
        {
            containerLifePointEffectorRepository = new MySQLLifePointEffectorRepository();
            shooterAbilitiesEffectorRepository = new MySQLShooterAbilitiesEffectorRepository();
        }
    }
}
