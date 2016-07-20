using DoorofSoul.Library.General;
using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Repositories
{
    public abstract class SoulRepository
    {
        public abstract Soul Create(int answerID, string soulName);
        public abstract void Delete(int soulID);
        public abstract Soul Find(int soulID);
        public abstract void Save(Soul soul);
        public abstract List<Soul> List();
        public abstract List<Soul> ListOfAnswer(int answerID);
    }
}
