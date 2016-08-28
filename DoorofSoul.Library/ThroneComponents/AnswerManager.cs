using DoorofSoul.Library.General.ThroneComponents;
using DoorofSoul.Library.General.MindComponents;
using System.Collections.Generic;

namespace DoorofSoul.Hexagram.ThroneComponents
{
    public class AnswerManager
    {
        private Dictionary<int, Answer> answerDictionary;
        public IEnumerable<Answer> Answers { get { return answerDictionary.Values; } }

        public AnswerManager()
        {
            answerDictionary = new Dictionary<int, Answer>();
        }
        public void Initial()
        {

        }

        public bool ContainsAnswer(int answerID)
        {
            return answerDictionary.ContainsKey(answerID);
        }
        public Answer FindAnswer(int answerID)
        {
            if(ContainsAnswer(answerID))
            {
                return answerDictionary[answerID];
            }
            else
            {
                return null;
            }
        }

        public void ProjectAnswer(Answer answer)
        {
            if (!answerDictionary.ContainsKey(answer.AnswerID))
            {
                answerDictionary.Add(answer.AnswerID, answer);
                answer.LoadSouls(Database.Database.RepositoryList.MindRepositoryList.SoulRepository.ListOfAnswer(answer));
                foreach (Soul soul in answer.Souls)
                {
                    Hexagram.Mind.SoulManager.ProjectSoul(soul);
                }
            }
        }
        public void ExtractAnswer(Answer answer)
        {
            if (answerDictionary.ContainsKey(answer.AnswerID))
            {
                Database.Database.RepositoryList.ThroneRepositoryList.AnswerRepository.Save(answer);
                foreach (Soul soul in answer.Souls)
                {
                    Hexagram.Mind.SoulManager.ExtractSoul(soul);

                }
                answer.ClearSouls();
                answerDictionary.Remove(answer.AnswerID);
            }
        }
    }
}
