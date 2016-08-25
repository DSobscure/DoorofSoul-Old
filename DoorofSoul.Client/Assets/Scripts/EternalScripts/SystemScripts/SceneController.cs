﻿using DoorofSoul.Client.Global;
using DoorofSoul.Client.Interfaces;
using DoorofSoul.Client.Scripts.MindScripts.CameraScripts;
using DoorofSoul.Client.Scripts.NatureScripts.SceneScripts;
using DoorofSoul.Client.Scripts.ShadowScripts.UiScripts.PlayerPanelScripts;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.NatureComponents.EntityElements;
using DoorofSoul.Library.General.NatureComponents.SceneElements;
using DoorofSoul.Protocol;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DoorofSoul.Client.Scripts.EternalScripts.SystemScripts
{
    public class SceneController : MonoBehaviour, IEventProvider
    {
        [SerializeField]
        private PlayerPanel playerPanelPrefab;

        [SerializeField]
        private ViewController viewControllerPrefab;
        private Canvas canvas;
        private DoorofSoul.Library.General.NatureComponents.Scene scene;
        void Awake()
        {
            RegisterEvents();
        }
        void OnDestroy()
        {
            EraseEvents();
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
        void OnSceneLoad(UnityEngine.SceneManagement.Scene unityScene, LoadSceneMode mode)
        {
            if (scene != null)
            {
                scene.OnEntityEnter -= InstantiateEntity;
                scene.OnEntityEnter -= AttachEntity;
                scene.OnEntityExit -= DestroyEntity;
                scene.ItemEntityManager.OnItemEntityChange -= OnItemEntityChange;
            }
            scene = Global.Global.Horizon.MainScene;
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            if (scene != null)
            {
                scene.SceneOperationManager.FetchDataResolver.FetchEntities();
                scene.SceneOperationManager.FetchDataResolver.FetchItemEntities();
                scene.OnEntityEnter += InstantiateEntity;
                scene.OnEntityEnter += AttachEntity;
                scene.OnEntityExit += DestroyEntity;
                scene.ItemEntityManager.OnItemEntityChange += OnItemEntityChange;
                ViewController viewController = Instantiate(viewControllerPrefab);
                viewController.transform.SetParent(GameObject.Find("Controllers").transform);
                foreach (Entity entity in scene.Entities)
                {
                    InstantiateEntity(entity);
                }
                PlayerPanel playerPanel = Instantiate(playerPanelPrefab);
                RectTransform panel = playerPanel.GetComponent<RectTransform>();
                panel.transform.SetParent(canvas.transform);
                panel.localScale = Vector3.one;
                panel.localPosition = Vector3.zero;
                playerPanel.Initial(scene, Global.Global.Seat.MainSoul, Global.Global.Seat.MainContainer);
            }
        }

        private void InstantiateEntity(Entity entity)
        {
            GameObject gameObjectPrefab = Resources.Load<GameObject>("EntityPrefabs/" + entity.EntityName);
            if (gameObjectPrefab != null)
            {
                GameObject entities = GameObject.Find("Entities");
                if (entities == null)
                {
                    SystemManager.ErrorFormat("Scene Not Set Entities Object SceneName: {0}", scene.SceneName);
                }
                else
                {
                    GameObject gameObject = Instantiate(gameObjectPrefab);
                    gameObject.transform.SetParent(entities.transform);
                    gameObject.transform.localPosition = (Vector3)entity.Position;
                    gameObject.transform.localRotation = Quaternion.Euler((Vector3)entity.Rotation);
                    IEntityController entityController = gameObject.GetComponent<IEntityController>();
                    entity.BindEntityController(entityController);
                    if (Global.Global.Seat.MainContainer.EntityID == entity.EntityID)
                    {
                        Camera.main.transform.SetParent(entity.EntityController.GameObject.transform.FindChild("ViewPort"));
                        Camera.main.transform.localPosition = Vector3.zero;
                    }
                }
            }
            else
            {
                SystemManager.ErrorFormat("EntityPrefabs {0} Not Found", entity.EntityName);
            }
        }
        private void AttachEntity(Entity entity)
        {
            if (Global.Global.Seat.MainContainer.EntityID == entity.EntityID)
            {
                Global.Global.Seat.MainContainer.BindEntity(entity);
            }
        }
        private void OnItemEntityChange(ItemEntity itemEntity, DataChangeTypeCode changeTypeCode)
        {
            switch(changeTypeCode)
            {
                case DataChangeTypeCode.Load:
                    {
                        GameObject gameObjectPrefab = Resources.Load<GameObject>("ItemEntityPrefabs/TestItemEntity");
                        if (gameObjectPrefab != null)
                        {
                            GameObject entities = GameObject.Find("Entities");
                            if (entities == null)
                            {
                                SystemManager.ErrorFormat("Scene Not Set Entities Object SceneName: {0}", scene.SceneName);
                            }
                            else
                            {
                                GameObject gameObject = Instantiate(gameObjectPrefab);
                                gameObject.transform.SetParent(entities.transform);
                                ItemEntityController controller = gameObject.GetComponent<ItemEntityController>();
                                controller.Initial(itemEntity);
                            }
                        }
                        else
                        {
                            SystemManager.ErrorFormat("ItemEntityPrefabs Not Found");
                        }
                    }
                    break;
                case DataChangeTypeCode.Unload:
                    {
                        GameObject entities = GameObject.Find("Entities");
                        if (entities == null)
                        {
                            SystemManager.ErrorFormat("Scene Not Set Entities Object SceneName: {0}", scene.SceneName);
                        }
                        else
                        {
                            Destroy(entities.transform.FindChild("ItemEntity" + itemEntity.ItemEntityID));
                        }
                    }
                    break;
            }
        }
    }
}