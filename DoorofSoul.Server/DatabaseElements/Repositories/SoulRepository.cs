using DoorofSoul.Library.General;
using System.Collections.Generic;

namespace DoorofSoul.Server.DatabaseElements.Repositories
{
    public abstract class SoulRepository
    {
        public abstract Soul Find(int soulID);
        public abstract void Save(Soul soul);
        public abstract List<Soul> List();
        public abstract List<Soul> ListOfAnswer(int answerID);
        public abstract List<Soul> ListOfContainer(int containerID);
    }
}
