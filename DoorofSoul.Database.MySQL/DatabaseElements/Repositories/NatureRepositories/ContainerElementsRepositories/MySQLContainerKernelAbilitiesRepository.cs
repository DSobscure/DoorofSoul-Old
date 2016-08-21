using DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories.ContainerElementsRepositories;
using DoorofSoul.Library.General.NatureComponents.ContainerElements;
using MySql.Data.MySqlClient;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.NatureRepositories.ContainerElementsRepositories
{
    class MySQLContainerKernelAbilitiesRepository : ContainerKernelAbilitiesRepository
    {
        public override ContainerKernelAbilities Create(int containerID, ContainerKernelAbilities kernelAbilities)
        {
            string sqlString = @"INSERT INTO ContainerKernelAbilitiesCollection 
                (ContainerID, SensibilityPoint, RegenerationPoint, ExplosivenessPoint, StrengthPoint, MemoryPoint, CoordinationPoint, ComputationPoint, ConductivityPoint) VALUES 
                (@containerID, @sensibilityPoint, @regenerationPoint, @explosivenessPoint, @strengthPoint, @memoryPoint, @coordinationPoint, @computationPoint, @conductivityPoint) ;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.ContainerElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@containerID", containerID);
                command.Parameters.AddWithValue("@sensibilityPoint", kernelAbilities.SensibilityPoint);
                command.Parameters.AddWithValue("@regenerationPoint", kernelAbilities.RegenerationPoint);
                command.Parameters.AddWithValue("@explosivenessPoint", kernelAbilities.ExplosivenessPoint);
                command.Parameters.AddWithValue("@strengthPoint", kernelAbilities.StrengthPoint);
                command.Parameters.AddWithValue("@memoryPoint", kernelAbilities.MemoryPoint);
                command.Parameters.AddWithValue("@coordinationPoint", kernelAbilities.CoordinationPoint);
                command.Parameters.AddWithValue("@computationPoint", kernelAbilities.ComputationPoint);
                command.Parameters.AddWithValue("@conductivityPoint", kernelAbilities.ConductivityPoint);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLContainerKernelAbilitiesRepository Create ContainerKernelAbilities Error ContainerID: {0}", containerID);
                }
            }
            return Find(containerID);
        }

        public override void Delete(int containerID)
        {
            string sqlString = @"DELETE FROM ContainerKernelAbilitiesCollection 
                WHERE ContainerID = @containerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.ContainerElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@containerID", containerID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLContainerKernelAbilitiesRepository Delete ContainerKernelAbilities Error ContainerID: {0}", containerID);
                }
            }
        }

        public override ContainerKernelAbilities Find(int containerID)
        {
            string sqlString = @"SELECT  
                SensibilityPoint, RegenerationPoint, ExplosivenessPoint, StrengthPoint, MemoryPoint, CoordinationPoint, ComputationPoint, ConductivityPoint
                from ContainerKernelAbilitiesCollection WHERE ContainerID = @containerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.ContainerElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@containerID", containerID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int sensibilityPoint = reader.GetInt32(0);
                        int regenerationPoint = reader.GetInt32(1);
                        int explosivenessPoint = reader.GetInt32(2);
                        int strengthPoint = reader.GetInt32(3);
                        int memoryPoint = reader.GetInt32(4);
                        int coordinationPoint = reader.GetInt32(5);
                        int computationPoint = reader.GetInt32(6);
                        int conductivityPoint = reader.GetInt32(7);
                        return new ContainerKernelAbilities(
                            sensibilityPoint: sensibilityPoint,
                            regenerationPoint: regenerationPoint,
                            explosivenessPoint: explosivenessPoint,
                            strengthPoint: strengthPoint,
                            memoryPoint: memoryPoint,
                            coordinationPoint: coordinationPoint,
                            computationPoint: computationPoint,
                            conductivityPoint: conductivityPoint
                        );
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override void Save(int containerID, ContainerKernelAbilities kernelAbilities)
        {
            string sqlString = @"UPDATE ContainerKernelAbilitiesCollection SET 
                SensibilityPoint = @sensibilityPoint, RegenerationPoint = @regenerationPoint, ExplosivenessPoint = @explosivenessPoint, StrengthPoint = @strengthPoint, MemoryPoint = @memoryPoint, CoordinationPoint = @coordinationPoint, ComputationPoint = @computationPoint, ConductivityPoint = @conductivityPoint
                WHERE ContainerID = @containerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.ContainerElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@sensibilityPoint", kernelAbilities.SensibilityPoint);
                command.Parameters.AddWithValue("@regenerationPoint", kernelAbilities.RegenerationPoint);
                command.Parameters.AddWithValue("@explosivenessPoint", kernelAbilities.ExplosivenessPoint);
                command.Parameters.AddWithValue("@strengthPoint", kernelAbilities.StrengthPoint);
                command.Parameters.AddWithValue("@memoryPoint", kernelAbilities.MemoryPoint);
                command.Parameters.AddWithValue("@coordinationPoint", kernelAbilities.CoordinationPoint);
                command.Parameters.AddWithValue("@computationPoint", kernelAbilities.ComputationPoint);
                command.Parameters.AddWithValue("@conductivityPoint", kernelAbilities.ConductivityPoint);
                command.Parameters.AddWithValue("@containerID", containerID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLContainerKernelAbilitiesRepository Save ContainerKernelAbilities Error ContainerID: {0}", containerID);
                }
            }
        }
    }
}
