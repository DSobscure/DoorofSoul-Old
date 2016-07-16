using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Library.General
{
    public class Entity
    {
        public int EntityID { get; protected set; }
        public string EntityName { get; protected set; }
        public int LocatedSceneID { get; set; }
        public Scene LocatedScene { get; set; }

        protected EntitySpaceProperties spaceProperties;

        public DSVector3 Position
        {
            get { return spaceProperties.position; }
            protected set { spaceProperties.position = value; }
        }
        public DSVector3 Rotation
        {
            get { return spaceProperties.rotation; }
            protected set { spaceProperties.rotation = value; }
        }
        public DSVector3 Scale
        {
            get { return spaceProperties.scale; }
            protected set { spaceProperties.scale = value; }
        }

        public DSVector3 Velocity
        {
            get { return spaceProperties.velocity; }
            protected set { spaceProperties.velocity = value; }
        }
        public DSVector3 MaxVelocity
        {
            get { return spaceProperties.maxVelocity; }
            protected set { spaceProperties.maxVelocity = value; }
        }
        public DSVector3 AngularVelocity
        {
            get { return spaceProperties.angularVelocity; }
            protected set { spaceProperties.angularVelocity = value; }
        }
        public DSVector3 MaxAngularVelocity
        {
            get { return spaceProperties.maxAngularVelocity; }
            protected set { spaceProperties.maxAngularVelocity = value; }
        }
        public float Mass
        {
            get { return spaceProperties.mass; }
            protected set { spaceProperties.mass = value; }
        }

        public Entity(int entityID, string entityName, int locatedSceneID, EntitySpaceProperties spaceProperties)
        {
            EntityID = entityID;
            EntityName = entityName;
            LocatedSceneID = locatedSceneID;
            this.spaceProperties = spaceProperties;
        }
    }
}
