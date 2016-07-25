﻿using DoorofSoul.Library.General;
using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Repositories
{
    public abstract class WorldRepository
    {
        public abstract World Create(string worldName);
        public abstract void Delete(int worldID);
        public abstract World Find(int worldID);
        public abstract void Save(World world);
        public abstract List<World> List();
    }
}