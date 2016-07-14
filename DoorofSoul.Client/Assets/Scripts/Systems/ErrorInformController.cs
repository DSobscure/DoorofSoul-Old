using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DoorofSoul.Client.Interfaces;

public class ErrorInformController : MonoBehaviour, IEventProvider
{
    [SerializeField]
    private ErrorInformPanel errorInformPanel;
    [SerializeField]
    private Canvas canvas;

    void Awake()
    {
        RegisterEvents();
    }
    void OnDestroy()
    {
        EraseEvents();
    }

    public void RegisterEvents()
    {
        Global.SystemManagers.SystemInformManager.OnErrorInform += ErrorInform;
    }

    public void EraseEvents()
    {
        Global.SystemManagers.SystemInformManager.OnErrorInform -= ErrorInform;
    }

    private void ErrorInform(string errorMessage)
    {
        errorInformPanel = Instantiate(errorInformPanel);
        errorInformPanel.transform.SetParent(canvas.transform);
        errorInformPanel.ShowMessage(errorMessage);
    }
}
