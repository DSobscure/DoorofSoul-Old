using DoorofSoul.Library.General;
using DoorofSoul.Library.General.ThroneComponents;
using DoorofSoul.Protocol;
using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Repositories.Throne
{
    public abstract class SoulRepository
    {
        public abstract Soul Create(Answer answer, string soulName, SoulKernelTypeCode mainSoulType);
        public abstract void Delete(int soulID);
        public abstract Soul Find(int soulID, Answer answer);
        public abstract void Save(Soul soul);
        public abstract List<Soul> ListOfAnswer(Answer answer);
    }
}
