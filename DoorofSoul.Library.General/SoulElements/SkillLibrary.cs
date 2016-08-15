using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Library.General.SoulElements
{
    public class SkillLibrary
    {
        protected Soul owner;
        protected Dictionary<int, SkillInfo> skillInfoDictionry;
        public IEnumerable<SkillInfo> SkillInfos { get { return skillInfoDictionry.Values; } }

        public SkillLibrary(Soul owner)
        {
            this.owner = owner;
            skillInfoDictionry = new Dictionary<int, SkillInfo>();
        }
        public bool ContainsSkillInfo(int skillInfoID)
        {
            return skillInfoDictionry.ContainsKey(skillInfoID);
        }
        public void LoadSkillInfo(SkillInfo info)
        {
            if(info.UnderstanderSoulID == owner.SoulID && ContainsSkillInfo(info.SkillInfoID))
            {
                skillInfoDictionry.Add(info.SkillInfoID, info);
            }
        }
        public void LoadSkillInfos(List<SkillInfo> infos)
        {
            foreach(SkillInfo info in infos)
            {
                if (info.UnderstanderSoulID == owner.SoulID && ContainsSkillInfo(info.SkillInfoID))
                {
                    skillInfoDictionry.Add(info.SkillInfoID, info);
                }
            }
        }
    }
}
