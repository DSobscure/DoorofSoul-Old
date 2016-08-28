using DoorofSoul.Hexagram.LightComponents.Knowledge;
using DoorofSoul.Library.General.KnowledgeComponents;
using DoorofSoul.Library.General.LightComponents.HexagramInterfaces;
using DoorofSoul.Library.General.LightComponents.HexagramInterfaces.Knowledge;

namespace DoorofSoul.Hexagram.LightComponents
{
    public class HexagramKnowledgeInterface : KnowledgeInterface
    {
        private HexagramSkillInterface skillInterface;
        public SkillIgeInterface SkillInterface
        {
            get
            {
                return skillInterface;
            }
        }

        public HexagramKnowledgeInterface()
        {
            skillInterface = new HexagramSkillInterface();
        }

        public Skill FindSkill(int skillID)
        {
            return Hexagram.Knowledge.SkillManager.FindSkill(skillID);
        }

        public StatusEffect FindStatusEffect(int statusEffectID)
        {
            return Hexagram.Knowledge.StatusEffectManager.FindStatusEffect(statusEffectID);
        }
    }
}
