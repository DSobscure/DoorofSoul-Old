﻿using DoorofSoul.Library.General;
using System.Collections.Generic;

namespace DoorofSoul.Server.DatabaseElements.Repositories
{
    public abstract class SceneRepository
    {
        public abstract Scene Create(string sceneName, int worldID);
        public abstract void Delete(int sceneID); 
        public abstract Scene Find(int sceneID);
        public abstract void Save(Scene scene);
        public abstract List<Scene> List();
        public abstract List<Scene> ListOfWorld(int worldID);
    }
}
