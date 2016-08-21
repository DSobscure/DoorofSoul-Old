using DoorofSoul.Library.General.ThroneComponents.SoulElements;

namespace DoorofSoul.Database.DatabaseElements.Repositories.ThroneRepositories.SoulElementsRepositories
{
    public abstract class SoulAttributesRepository
    {
        public abstract SoulAttributes Create(int soulID, SoulAttributes soulAttributes);
        public abstract void Delete(int soulID);
        public abstract SoulAttributes Find(int soulID);
        public abstract void Save(int soulID, SoulAttributes soulAttributes);
    }
}
