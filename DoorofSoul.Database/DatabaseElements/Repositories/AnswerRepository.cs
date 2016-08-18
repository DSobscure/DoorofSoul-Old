using DoorofSoul.Library.General;
using DoorofSoul.Library.General.ThroneComponents;

namespace DoorofSoul.Database.DatabaseElements.Repositories
{
    public abstract class AnswerRepository
    {
        public abstract Answer Find(int answerID, Player correspondingPlayer);
        public abstract void Save(Answer answer);
    }
}
