﻿using DoorofSoul.Library.General;

namespace DoorofSoul.Database.DatabaseElements.Repositories
{
    public abstract class PlayerRepository
    {
        public abstract bool Contains(string account, out int playerID);
        public abstract Player Find(int playerID);
        public abstract void Save(Player player);
    }
}