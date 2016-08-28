using DoorofSoul.Library.General.MindComponents.SoulElements;

namespace DoorofSoul.Database.DatabaseElements.Repositories.MindRepositories.SoulElementsRepositories
{
    public abstract class SoulAttributesRepository
    {
        public abstract SoulAttributes Create(int soulID, SoulAttributes soulAttributes);
        public abstract void Delete(int soulID);
        public abstract SoulAttributes Find(int soulID);
        public abstract void Save(int soulID, SoulAttributes soulAttributes);
    }
}
