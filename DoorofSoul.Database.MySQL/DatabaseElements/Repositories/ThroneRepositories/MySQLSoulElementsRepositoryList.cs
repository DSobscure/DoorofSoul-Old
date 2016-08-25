using System;
using DoorofSoul.Database.DatabaseElements.Repositories.ThroneRepositories;
using DoorofSoul.Database.DatabaseElements.Repositories.ThroneRepositories.SoulElementsRepositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.ThroneRepositories.SoulElementsRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.ThroneRepositories
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
