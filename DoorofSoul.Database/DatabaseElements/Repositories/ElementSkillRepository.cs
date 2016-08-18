using DoorofSoul.Library.General.KnowledgeElements.Skills;
using DoorofSoul.Protocol;
using System.Collections.Generic;
namespace DoorofSoul.Database.DatabaseElements.Repositories
{
    public abstract class ElementSkillRepository
    {
        public abstract Skill Create(string skillName, SkillMediaTypeCode mediaTypeCode, decimal basicLifePointCost, decimal basicEnergyPointCost, decimal basicCorePointCost, decimal basicSpiritPointCost);
        public abstract void Delete(int skillID);
        public abstract Skill Find(int skillID);
        public abstract void Save(Skill skill);
        public abstract List<Skill> List();
    }
}
