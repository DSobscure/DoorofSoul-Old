using DoorofSoul.Client.Interfaces;
using DoorofSoul.Library.General;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IEventProvider
{
    void Awake()
    {
        RegisterEvents();
    }   
    void Start()
    {
        Global.OperationManagers.FetchDataOperationManager.FetchAnswer();
    }
    void OnDestroy()
    {
        EraseEvents();
    }

    public void EraseEvents()
    {
        Global.Player.OnActiveAnswer -= OnActiveAnswer;
    }

    public void RegisterEvents()
    {
        Global.Player.OnActiveAnswer += OnActiveAnswer;
    }

    private void OnActiveAnswer(Answer answer)
    {
        SceneManager.LoadScene("PlayerAnswerScene");
    }
}
