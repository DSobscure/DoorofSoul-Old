using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Library.General.SceneElements
{
    public abstract class SceneEye
    {
        public int ObserverLevel { get; protected set; }
        public Scene Scene { get; protected set; }
        public Container Observer { get; protected set; }

        public void BindScene(Scene scene)
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

        public abstract void StartMonitot(int cycleTimeMilliseconds);
        public abstract void StopMonotot();

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
