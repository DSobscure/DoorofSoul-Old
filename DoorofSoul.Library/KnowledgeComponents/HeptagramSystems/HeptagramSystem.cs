using DoorofSoul.Library.General.KnowledgeComponents.Skill;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.ThroneComponents;
using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Communication;
using System.Collections.Generic;

namespace DoorofSoul.Library.KnowledgeComponents.HeptagramSystems
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
            if(!ContainsSkill(skill.SystemTypeCode, skill.SkillID))
            {
                skillDictionary.Add(skill.SkillID, skill);
            }
        }
        public void LoadSkills(List<Skill> skills)
        {
            foreach(Skill skill in skills)
            {
                if (!ContainsSkill(skill.SystemTypeCode, skill.SkillID))
                {
                    skillDictionary.Add(skill.SkillID, skill);
                }
            }
        }

        public virtual bool OperateSkill(Soul user, Container agent, SkillInfo skillInfo, Dictionary<byte, object> skillParameters, out Dictionary<byte, object> skillResponseParameters, out ErrorCode errorCode, out string debugMessage)
        {
            if(ContainsSkill(skillInfo.Skill.SystemTypeCode, skillInfo.SkillInfoID))
            {
                skillResponseParameters = null;
                errorCode = ErrorCode.NoError;
                debugMessage = null;
                return true;
            }
            else
            {
                skillResponseParameters = new Dictionary<byte, object>();
                errorCode = ErrorCode.NotExist;
                debugMessage = "SkillInfo Not Exist";
                return false;
            }
        }
    }
}
