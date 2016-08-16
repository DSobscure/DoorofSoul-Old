using System;
using System.Collections.Generic;
using DoorofSoul.Protocol;

namespace DoorofSoul.Library.General.SoulElements
{
    public class SkillLibrary
    {
        public Soul Owner { get; protected set; }
        protected Dictionary<int, SkillInfo> skillInfoDictionry;
        protected Dictionary<HeptagramSystemTypeCode, Dictionary<int, SkillInfo>> skillInfoSystemDictionries;
        public IEnumerable<HeptagramSystemTypeCode> Systems { get { return skillInfoSystemDictionries.Keys; } }
        public IEnumerable<SkillInfo> SkillInfos { get { return skillInfoDictionry.Values; } }

        public SkillLibrary(Soul owner)
        {
            Owner = owner;
            skillInfoDictionry = new Dictionary<int, SkillInfo>();
            skillInfoSystemDictionries = new Dictionary<HeptagramSystemTypeCode, Dictionary<int, SkillInfo>>();
        }
        public bool ContainsSkillInfo(int skillInfoID)
        {
            return skillInfoDictionry.ContainsKey(skillInfoID);
        }
        public void LoadSkillInfo(SkillInfo info)
        {
            if(info.UnderstanderSoulID == Owner.SoulID && !ContainsSkillInfo(info.SkillInfoID))
            {
                skillInfoDictionry.Add(info.SkillInfoID, info);
                if(!skillInfoSystemDictionries.ContainsKey(info.Skill.SystemTypeCode))
                {
                    skillInfoSystemDictionries.Add(info.Skill.SystemTypeCode, new Dictionary<int, SkillInfo>());
                }
                if(!skillInfoSystemDictionries[info.Skill.SystemTypeCode].ContainsKey(info.SkillInfoID))
                {
                    skillInfoSystemDictionries[info.Skill.SystemTypeCode].Add(info.SkillInfoID, info);
                }
            }
        }
        public void LoadSkillInfos(List<SkillInfo> infos)
        {
            foreach(SkillInfo info in infos)
            {
                if (info.UnderstanderSoulID == Owner.SoulID && !ContainsSkillInfo(info.SkillInfoID))
                {
                    skillInfoDictionry.Add(info.SkillInfoID, info);
                    if (!skillInfoSystemDictionries.ContainsKey(info.Skill.SystemTypeCode))
                    {
                        skillInfoSystemDictionries.Add(info.Skill.SystemTypeCode, new Dictionary<int, SkillInfo>());
                    }
                    if (!skillInfoSystemDictionries[info.Skill.SystemTypeCode].ContainsKey(info.SkillInfoID))
                    {
                        skillInfoSystemDictionries[info.Skill.SystemTypeCode].Add(info.SkillInfoID, info);
                    }
                }
            }
        }
        public IEnumerable<SkillInfo> GetSkillInfosBySystem(HeptagramSystemTypeCode systemCode)
        {
            if(skillInfoSystemDictionries.ContainsKey(systemCode))
            {
                return skillInfoSystemDictionries[systemCode].Values;
            }
            else
            {
                return null;
            }
        }
    }
}
