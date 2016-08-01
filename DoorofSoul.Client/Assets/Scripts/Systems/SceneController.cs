using System;
using DoorofSoul.Client.Global;
using DoorofSoul.Client.Interfaces;
using DoorofSoul.Library.General;
using DoorofSoul.Library.General.IControllers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour, IEventProvider
{
    [SerializeField]
    private PlayerPanel playerPanelPrefab;
    private Canvas canvas;
    private DoorofSoul.Library.General.Scene scene;
    void Awake()
    {
        RegisterEvents();
    }
    void OnDestroy()
    {
        EraseEvents();
    }
    void OnSceneLoad(UnityEngine.SceneManagement.Scene unityScene, LoadSceneMode mode)
    {
        if (scene != null)
        {
            scene.OnEntityEnter -= InstantiateEntity;
            scene.OnEntityExit -= DestroyEntity;
        }
        scene = Global.Horizon.MainScene;
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        if (scene != null)
        {
            scene.SceneOperationManager.FetchEntities();
            scene.OnEntityEnter += InstantiateEntity;
            scene.OnEntityExit += DestroyEntity;
            foreach (Entity entity in scene.Entities)
            {
                InstantiateEntity(entity);
            }
            RectTransform playerPanel = Instantiate(playerPanelPrefab).GetComponent<RectTransform>();
            playerPanel.transform.SetParent(canvas.transform);
            playerPanel.localScale = Vector3.one;
            playerPanel.localPosition = Vector3.zero;
        }
    }

    private void InstantiateEntity(Entity entity)
    {
        GameObject gameObjectPrefab = Resources.Load<GameObject>("EntityPrefabs/" + entity.EntityName);
        if(gameObjectPrefab != null)
        {
            GameObject entities = GameObject.Find("Entities");
            if(entities == null)
            {
                SystemManager.ErrorFormat("Scene Not Set Entities Object SceneName: {0}", scene.SceneName);
            }
            else
            {
                GameObject gameObject = Instantiate(gameObjectPrefab);
                gameObject.transform.SetParent(entities.transform);
                gameObject.transform.position = (Vector3)entity.Position;
                gameObject.transform.rotation = Quaternion.Euler((Vector3)entity.Rotation);
                IEntityController entityController = gameObject.GetComponent<IEntityController>();
                entity.BindEntityController(entityController);
            }
        }
        else
        {
            SystemManager.ErrorFormat("EntityPrefabs {0} Not Found", entity.EntityName);
        }
    }
    private void DestroyEntity(Entity entity)
    {
        Destroy(entity.EntityController.GameObject);
    }

    public void RegisterEvents()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    public void EraseEvents()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
}
