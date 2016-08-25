using DoorofSoul.Hexagram.LightComponents.Skills;
using DoorofSoul.Library.General.KnowledgeComponents;
using DoorofSoul.Library.General.LightComponents;
using DoorofSoul.Library.General.LightComponents.Knowledge;

namespace DoorofSoul.Hexagram.LightComponents
{
    public class HexagramKnowledgeInterface : KnowledgeInterface
    {
        private HexagramSkillInterface skillKnowledgeInterface;
        public SkillKnowledgeInterface SkillKnowledgeInterface
        {
            get
            {
                return skillKnowledgeInterface;
            }
        }

        public HexagramKnowledgeInterface()
        {
            skillKnowledgeInterface = new HexagramSkillInterface();
        }

        public Skill FindSkill(int skillID)
        {
            return Hexagram.Instance.Knowledge.SkillManager.FindSkill(skillID);
        }

        public StatusEffect FindStatusEffect(int statusEffectID)
        {
            return Hexagram.Instance.Knowledge.StatusEffectManager.FindStatusEffect(statusEffectID);
        }
    }
}
