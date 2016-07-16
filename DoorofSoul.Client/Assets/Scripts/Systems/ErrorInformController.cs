using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DoorofSoul.Client.Interfaces;

public class ErrorInformController : MonoBehaviour, IEventProvider
{
    [SerializeField]
    private ErrorInformPanel errorInformPanelPrefab;

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
        var panel = Instantiate(errorInformPanelPrefab);
        panel.transform.SetParent(GameObject.Find("Canvas").transform);
        panel.ShowMessage(errorMessage);
    }
}
