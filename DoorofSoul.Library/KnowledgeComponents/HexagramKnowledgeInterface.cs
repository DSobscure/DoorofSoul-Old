using System;
using DoorofSoul.Database;
using DoorofSoul.Library.General.Skills;
using DoorofSoul.Protocol;

namespace DoorofSoul.Library.KnowledgeComponents
{
    public class HexagramKnowledgeInterface : KnowledgeInterface
    {
        public Skill FindSkill(HeptagramSystemTypeCode systemTypeCode, int skillID)
        {
            return Hexagram.Instance.Knowledge.ChoseSystem(systemTypeCode).FindSkill(skillID);
        }
    }
}
