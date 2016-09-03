using DoorofSoul.Library.General.ElementComponents;

namespace DoorofSoul.Library.General.NatureComponents.SceneElements
{
    public abstract class SceneEye
    {
        public int ObserverLevel { get; protected set; }
        public Scene Scene { get; protected set; }
        public Container Observer { get; protected set; }

        public virtual void BindScene(Scene scene)
        {
            Scene = scene;
        }

        public bool SetObserver(int observerLevel, Container observer)
        {
            if(observerLevel > ObserverLevel)
            {
                ObserverLevel = observerLevel;
                Observer = observer;
                return true;
            }
            else
            {
                return false;
            }
        }

        public abstract void StartMonitor(int cycleTimeMilliseconds);
        public abstract void StopMonitor();

        public void Monitor()
        {
            Observer.ContainerEventManager.ObserveSceneEntitiesTransform();
        }
        public void UpdateEntityTransform(int entityID, DSVector3 position, DSVector3 rotation)
        {
            if(Scene.ContainsEntity(entityID))
            {
                Entity entity = Scene.FindEntity(entityID);
                entity.Position = position;
                entity.Rotation = rotation;
            }
        }
    }
}
