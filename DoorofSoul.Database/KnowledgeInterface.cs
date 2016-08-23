using DoorofSoul.Library.General.KnowledgeComponents;

namespace DoorofSoul.Database
{
    public interface KnowledgeInterface
    {
        Skill FindSkill(int skillID);
        StatusEffect FindStatusEffect(int statusEffectID);
    }
}
