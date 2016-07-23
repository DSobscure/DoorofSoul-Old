using UnityEngine;
using UnityEngine.SceneManagement;
using DoorofSoul.Library.General;
using DoorofSoul.Client.Library.General;
using DoorofSoul.Client.Interfaces;

public class SceneController : MonoBehaviour
{
    void Awake()
    {
        Global.ScenesManager.BindFunctions(SceneManager.LoadScene, InstantiateEntity, DestroyEntity);
    }

    private void InstantiateEntity(Entity entity)
    {
        GameObject gameObjectPrefab = Resources.Load<GameObject>("EntityPrefabs/" + entity.EntityName);
        if(gameObjectPrefab != null)
        {
            GameObject entities = GameObject.Find("Entities");
            if(entities == null)
            {
                Global.SystemManagers.DebugInformManager.DebugInform(string.Format("Scene Not Set Entities Object"));
            }
            else
            {
                GameObject gameObject = Instantiate(gameObjectPrefab);
                gameObject.transform.SetParent(entities.transform);
                gameObject.transform.position = (Vector3)entity.Position;
                gameObject.transform.rotation = Quaternion.Euler((Vector3)entity.Rotation);
                IEntityController entityController = gameObject.GetComponent<IEntityController>();
                ClientEntity clientEntity = entity as ClientEntity;
                clientEntity.BindEntityController(entityController);
                entityController.BindEntity(clientEntity);
            }
        }
        else
        {
            Global.SystemManagers.DebugInformManager.DebugInform(string.Format("EntityPrefabs {0} Not Found", entity.EntityName));
        }
    }
    private void DestroyEntity(Entity entity)
    {
        Destroy((entity as ClientEntity).GameObject);
    }
}
