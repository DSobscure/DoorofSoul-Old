using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Repositories.Path.Seperation_ConcretionRepositories
{
    public abstract class ConsumablesLifePointEffectorPossessionRepository
    {
        public abstract List<int> GetLifePointEffectorIDs(int consumablesID);
        public abstract void AddPossession(int consumablesID, int lifePointEffectorID);
        public abstract void RemovePossession(int consumablesID, int lifePointEffectorID);
    }
}
