using DoorofSoul.Hexagram.ThroneComponents;
using DoorofSoul.Library.General;
using DoorofSoul.Library.General.ThroneComponents;

namespace DoorofSoul.Hexagram
{
    public class Throne
    {
        public AnswerManager AnswerManager { get; protected set; }
        

        public Throne()
        {
            AnswerManager = new AnswerManager();
        }
        public void Initial()
        {
            AnswerManager.Initial();
        }

        public void ProjectPlayer(Player player)
        {
            Answer answer = Database.Database.RepositoryList.ThroneRepositoryList.AnswerRepository.Find(player.AnswerID, player);
            if(answer != null)
            {
                AnswerManager.ProjectAnswer(answer);
            }
        }

        public void ExtractPlayer(Player player)
        {
            if (player.Answer != null)
            {
                Database.Database.RepositoryList.PlayerRepository.Save(player);
                AnswerManager.ExtractAnswer(player.Answer);
            }
        }
    }
}
