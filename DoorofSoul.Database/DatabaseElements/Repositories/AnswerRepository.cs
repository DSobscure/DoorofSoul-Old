using DoorofSoul.Database.Library;
using DoorofSoul.Library.General;

namespace DoorofSoul.Database.DatabaseElements.Repositories
{
    public abstract class AnswerRepository
    {
        public abstract DatabaseAnswer Find(int answerID, Player correspondingPlayer);
        public abstract void Save(Answer answer);
    }
}
