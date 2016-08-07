using UnityEngine;
using UnityEngine.UI;
using DoorofSoul.Library.General;
using DoorofSoul.Client.Interfaces;
using DoorofSoul.Client.HelpFunctions;
using DoorofSoul.Client.Global;
using DoorofSoul.Protocol.Language;
using System.Collections.Generic;
using System.Linq;
using DoorofSoul.Client.Library.General;

public class SoulsPanelController : MonoBehaviour, IEventProvider
{
    private Answer answer;
    private Horizon horizon;

    [SerializeField]
    private SoulPanel soulPanelPrefab;
    private RectTransform soulsPanel;
    private Text answerIDText;
    private Text soulCountLimitText;

    void Awake()
    {
        answer = Global.Player.Answer;
        horizon = Global.Horizon;
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
        foreach(Soul soul in souls)
        {
            soul.OnSoulActivate += OnSoulActivate;
        }
    }

    private void ShowSouls()
    {
        soulsPanel.ClearChild();
        float blockSize = soulPanelPrefab.GetComponent<RectTransform>().rect.width + 20;
        soulsPanel.sizeDelta = new Vector2(blockSize * answer.SoulCount, soulsPanel.rect.height);
        float xOffest = -1 * answer.SoulCount * blockSize / 2f + blockSize / 2;
        int counter = 0;
        foreach (Soul soul in answer.Souls)
        {
            SoulPanel soulPanel = Instantiate(soulPanelPrefab);
            soulPanel.transform.SetParent(soulsPanel);
            RectTransform rect = soulPanel.GetComponent<RectTransform>();
            rect.localScale = Vector3.one;
            rect.localPosition = new Vector2(xOffest + counter * blockSize, 0);
            soulPanel.Show(soul);
            int soulID = soul.SoulID;
            soulPanel.SetButton("連結", () => { answer.AnswerOperationManager.ActivateSoul(soulID); });
            counter++;
        }
    }
    private void OnSoulActivate(Soul soul)
    {
        if(soul.IsActivate)
        {
            Global.Seat.MainSoul = soul;
            Global.Seat.MainContainer = soul.Containers.First();
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
