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
        answer.CreateSoul(createSoulPanel.SoulName);
    }
    public void DeleteSoul(int soulID)
    {
        answer.DeleteSoul(soulID);
    }
    public void ActivateSoul(int soulID)
    {
        answer.ActivateSoul(soulID);
    }
    public void Logout()
    {
        answer.Player.Logout();
    }
}
