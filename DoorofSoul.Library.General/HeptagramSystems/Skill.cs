using DoorofSoul.Protocol;

namespace DoorofSoul.Library.General.HeptagramSystems
{
    public class Skill
    {
        public int SkillID { get; protected set; }
        public HeptagramSystemTypeCode SystemTypeCode { get; protected set; }
        public string SkillName { get; protected set; }
        public decimal BasicLifePointCost { get; protected set; }
        public decimal BasicEnergyPointCost { get; protected set; }
        public decimal BasicCorePointCost { get; protected set; }
        public decimal BasicSpiritPointCost { get; protected set; }

        public Skill() { }
        public Skill(int skillID, HeptagramSystemTypeCode systemTypeCode, string skillName, decimal basicLifePointCost, decimal basicEnergyPointCost, decimal basicCorePointCost, decimal basicSpiritPointCost)
        {
            SkillID = skillID;
            SystemTypeCode = systemTypeCode;
            SkillName = skillName;
            BasicLifePointCost = basicLifePointCost;
            BasicEnergyPointCost = basicEnergyPointCost;
            BasicCorePointCost = basicCorePointCost;
            BasicSpiritPointCost = basicSpiritPointCost;
        }
    }
}
