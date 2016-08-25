using DoorofSoul.Library.General.KnowledgeComponents;
using DoorofSoul.Library.General.LightComponents.Knowledge;

namespace DoorofSoul.Library.General.LightComponents
{
    public interface KnowledgeInterface
    {
        Skill FindSkill(int skillID);
        StatusEffect FindStatusEffect(int statusEffectID);

        SkillKnowledgeInterface SkillKnowledgeInterface { get;}
    }
}
