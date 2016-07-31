using UnityEngine;
using System.Collections;
using DoorofSoul.Library.General;
using DoorofSoul.Library.General.IControllers;

public class EntityRotateTest : MonoBehaviour
{
    [SerializeField]
    ViewMaskTest maskTest;
    Entity entity;
    ViewController vc;
	// Use this for initialization
	void Start ()
    {
        entity = new Entity(1, "TestEntity", -1, new EntitySpaceProperties
        {
            velocity = new DSVector3 { x = 0, y = 0, z =0 },
            angularVelocity = new DSVector3 { x = 0, y = 0, z = 0 },
        });
        GameObject gameObjectPrefab = Resources.Load<GameObject>("EntityPrefabs/" + entity.EntityName);
        GameObject entities = GameObject.Find("Entities");
        GameObject gameObject = Instantiate(gameObjectPrefab);
        gameObject.transform.SetParent(entities.transform);
        gameObject.transform.position = (Vector3)entity.Position;
        gameObject.transform.rotation = Quaternion.Euler((Vector3)entity.Rotation);
        IEntityController entityController = gameObject.GetComponent<IEntityController>();
        entity.BindEntityController(entityController);
        Camera.main.transform.SetParent(entity.EntityController.GameObject.transform);
        Camera.main.transform.localPosition = Vector3.zero;
        Camera.main.transform.localRotation = Quaternion.identity;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            (entity.EntityController as EntityController).StartRotate(-1);
            maskTest.Rotate(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            (entity.EntityController as EntityController).StartRotate(1);
            maskTest.Rotate(1);
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            (entity.EntityController as EntityController).StartRotate(0);
            maskTest.Rotate(0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            (entity.EntityController as EntityController).StartMove(1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            (entity.EntityController as EntityController).StartMove(-1);
        }
        else
        {
            (entity.EntityController as EntityController).StartMove(0);
        }
    }
}
