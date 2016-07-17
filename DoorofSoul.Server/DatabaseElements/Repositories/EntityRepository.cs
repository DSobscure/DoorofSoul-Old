﻿using DoorofSoul.Library.General;
using System.Collections.Generic;

namespace DoorofSoul.Server.DatabaseElements.Repositories
{
    public abstract class EntityRepository
    {
        public abstract Entity Create(string entityName, int locatedSceneID, EntitySpaceProperties spaceProperties);
        public abstract void Delete(int entityID);
        public abstract Entity Find(int entityID);
        public abstract void Save(Entity entity);
        public abstract List<Entity> List();
        public abstract List<Entity> ListInScene(int sceneID);
    }
}
