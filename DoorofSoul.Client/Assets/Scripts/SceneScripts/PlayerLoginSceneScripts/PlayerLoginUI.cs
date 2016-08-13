using DoorofSoul.Library.General;
using UnityEngine;
using UnityEngine.UI;
using DoorofSoul.Client.Global;
using DoorofSoul.Client.Interfaces;

namespace DoorofSoul.Client.Scripts.SceneScripts.PlayerLoginSceneScripts
{
    public class PlayerLoginUI : MonoBehaviour, IEventProvider
    {
        private Player player;

        [SerializeField]
        private InputField accountInputField;
        [SerializeField]
        private InputField passwordInputField;
        [SerializeField]
        private Button playerLoginButton;

        public string Account { get { return accountInputField.text; } }
        public string Password { get { return passwordInputField.text; } }

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
            player.OnOnline += OnLoginResponse;
        }

        public void EraseEvents()
        {
            player.OnOnline -= OnLoginResponse;
        }
        public void PlayerLogin()
        {
            accountInputField.enabled = false;
            passwordInputField.enabled = false;
            playerLoginButton.enabled = false;
        }
        public void OnLoginResponse(Player player)
        {
            if (!player.IsOnline)
            {
                accountInputField.enabled = true;
                passwordInputField.enabled = true;
                playerLoginButton.enabled = true;
            }
        }
    }
}
