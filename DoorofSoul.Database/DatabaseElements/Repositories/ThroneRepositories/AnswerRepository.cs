using DoorofSoul.Library.General;
using DoorofSoul.Library.General.ThroneComponents;

namespace DoorofSoul.Database.DatabaseElements.Repositories.ThroneRepositories
{
    public abstract class AnswerRepository
    {
        public abstract int Create(int soulCountLimit);
        public abstract Answer Find(int answerID, Player correspondingPlayer);
        public abstract void Save(Answer answer);
    }
}
