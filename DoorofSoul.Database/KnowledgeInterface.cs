using DoorofSoul.Protocol;
using DoorofSoul.Library.General.KnowledgeComponents.Skills;

namespace DoorofSoul.Database
{
    public interface KnowledgeInterface
    {
        Skill FindSkill(HeptagramSystemTypeCode systemTypeCode, int skillID);
    }
}
