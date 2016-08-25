﻿using DoorofSoul.Client.Interfaces;
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
    }
}