using DoorofSoul.Library.General;
using UnityEngine;
using UnityEngine.UI;
using DoorofSoul.Client.Global;
using DoorofSoul.Client.Interfaces;
using System.Collections;

namespace DoorofSoul.Client.Scripts.NatureScripts.SceneScripts.PlayerLoginSceneScripts
{
    public class PlayerRegisterUI : MonoBehaviour
    {
        [SerializeField]
        private InputField accountInputField;
        [SerializeField]
        private InputField passwordInputField;
        [SerializeField]
        private InputField passwordCheckInputField;
        [SerializeField]
        private Button registerButton;

        public string Account { get { return accountInputField.text; } }
        public string Password { get { return passwordInputField.text; } }
        public string PasswordCheck { get { return passwordCheckInputField.text; } }

        void Awake()
        {
            registerButton.onClick.AddListener(BlockRegisterButton);
        }
        private void BlockRegisterButton()
        {
            registerButton.enabled = false;
            StartCoroutine(UnblockRegisterButton());
        }
        IEnumerator UnblockRegisterButton()
        {
            yield return new WaitForSecondsRealtime(2);
            registerButton.enabled = true;
        }
    }
}
