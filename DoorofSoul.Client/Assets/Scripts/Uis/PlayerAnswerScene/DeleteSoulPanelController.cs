using DoorofSoul.Client.Communication;
using DoorofSoul.Client.HelpFunctions;
using DoorofSoul.Client.Interfaces;
using DoorofSoul.Library.General;
using UnityEngine;
using UnityEngine.UI;

public class DeleteSoulPanelController : MonoBehaviour, IEventProvider
{
    [SerializeField]
    private SoulPanel soulPanelPrefab;
    [SerializeField]
    private RectTransform deleteSoulsPanel;


    void Awake()
    {
        RegisterEvents();
    }
    void Start()
    {
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
        deleteSoulsPanel.ClearChild();
        float blockSize = soulPanelPrefab.GetComponent<RectTransform>().rect.width + 20;
        deleteSoulsPanel.sizeDelta = new Vector2(blockSize * answer.SoulCount, deleteSoulsPanel.rect.height);
        float xOffest = -1 * answer.SoulCount * blockSize / 2f + blockSize / 2;
        int counter = 0;
        foreach (Soul soul in answer.Souls)
        {
            Debug.Log(soul.SoulName);
            SoulPanel soulPanel = Instantiate(soulPanelPrefab);
            soulPanel.transform.SetParent(deleteSoulsPanel);
            RectTransform rect = soulPanel.GetComponent<RectTransform>();
            rect.localScale = Vector3.one;
            rect.localPosition = Vector3.zero;
            rect.localPosition = new Vector2(xOffest + counter * blockSize, 0);
            soulPanel.Show(soul);
            counter++;
        }
    }
}
