using DoorofSoul.Library.General.KnowledgeComponents;
using DoorofSoul.Protocol;
using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Repositories.KnowledgeRepositories
{
    public abstract class SkillRepository
    {
        public abstract Skill Create(HeptagramSystemTypeCode systemTypeCode, SkillMediaTypeCode mediaTypeCode, string skillName, decimal basicLifePointCost, decimal basicEnergyPointCost, decimal basicCorePointCost, decimal basicSpiritPointCost);
        public abstract void Delete(int skillID);
        public abstract Skill Find(int skillID);
        public abstract void Save(Skill skill);
        public abstract List<Skill> List();
    }
}
