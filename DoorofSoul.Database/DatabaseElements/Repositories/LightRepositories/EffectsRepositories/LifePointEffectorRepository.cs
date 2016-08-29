using DoorofSoul.Library.General.LightComponents.Effects.Effectors;

namespace DoorofSoul.Database.DatabaseElements.Repositories.LightRepositories.EffectsRepositories
{
    public abstract class LifePointEffectorRepository
    {
        public abstract LifePointEffector Create(decimal effectValue);
        public abstract void Delete(int effectorID);
        public abstract LifePointEffector Find(int effectorID);
        public abstract void Save(LifePointEffector effector);
    }
}
