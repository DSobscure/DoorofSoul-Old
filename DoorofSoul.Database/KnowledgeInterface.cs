using DoorofSoul.Protocol;
using DoorofSoul.Library.General.KnowledgeComponents.Skill;

namespace DoorofSoul.Database
{
    public interface KnowledgeInterface
    {
        Skill FindSkill(HeptagramSystemTypeCode systemTypeCode, int skillID);
    }
}
