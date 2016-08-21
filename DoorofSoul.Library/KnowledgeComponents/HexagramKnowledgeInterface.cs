using DoorofSoul.Database;
using DoorofSoul.Library.General.KnowledgeComponents;

namespace DoorofSoul.Library.KnowledgeComponents
{
    public class HexagramKnowledgeInterface : KnowledgeInterface
    {
        public Skill FindSkill(int skillID)
        {
            return Hexagram.Instance.Knowledge.FindSkill(skillID);
        }
    }
}
