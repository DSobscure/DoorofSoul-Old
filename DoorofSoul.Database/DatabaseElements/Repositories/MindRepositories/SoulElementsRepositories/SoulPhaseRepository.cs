using DoorofSoul.Library.General.MindComponents.SoulElements;
using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Repositories.MindRepositories.SoulElementsRepositories
{
    public abstract class SoulPhaseRepository
    {
        public abstract SoulPhase Create(int soulID, SoulPhase phase);
        public abstract void Delete(int soulID);
        public abstract SoulPhase Find(int soulID, byte phaseLevel);
        public abstract void Save(int soulID, SoulPhase phase);
        public abstract List<SoulPhase> ListOfSoul(int soulID);
        public abstract void SavePhases(int soulID, SoulPhase[] phases);
    }
}
