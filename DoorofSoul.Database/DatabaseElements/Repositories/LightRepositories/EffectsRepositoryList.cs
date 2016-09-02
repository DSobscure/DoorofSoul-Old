using DoorofSoul.Database.DatabaseElements.Repositories.LightRepositories.EffectsRepositories;

namespace DoorofSoul.Database.DatabaseElements.Repositories.LightRepositories
{
    public abstract class EffectsRepositoryList
    {
        public abstract LifePointEffectorRepository ContainerLifePointEffectorRepository { get; }
        public abstract ShooterAbilitiesEffectorRepository ShooterAbilitiesEffectorRepository { get; }
    }
}
