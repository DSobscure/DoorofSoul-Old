using DoorofSoul.Protocol;

namespace DoorofSoul.Library.General.KnowledgeElements.Skills
{
    public class Skill
    {
        public int SkillID { get; protected set; }
        public HeptagramSystemTypeCode SystemTypeCode { get; protected set; }
        public SkillMediaTypeCode MediaTypeCode { get; protected set; }
        public string SkillName { get; protected set; }
        public decimal BasicLifePointCost { get; protected set; }
        public decimal BasicEnergyPointCost { get; protected set; }
        public decimal BasicCorePointCost { get; protected set; }
        public decimal BasicSpiritPointCost { get; protected set; }

        public Skill() { }
        public Skill(int skillID, HeptagramSystemTypeCode systemTypeCode, SkillMediaTypeCode mediaTypeCode, string skillName, decimal basicLifePointCost = 0, decimal basicEnergyPointCost = 0, decimal basicCorePointCost = 0, decimal basicSpiritPointCost = 0)
        {
            SkillID = skillID;
            SystemTypeCode = systemTypeCode;
            MediaTypeCode = mediaTypeCode;
            SkillName = skillName;
            BasicLifePointCost = basicLifePointCost;
            BasicEnergyPointCost = basicEnergyPointCost;
            BasicCorePointCost = basicCorePointCost;
            BasicSpiritPointCost = basicSpiritPointCost;
        }
    }
}
