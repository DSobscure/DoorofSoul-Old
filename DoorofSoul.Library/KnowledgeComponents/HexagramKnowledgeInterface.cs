using System;
using DoorofSoul.Database;
using DoorofSoul.Library.General.KnowledgeComponents;

namespace DoorofSoul.Library.KnowledgeComponents
{
    public class HexagramKnowledgeInterface : KnowledgeInterface
    {
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
