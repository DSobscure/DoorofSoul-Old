using DoorofSoul.Protocol;
using DoorofSoul.Library.General.HeptagramSystems;

namespace DoorofSoul.Database
{
    public interface KnowledgeInterface
    {
        Skill FindSkill(HeptagramSystemTypeCode systemTypeCode, int skillID);
    }
}
