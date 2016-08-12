using UnityEngine;
using DoorofSoul.Client.Global;
using UnityEngine.SceneManagement;
using System.Linq;
using DoorofSoul.Library.General;

public class PlayerAnswerController : MonoBehaviour
{
    private Answer answer;

    [SerializeField]
    private CreateSoulPanel createSoulPanel;

    void Awake()
    {
        answer = Global.Player.Answer;
    }
    public void CreateSoul()
    {
        answer.AnswerOperationManager.CreateSoul(createSoulPanel.SoulName, createSoulPanel.MainSoulType);
    }
    public void DeleteSoul(int soulID)
    {
        answer.AnswerOperationManager.DeleteSoul(soulID);
    }
    public void ActivateSoul(int soulID)
    {
        answer.AnswerOperationManager.ActivateSoul(soulID);
    }
    public void Logout()
    {
        answer.Player.PlayerOperationManager.Logout();
    }
}
