using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Repositories.Path.Seperation_ConcretionRepositories
{
    public abstract class ConsumablesShooterAbilitiesEffectorPossessionRepository
    {
        public abstract List<int> GetShooterAbilitiesEffectorIDs(int consumablesID);
        public abstract void AddPossession(int consumablesID, int shooterAbilitiesEffectorID);
        public abstract void RemovePossession(int consumablesID, int shooterAbilitiesEffectorID);
    }
}
