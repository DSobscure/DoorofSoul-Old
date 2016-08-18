using DoorofSoul.Library.General.ThroneComponents;
using UnityEngine;

namespace DoorofSoul.Client.Scripts.SceneScripts.PlayerAnswerSceneScripts
{
    public class PlayerAnswerController : MonoBehaviour
    {
        private Answer answer;

        [SerializeField]
        private CreateSoulPanel createSoulPanel;

        void Awake()
        {
            answer = Global.Global.Player.Answer;
        }
        public void CreateSoul()
        {
            answer.AnswerOperationManager.CreateSoul(createSoulPanel.SoulName, createSoulPanel.MainSoulType);
        }
        public void DeleteSoul(int soulID)
        {
            answer.AnswerOperationManager.DeleteSoul(soulID);
        }
        public void ActivateSoul(int soulID)
        {
            answer.AnswerOperationManager.ActivateSoul(soulID);
        }
        public void Logout()
        {
            answer.Player.PlayerOperationManager.Logout();
        }
    }
}
