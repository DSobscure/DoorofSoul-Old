using DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories.ContainerElementsRepositories;
using DoorofSoul.Library.General.NatureComponents.ContainerElements;
using MySql.Data.MySqlClient;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.NatureRepositories.ContainerElementsRepositories
{
    class MySQLContainerAttributesRepository : ContainerAttributesRepository
    {
        public override ContainerAttributes Create(int containerID, ContainerAttributes containerAttributes)
        {
            string sqlString = @"INSERT INTO ContainerAttributesCollection 
            (ContainerID, Level, MaxLevel, Experience, LifePoint, MaxLifePoint, EnergyPoint, MaxEnergyPoint) VALUES 
                (@containerID, @level, @maxLevel, @experience, @lifePoint, @maxLifePoint, @energyPoint, @maxEnergyPoint) ;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.ContainerElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@containerID", containerID);
                command.Parameters.AddWithValue("@level", containerAttributes.Level);
                command.Parameters.AddWithValue("@maxLevel", containerAttributes.MaxLevel);
                command.Parameters.AddWithValue("@experience", containerAttributes.Experience);
                command.Parameters.AddWithValue("@lifePoint", containerAttributes.LifePoint);
                command.Parameters.AddWithValue("@maxLifePoint", containerAttributes.MaxLifePoint);
                command.Parameters.AddWithValue("@energyPoint", containerAttributes.EnergyPoint);
                command.Parameters.AddWithValue("@maxEnergyPoint", containerAttributes.MaxEnergyPoint);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLContainerAttributesRepository Create ContainerAttributes Error ContainerID: {0}", containerID);
                }
            }
            Database.RepositoryList.NatureRepositoryList.ContainerElementsRepositoryList.ContainerKernelAbilitiesPotentialRepository.Create(containerID, containerAttributes.KernelAbilitiesPotential);
            Database.RepositoryList.NatureRepositoryList.ContainerElementsRepositoryList.ContainerKernelAbilitiesRepository.Create(containerID, containerAttributes.KernelAbilities);
            return Find(containerID);
        }

        public override void Delete(int containerID)
        {
            string sqlString = @"DELETE FROM ContainerAttributesCollection 
            WHERE ContainerID = @containerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.ContainerElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@containerID", containerID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLContainerAttributesRepository Delete ContainerAttributes Error ContainerID: {0}", containerID);
                }
            }
            Database.RepositoryList.NatureRepositoryList.ContainerElementsRepositoryList.ContainerKernelAbilitiesPotentialRepository.Delete(containerID);
            Database.RepositoryList.NatureRepositoryList.ContainerElementsRepositoryList.ContainerKernelAbilitiesRepository.Delete(containerID);
        }

        public override ContainerAttributes Find(int containerID)
        {
            string sqlString = @"SELECT  
            Level, MaxLevel, Experience, LifePoint, MaxLifePoint, EnergyPoint, MaxEnergyPoint
            from ContainerAttributesCollection WHERE ContainerID = @containerID;";
            ContainerKernelAbilitiesPotential kernelAbilitiesPotential = Database.RepositoryList.NatureRepositoryList.ContainerElementsRepositoryList.ContainerKernelAbilitiesPotentialRepository.Find(containerID);
            ContainerKernelAbilities kernelAbilities = Database.RepositoryList.NatureRepositoryList.ContainerElementsRepositoryList.ContainerKernelAbilitiesRepository.Find(containerID);
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.ContainerElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@containerID", containerID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        byte level = reader.GetByte(0);
                        byte maxLevel = reader.GetByte(1);
                        int experience = reader.GetInt32(2);
                        decimal lifePoint = reader.GetDecimal(3);
                        decimal maxLifePoint = reader.GetDecimal(4);
                        decimal energyPoint = reader.GetDecimal(5);
                        decimal maxEnergyPoint = reader.GetDecimal(6);
                        return new ContainerAttributes(
                            level: level,
                            maxLevel: maxLevel,
                            experience: experience,
                            lifePoint: lifePoint,
                            maxLifePoint: maxLifePoint,
                            energyPoint: energyPoint,
                            maxEnergyPoint: maxEnergyPoint,
                            kernelAbilitiesPotential: kernelAbilitiesPotential,
                            kernelAbilities: kernelAbilities
                        );
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override void Save(int containerID, ContainerAttributes containerAttributes)
        {
            string sqlString = @"UPDATE ContainerAttributesCollection SET 
                Level = @level, MaxLevel = @maxLevel, Experience = @experience, LifePoint = @lifePoint, MaxLifePoint = @maxLifePoint, EnergyPoint = @energyPoint, MaxEnergyPoint = @maxEnergyPoint
                WHERE ContainerID = @containerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.ContainerElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@level", containerAttributes.Level);
                command.Parameters.AddWithValue("@maxLevel", containerAttributes.MaxLevel);
                command.Parameters.AddWithValue("@experience", containerAttributes.Experience);
                command.Parameters.AddWithValue("@lifePoint", containerAttributes.LifePoint);
                command.Parameters.AddWithValue("@maxLifePoint", containerAttributes.MaxLifePoint);
                command.Parameters.AddWithValue("@energyPoint", containerAttributes.EnergyPoint);
                command.Parameters.AddWithValue("@maxEnergyPoint", containerAttributes.MaxEnergyPoint);
                command.Parameters.AddWithValue("@containerID", containerID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLContainerAttributesRepository Save ContainerAttributes Error ContainerID: {0}", containerID);
                }
            }
            Database.RepositoryList.NatureRepositoryList.ContainerElementsRepositoryList.ContainerKernelAbilitiesPotentialRepository.Save(containerID, containerAttributes.KernelAbilitiesPotential);
            Database.RepositoryList.NatureRepositoryList.ContainerElementsRepositoryList.ContainerKernelAbilitiesRepository.Save(containerID, containerAttributes.KernelAbilities);
        }
    }
}
