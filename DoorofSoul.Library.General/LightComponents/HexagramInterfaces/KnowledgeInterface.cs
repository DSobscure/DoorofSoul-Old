using DoorofSoul.Library.General.KnowledgeComponents;
using DoorofSoul.Library.General.LightComponents.HexagramInterfaces.Knowledge;

namespace DoorofSoul.Library.General.LightComponents.HexagramInterfaces
{
    public interface KnowledgeInterface
    {
        Skill FindSkill(int skillID);
        StatusEffect FindStatusEffect(int statusEffectID);

        SkillIgeInterface SkillInterface { get;}
    }
}
