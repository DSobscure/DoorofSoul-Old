﻿using DoorofSoul.Client.Global;
using DoorofSoul.Client.HelpFunctions;
using DoorofSoul.Client.Interfaces;
using DoorofSoul.Library.General;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSoulPanelController : MonoBehaviour, IEventProvider
{
    private Answer answer;

    [SerializeField]
    private SoulActivatePanel soulActivatePanelPrefab;
    [SerializeField]
    private RectTransform deleteSoulsPanel;


    void Awake()
    {
        answer = Global.Player.Answer;
        RegisterEvents();
    }
    void Start()
    {
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
    }

    private void ShowSouls()
    {
        deleteSoulsPanel.ClearChild();
        float blockSize = soulActivatePanelPrefab.GetComponent<RectTransform>().rect.width + 20;
        deleteSoulsPanel.sizeDelta = new Vector2(blockSize * answer.SoulCount, deleteSoulsPanel.rect.height);
        float xOffest = -1 * answer.SoulCount * blockSize / 2f + blockSize / 2;
        int counter = 0;
        foreach (Soul soul in answer.Souls)
        {
            SoulActivatePanel soulActivatePanel = Instantiate(soulActivatePanelPrefab);
            soulActivatePanel.transform.SetParent(deleteSoulsPanel);
            RectTransform rect = soulActivatePanel.GetComponent<RectTransform>();
            rect.localScale = Vector3.one;
            rect.localPosition = Vector3.zero;
            rect.localPosition = new Vector2(xOffest + counter * blockSize, 0);
            soulActivatePanel.Show(soul);
            int soulID = soul.SoulID;
            soulActivatePanel.SetButton("刪除", () => { answer.AnswerOperationManager.DeleteSoul(soulID); });
            counter++;
        }
    }
}
