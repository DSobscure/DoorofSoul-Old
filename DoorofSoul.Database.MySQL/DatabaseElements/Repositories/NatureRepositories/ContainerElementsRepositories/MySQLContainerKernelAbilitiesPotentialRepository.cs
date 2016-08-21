using DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories.ContainerElementsRepositories;
using DoorofSoul.Library.General.NatureComponents.ContainerElements;
using MySql.Data.MySqlClient;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.NatureRepositories.ContainerElementsRepositories
{
    class MySQLContainerKernelAbilitiesPotentialRepository : ContainerKernelAbilitiesPotentialRepository
    {
        public override ContainerKernelAbilitiesPotential Create(int containerID, ContainerKernelAbilitiesPotential kernelAbilitiesPotential)
        {
            string sqlString = @"INSERT INTO ContainerKernelAbilitiesPotentialCollection 
            (ContainerID, Sensibility, Regeneration, Explosiveness, Strength, Memory, Coordination, Computation, Conductivity) VALUES 
            (@containerID, @sensibility, @regeneration, @explosiveness, @strength, @memory, @coordination, @computation, @conductivity) ;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.ContainerElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@containerID", containerID);
                command.Parameters.AddWithValue("@sensibility", kernelAbilitiesPotential.Sensibility);
                command.Parameters.AddWithValue("@regeneration", kernelAbilitiesPotential.Regeneration);
                command.Parameters.AddWithValue("@explosiveness", kernelAbilitiesPotential.Explosiveness);
                command.Parameters.AddWithValue("@strength", kernelAbilitiesPotential.Strength);
                command.Parameters.AddWithValue("@memory", kernelAbilitiesPotential.Memory);
                command.Parameters.AddWithValue("@coordination", kernelAbilitiesPotential.Coordination);
                command.Parameters.AddWithValue("@computation", kernelAbilitiesPotential.Computation);
                command.Parameters.AddWithValue("@conductivity", kernelAbilitiesPotential.Conductivity);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLContainerKernelAbilitiesPotentialRepository Create ContainerKernelAbilitiesPotential Error ContainerID: {0}", containerID);
                }
            }
            return Find(containerID);
        }

        public override void Delete(int containerID)
        {
            string sqlString = @"DELETE FROM ContainerKernelAbilitiesPotentialCollection 
            WHERE ContainerID = @containerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.ContainerElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@containerID", containerID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLContainerKernelAbilitiesPotentialRepository Delete ContainerKernelAbilitiesPotential Error ContainerID: {0}", containerID);
                }
            }
        }

        public override ContainerKernelAbilitiesPotential Find(int containerID)
        {
            string sqlString = @"SELECT  
                Sensibility, Regeneration, Explosiveness, Strength, Memory, Coordination, Computation, Conductivity
                from ContainerKernelAbilitiesPotentialCollection WHERE ContainerID = @containerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.ContainerElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@containerID", containerID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int sensibility = reader.GetInt32(0);
                        int regeneration = reader.GetInt32(1);
                        int explosiveness = reader.GetInt32(2);
                        int strength = reader.GetInt32(3);
                        int memory = reader.GetInt32(4);
                        int coordination = reader.GetInt32(5);
                        int computation = reader.GetInt32(6);
                        int conductivity = reader.GetInt32(7);
                        return new ContainerKernelAbilitiesPotential(
                            sensibility: sensibility,
                            regeneration: regeneration,
                            explosiveness: explosiveness,
                            strength: strength,
                            memory: memory,
                            coordination: coordination,
                            computation: computation,
                            conductivity: conductivity
                        );
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override void Save(int containerID, ContainerKernelAbilitiesPotential kernelAbilitiesPotential)
        {
            string sqlString = @"UPDATE ContainerKernelAbilitiesPotentialCollection SET 
                Sensibility = @sensibility, Regeneration = @regeneration, Explosiveness = @explosiveness, Strength = @strength, Memory = @memory, Coordination = @coordination, Computation = @computation, Conductivity = @conductivity
                WHERE ContainerID = @containerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.ContainerElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@sensibility", kernelAbilitiesPotential.Sensibility);
                command.Parameters.AddWithValue("@regeneration", kernelAbilitiesPotential.Regeneration);
                command.Parameters.AddWithValue("@explosiveness", kernelAbilitiesPotential.Explosiveness);
                command.Parameters.AddWithValue("@strength", kernelAbilitiesPotential.Strength);
                command.Parameters.AddWithValue("@memory", kernelAbilitiesPotential.Memory);
                command.Parameters.AddWithValue("@coordination", kernelAbilitiesPotential.Coordination);
                command.Parameters.AddWithValue("@computation", kernelAbilitiesPotential.Computation);
                command.Parameters.AddWithValue("@conductivity", kernelAbilitiesPotential.Conductivity);
                command.Parameters.AddWithValue("@containerID", containerID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLContainerKernelAbilitiesPotentialRepository Save ContainerKernelAbilitiesPotential Error ContainerID: {0}", containerID);
                }
            }
        }
    }
}
