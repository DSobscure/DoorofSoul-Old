using DoorofSoul.Protocol;
using DoorofSoul.Library.General.KnowledgeElements.Skills;

namespace DoorofSoul.Database
{
    public interface KnowledgeInterface
    {
        Skill FindSkill(HeptagramSystemTypeCode systemTypeCode, int skillID);
    }
}
