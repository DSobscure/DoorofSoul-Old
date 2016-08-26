using DoorofSoul.Client.Interfaces;
using DoorofSoul.Library.General;
using DoorofSoul.Library.General.ThroneComponents;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DoorofSoul.Client.Scripts.NatureScripts.SceneScripts.PlayerLoginSceneScripts
{
    public class PlayerLoginController : MonoBehaviour, IEventProvider
    {
        [SerializeField]
        private PlayerLoginUI playerLoginUI;
        [SerializeField]
        private PlayerRegisterUI playerRegisterUI;

        private DoorofSoul.Library.General.Player player;

        void Awake()
        {
            player = Global.Global.Player;
            RegisterEvents();
        }
        void OnDestroy()
        {
            EraseEvents();
        }

        public void RegisterEvents()
        {
            player.OnActiveAnswer += OnActiveAnswer;
        }

        public void EraseEvents()
        {
            player.OnActiveAnswer -= OnActiveAnswer;
        }
        public void PlayerLogin()
        {
            player.PlayerOperationManager.Login(playerLoginUI.Account, playerLoginUI.Password);
            playerLoginUI.PlayerLogin();
        }
        public void OnActiveAnswer(Answer answer)
        {
            Global.Global.Seat.MainAnswer = answer;
            SceneManager.LoadScene("PlayerAnswerScene");
        }
        public void PlayerRegister()
        {
            if(playerRegisterUI.Account != null && playerRegisterUI.Account.Length >= 2)
            {
                if(playerRegisterUI.Password != null && playerRegisterUI.Password.Length >= 2)
                {
                    if(playerRegisterUI.PasswordCheck != null && playerRegisterUI.Password == playerRegisterUI.PasswordCheck)
                    {
                        player.PlayerOperationManager.Register(playerRegisterUI.Account, playerRegisterUI.Password);
                    }
                    else
                    {
                        player.PlayerEventManager.ErrorInform("錯誤", "密碼與確認密碼不同");
                    }
                }
                else
                {
                    player.PlayerEventManager.ErrorInform("錯誤", "密碼太短");
                }
            }
            else
            {
                player.PlayerEventManager.ErrorInform("錯誤","帳號太短");
            }
        }
    }
}
