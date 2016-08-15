using System;
using System.Collections.Generic;
using DoorofSoul.Protocol;
using DoorofSoul.Library.General.HeptagramSystems;

namespace DoorofSoul.Library.General.SoulElements
{
    public class SkillInfo
    {
        public int SkillInfoID { get; protected set; }
        public int UnderstanderSoulID { get; protected set; }
        public Skill Skill { get; protected set; }
        public byte SkillLevel { get; protected set; }
        public SkillPitch SkillPitch { get; protected set; }

        public SkillInfo() { }
        public SkillInfo(int skillInfoID, int understanderSoulID, Skill skill, byte skillLevel, SkillPitch skillPitch)
        {
            SkillInfoID = skillInfoID;
            UnderstanderSoulID = understanderSoulID;
            Skill = skill;
            SkillLevel = skillLevel;
            skillPitch = SkillPitch;
        }
    }
}
