using DoorofSoul.Library.General;
using DoorofSoul.Library.General.EntityElements;
using System;

namespace DoorofSoul.Library.Authority
{
    public class SceneEye
    {
        public int Level { get; protected set; }
        protected Scene scene;
        protected Action<int> seeEntityAction;
        public SceneEye(int level, Action<int> seeEntityAction)
        {
            Level = level;
            this.seeEntityAction = seeEntityAction;
        }
        public void ObserveScene(Scene scene)
        {
            this.scene = scene;
        }
        public void See(int entityID)
        {
            seeEntityAction(entityID);
        }
        public void Saw(int entityID, EntitySpaceProperties spaceProperties)
        {
            scene.UpdateEntitySpaceProperties(entityID, spaceProperties);
        }
    }
}
