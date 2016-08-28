using DoorofSoul.Database.DatabaseElements.Repositories.MindRepositories;
using DoorofSoul.Database.DatabaseElements.Repositories.MindRepositories.SoulElementsRepositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.MindRepositories.SoulElementsRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.MindRepositories
{
    class MySQLSoulElementsRepositoryList : SoulElementsRepositoryList
    {
        private MySQLSoulAttributesRepository soulAttributesRepository;
        private MySQLSoulKernelAbilitiesRepository soulKernelAbilitiesRepository;
        private MySQLSoulPhaseRepository soulPhaseRepository;

        public override SoulAttributesRepository SoulAttributesRepository { get { return soulAttributesRepository; } }
        public override SoulKernelAbilitiesRepository SoulKernelAbilitiesRepository { get { return soulKernelAbilitiesRepository; } }
        public override SoulPhaseRepository SoulPhaseRepository { get { return soulPhaseRepository; } }

        public MySQLSoulElementsRepositoryList()
        {
            soulAttributesRepository = new MySQLSoulAttributesRepository();
            soulKernelAbilitiesRepository = new MySQLSoulKernelAbilitiesRepository();
            soulPhaseRepository = new MySQLSoulPhaseRepository();
        }
    }
}
