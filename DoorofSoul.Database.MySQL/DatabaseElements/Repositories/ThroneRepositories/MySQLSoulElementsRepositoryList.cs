using DoorofSoul.Database.DatabaseElements.Repositories.ThroneRepositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.ThroneRepositories.SoulElementsRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.ThroneRepositories
{
    class MySQLSoulElementsRepositoryList : SoulElementsRepositoryList
    {
        protected override void InstantiateRepositories()
        {
            SoulAttributesRepository = new MySQLSoulAttributesRepository();
            SoulKernelAbilitiesRepository = new MySQLSoulKernelAbilitiesRepository();
            SoulPhaseRepository = new MySQLSoulPhaseRepository();
        }
    }
}
