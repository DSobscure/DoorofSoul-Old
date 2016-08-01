using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DoorofSoul.Client.Interfaces;
using DoorofSoul.Library.General;
using DoorofSoul.Client.Global;

public class ErrorInformController : MonoBehaviour, IEventProvider
{
    [SerializeField]
    private ErrorInformPanel errorInformPanelPrefab;
    private SystemManager systemManager;

    void Awake()
    {
        systemManager = Global.SystemManager;
        RegisterEvents();
    }
    void OnDestroy()
    {
        EraseEvents();
    }

    public void RegisterEvents()
    {
        systemManager.OnErrorInform += OnErrorInform;
    }

    public void EraseEvents()
    {
        systemManager.OnErrorInform -= OnErrorInform;
    }

    private void OnErrorInform(string title, string errorMessage)
    {
        if(errorMessage != null)
        {
            var panel = Instantiate(errorInformPanelPrefab);
            panel.transform.SetParent(GameObject.Find("Canvas").transform);
            panel.ShowMessage(errorMessage);
        }
        else
        {
            SystemManager.Error("ErrorInformController errorMessage is null");
        }
    }
}
