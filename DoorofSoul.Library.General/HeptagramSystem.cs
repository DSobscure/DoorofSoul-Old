using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol;
using DoorofSoul.Library.General.HeptagramSystems;

namespace DoorofSoul.Library.General
{
    public abstract class HeptagramSystem
    {
        public abstract HeptagramSystemTypeCode SystemTypeCode { get; }
        protected Dictionary<int, Skill> skillDictionary;

        protected HeptagramSystem()
        {
            skillDictionary = new Dictionary<int, Skill>();
        }

        public bool ContainsSkill(HeptagramSystemTypeCode systemTypeCode, int skillID)
        {
            return systemTypeCode == SystemTypeCode && skillDictionary.ContainsKey(skillID);
        }
        public Skill FindSkill(int skillID)
        {
            if(ContainsSkill(SystemTypeCode, skillID))
            {
                return skillDictionary[skillID];
            }
            else
            {
                return null;
            }
        }
        public void LoadSkill(Skill skill)
        {
            if(ContainsSkill(skill.SystemTypeCode, skill.SkillID))
            {
                skillDictionary.Add(skill.SkillID, skill);
            }
        }
        public void LoadSkills(List<Skill> skills)
        {
            foreach(Skill skill in skills)
            {
                if (ContainsSkill(skill.SystemTypeCode, skill.SkillID))
                {
                    skillDictionary.Add(skill.SkillID, skill);
                }
            }
        }
    }
}
