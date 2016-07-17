﻿using System;
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

        public EntitySpaceProperties SpaceProperties { get; protected set; }

        public DSVector3 Position
        {
            get { return SpaceProperties.position; }
            protected set { SpaceProperties.position = value; }
        }
        public DSVector3 Rotation
        {
            get { return SpaceProperties.rotation; }
            protected set { SpaceProperties.rotation = value; }
        }
        public DSVector3 Scale
        {
            get { return SpaceProperties.scale; }
            protected set { SpaceProperties.scale = value; }
        }

        public DSVector3 Velocity
        {
            get { return SpaceProperties.velocity; }
            protected set { SpaceProperties.velocity = value; }
        }
        public DSVector3 MaxVelocity
        {
            get { return SpaceProperties.maxVelocity; }
            protected set { SpaceProperties.maxVelocity = value; }
        }
        public DSVector3 AngularVelocity
        {
            get { return SpaceProperties.angularVelocity; }
            protected set { SpaceProperties.angularVelocity = value; }
        }
        public DSVector3 MaxAngularVelocity
        {
            get { return SpaceProperties.maxAngularVelocity; }
            protected set { SpaceProperties.maxAngularVelocity = value; }
        }
        public float Mass
        {
            get { return SpaceProperties.mass; }
            protected set { SpaceProperties.mass = value; }
        }

        public Entity(int entityID, string entityName, int locatedSceneID, EntitySpaceProperties spaceProperties)
        {
            EntityID = entityID;
            EntityName = entityName;
            LocatedSceneID = locatedSceneID;
            this.SpaceProperties = spaceProperties;
        }
    }
}
