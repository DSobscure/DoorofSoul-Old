using DoorofSoul.Library.General;
using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Repositories
{
    public abstract class SoulRepository
    {
        public abstract Soul Create(Answer answer, string soulName);
        public abstract void Delete(int soulID);
        public abstract Soul Find(int soulID, Answer answer);
        public abstract void Save(Soul soul);
        public abstract List<Soul> ListOfAnswer(Answer answer);
    }
}
