using System;
using DoorofSoul.Database.DatabaseElements.Repositories;
using DoorofSoul.Database.DatabaseElements.Repositories.LightRepositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.LightRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories
{
    class MySQLLightRepositoryList : LightRepositoryList
    {
        private MySQLEffectsRepositoryList effectsRepositoryList;
        public override EffectsRepositoryList EffectsRepositoryList { get { return effectsRepositoryList; } }

        public MySQLLightRepositoryList()
        {
            effectsRepositoryList = new MySQLEffectsRepositoryList();
        }
    }
}
