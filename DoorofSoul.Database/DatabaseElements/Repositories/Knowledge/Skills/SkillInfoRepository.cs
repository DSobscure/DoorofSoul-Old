using DoorofSoul.Library.General.KnowledgeComponents.Skill;
using DoorofSoul.Library.General.ThroneComponents.SoulElements;
using DoorofSoul.Protocol;
using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Repositories.Knowledge.Skills
{
    public abstract class SkillInfoRepository
    {
        public abstract SkillInfo Create(int understanderSoulID, Skill skill, byte skillLevel, SkillPitch skillPatch);
        public abstract void Delete(int skillInfoID);
        public abstract SkillInfo Find(int skillInfoID);
        public abstract void Save(SkillInfo skillInfo);
        public abstract List<SkillInfo> ListOfUnderstander(int understanderSoulID);
    }
}
