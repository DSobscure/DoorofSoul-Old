using UnityEngine;
using UnityEngine.UI;
using DoorofSoul.Library.General;
using DoorofSoul.Client.Interfaces;
using DoorofSoul.Client.HelpFunctions;
using DoorofSoul.Client.Communication;

public class SoulsPanelController : MonoBehaviour, IEventProvider
{
    [SerializeField]
    private SoulPanel soulPanelPrefab;
    private RectTransform soulsPanel;
    private Text answerIDText;
    private Text soulCountLimitText;

    void Awake()
    {
        RegisterEvents();
    }
    void Start()
    {
        soulsPanel = GameObject.Find("SoulsPanel").GetComponent<RectTransform>();
        answerIDText = GameObject.Find("AnswerIDText").GetComponent<Text>();
        soulCountLimitText = GameObject.Find("SoulCountLimitText").GetComponent<Text>();
        answerIDText.text = Global.Player.Answer.AnswerID.ToString();
        soulCountLimitText.text = string.Format("{0}: {1}", LauguageDictionarySelector.Instance[Global.SystemManagers.UsingLauguage]["SoulCountLimit"], Global.Player.Answer.SoulCountLimit);
        ShowSouls(Global.Player.Answer);
    }
    void OnDestroy()
    {
        EraseEvents();
    }

    public void RegisterEvents()
    {
        Global.Player.Answer.OnLoadSouls += OnLoadSouls;
    }
    public void EraseEvents()
    {
        Global.Player.Answer.OnLoadSouls -= OnLoadSouls;
    }

    private void OnLoadSouls(Answer answer)
    {
        ShowSouls(answer);
    }

    private void ShowSouls(Answer answer)
    {
        soulsPanel.ClearChild();
        float blockSize = soulPanelPrefab.GetComponent<RectTransform>().rect.width + 20;
        soulsPanel.sizeDelta = new Vector2(blockSize * answer.SoulCount, soulsPanel.rect.height);
        float xOffest = -1 * answer.SoulCount * blockSize / 2f + blockSize / 2;
        int counter = 0;
        foreach (Soul soul in answer.Souls)
        {
            Debug.Log(soul.SoulName);
            SoulPanel soulPanel = Instantiate(soulPanelPrefab);
            soulPanel.transform.SetParent(soulsPanel);
            RectTransform rect = soulPanel.GetComponent<RectTransform>();
            rect.localScale = Vector3.one;
            rect.localPosition = Vector3.zero;
            rect.localPosition = new Vector2(xOffest + counter * blockSize, 0);
            soulPanel.Show(soul);
            counter++;
        }
    }
}
