using DoorofSoul.Database.DatabaseElements.Repositories.MySQL.Nature.EntityElements;
using DoorofSoul.Database.DatabaseElements.RepositoryList.Nature;

namespace DoorofSoul.Database.DatabaseElements.MySQLManagers.RepositoryList.Nature
{
    public class MySQLEntityElementsList : EntityElementsList
    {
        public MySQLEntityElementsList()
        {
            EntitySpacePropertiesRepository = new MySQLEntitySpacePropertiesRepository();
        }
    }
}
