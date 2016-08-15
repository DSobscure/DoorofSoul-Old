using DoorofSoul.Library.General.HeptagramSystems;
using DoorofSoul.Library.General.SoulElements;
using DoorofSoul.Protocol;
using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Repositories
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
