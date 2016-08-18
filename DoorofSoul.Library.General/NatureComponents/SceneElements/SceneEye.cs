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
            Observer.ContainerEventManager.ObserveSceneEntitiesPosition();
        }
        public void UpdateEntityPosition(int entityID, DSVector3 position)
        {
            if(Scene.ContainsEntity(entityID))
            {
                Scene.FindEntity(entityID).Position = position;
            }
        }
    }
}
