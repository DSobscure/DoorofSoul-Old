﻿using DoorofSoul.Client.HelpFunctions;
using DoorofSoul.Client.Interfaces;
using DoorofSoul.Client.Library.General;
using DoorofSoul.Library.General.ThroneComponents;
using DoorofSoul.Protocol.Language;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DoorofSoul.Client.Scripts.NatureScripts.SceneScripts.PlayerAnswerSceneScripts
{
    public class SoulsPanelController : MonoBehaviour, IEventProvider
    {
        private Answer answer;
        private Horizon horizon;

        [SerializeField]
        private SoulActivatePanel soulActivatePanelPrefab;
        private RectTransform soulsPanel;
        private Text answerIDText;
        private Text soulCountLimitText;

        void Awake()
        {
            answer = Global.Global.Player.Answer;
            horizon = Global.Global.Horizon;
            RegisterEvents();
        }
        void Start()
        {
            soulsPanel = GameObject.Find("SoulsPanel").GetComponent<RectTransform>();
            answerIDText = GameObject.Find("AnswerIDText").GetComponent<Text>();
            soulCountLimitText = GameObject.Find("SoulCountLimitText").GetComponent<Text>();
            answerIDText.text = answer.AnswerID.ToString();
            soulCountLimitText.text = string.Format("{0}: {1}", LauguageDictionarySelector.Instance[answer.UsingLanguage]["SoulCountLimit"], answer.SoulCountLimit);
            ShowSouls();
        }
        void OnDestroy()
        {
            EraseEvents();
        }

        public void RegisterEvents()
        {
            answer.OnLoadSouls += OnLoadSouls;
        }
        public void EraseEvents()
        {
            answer.OnLoadSouls -= OnLoadSouls;
        }

        private void OnLoadSouls(List<Soul> souls)
        {
            ShowSouls();
            foreach (Soul soul in souls)
            {
                soul.OnSoulActivate += OnSoulActivate;
            }
        }

        private void ShowSouls()
        {
            soulsPanel.ClearChild();
            float blockSize = soulActivatePanelPrefab.GetComponent<RectTransform>().rect.width + 20;
            soulsPanel.sizeDelta = new Vector2(blockSize * answer.SoulCount, soulsPanel.rect.height);
            float xOffest = -1 * answer.SoulCount * blockSize / 2f + blockSize / 2;
            int counter = 0;
            foreach (Soul soul in answer.Souls)
            {
                SoulActivatePanel soulActivatePanel = Instantiate(soulActivatePanelPrefab);
                soulActivatePanel.transform.SetParent(soulsPanel);
                RectTransform rect = soulActivatePanel.GetComponent<RectTransform>();
                rect.localScale = Vector3.one;
                rect.localPosition = new Vector2(xOffest + counter * blockSize, 0);
                soulActivatePanel.Show(soul);
                int soulID = soul.SoulID;
                soulActivatePanel.SetButton("連結", () => { answer.AnswerOperationManager.ActivateSoul(soulID); });
                counter++;
            }
        }
        private void OnSoulActivate(Soul soul)
        {
            if (soul.IsActivate)
            {
                Global.Global.Seat.MainSoul = soul;
                Global.Global.Seat.MainContainer = soul.Containers.First();
                if (soul.Containers.First().Entity.LocatedScene == null)
                {
                    horizon.FetchMainScene(soul.Containers.First().Entity.LocatedSceneID);
                }
                else
                {
                    horizon.ChangeToScene(soul.Containers.First().Entity.LocatedScene);
                }
            }
        }
    }
}