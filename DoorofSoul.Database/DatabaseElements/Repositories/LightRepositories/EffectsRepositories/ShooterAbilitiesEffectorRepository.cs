using DoorofSoul.Library.General.LightComponents.Effects.Effectors;
using DoorofSoul.Protocol;

namespace DoorofSoul.Database.DatabaseElements.Repositories.LightRepositories.EffectsRepositories
{
    public abstract class ShooterAbilitiesEffectorRepository
    {
        public abstract ShooterAbilitiesEffector Create(ShooterAbilitiesTypeCode AbilityType, int effectValue);
        public abstract void Delete(int effectorID);
        public abstract ShooterAbilitiesEffector Find(int effectorID);
        public abstract void Save(ShooterAbilitiesEffector effector);
    }
}
