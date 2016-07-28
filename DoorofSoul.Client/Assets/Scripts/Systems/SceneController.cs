using DoorofSoul.Client.Global;
using DoorofSoul.Library.General;
using DoorofSoul.Library.General.IControllers;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    private DoorofSoul.Library.General.Scene scene;
    void OnLevelWasLoaded(int level)
    {
        if(scene != null)
        {
            scene.OnEntityEnter -= InstantiateEntity;
            scene.OnEntityExit -= DestroyEntity;
        }
        scene = Global.Horizon.MainScene;
        if (scene != null)
        {
            scene.FetchEntities();
            scene.OnEntityEnter += InstantiateEntity;
            scene.OnEntityExit += DestroyEntity;
            foreach (Entity entity in scene.Entities)
            {
                InstantiateEntity(entity);
            }
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
}
