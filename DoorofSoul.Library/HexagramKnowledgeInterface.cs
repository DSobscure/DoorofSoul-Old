using System;
using DoorofSoul.Database;
using DoorofSoul.Library.General.HeptagramSystems;
using DoorofSoul.Protocol;

namespace DoorofSoul.Library
{
    public class HexagramKnowledgeInterface : KnowledgeInterface
    {
        public Skill FindSkill(HeptagramSystemTypeCode systemTypeCode, int skillID)
        {
            return Hexagram.Instance.Knowledge.ChoseSystem(systemTypeCode).FindSkill(skillID);
        }
    }
}
