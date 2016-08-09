using DoorofSoul.Library.General;
using DoorofSoul.Library.General.ContainerElements;
using DoorofSoul.Library.General.EntityElements;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Repositories.MySQL
{
    public class MySQLContainerRepository : ContainerRepository
    {
        public override Container Create(string entityName, int locatedSceneID, EntitySpaceProperties spaceProperties)
        {
            Entity entity = DataBase.Instance.RepositoryManager.EntityRepository.Create(entityName, locatedSceneID, spaceProperties);
            
            if (entity != null)
            {
                string sqlString = @"INSERT INTO Containers 
                (EntityID) VALUES (@entityID) ;
                SELECT LAST_INSERT_ID();";
                int containerID;
                using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
                {
                    command.Parameters.AddWithValue("entityID", entity.EntityID);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            containerID = reader.GetInt32(0);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                sqlString = @"INSERT INTO ContainerAttributes 
                (ContainerID, Level, MaxLevel, Experience, LifePoint, MaxLifePoint, EnergyPoint, MaxEnergyPoint) VALUES 
                (@containerID, @level, @maxLevel, @experience, @lifePoint, @maxLifePoint, @energyPoint, @maxEnergyPoint) ;
                INSERT INTO ContainerAttributes_KernelAbilityPotential
                (ContainerID, Sensibility, Regeneration, Explosiveness, Strength, Memory, Coordination, Computation, Conductivity) VALUES 
                (@containerID, @sensibility, @regeneration, @explosiveness, @strength, @memory, @coordination, @computation, @conductivity) ;
                INSERT INTO ContainerAttributes_KernelAbility
                (ContainerID, CreationPoint, DestructionPoint, ConservationPoint, RevolutionPoint, BalancePoint, GuidancePoint, MindPoint) VALUES 
                (@containerID, @sensibilityPoint, @regenerationPoint, @explosivenessPoint, @strengthPoint, @memoryPoint, @coordinationPoint, @computationPoint, @conductivityPoint) ;";
                using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
                {
                    command.Parameters.AddWithValue("@containerID", containerID);
                    command.Parameters.AddWithValue("@level", 1);
                    command.Parameters.AddWithValue("@maxLevel", 30);
                    command.Parameters.AddWithValue("@lifePoint", 100);
                    command.Parameters.AddWithValue("@maxLifePoint", 100);
                    command.Parameters.AddWithValue("@energyPoint", 50);
                    command.Parameters.AddWithValue("@maxEnergyPoint", 50);

                    command.Parameters.AddWithValue("@sensibility", 1);
                    command.Parameters.AddWithValue("@regeneration", 1);
                    command.Parameters.AddWithValue("@explosiveness", 1);
                    command.Parameters.AddWithValue("@strength", 1);
                    command.Parameters.AddWithValue("@memory", 1);
                    command.Parameters.AddWithValue("@coordination", 1);
                    command.Parameters.AddWithValue("@computation", 1);
                    command.Parameters.AddWithValue("@conductivity", 1);

                    command.Parameters.AddWithValue("@sensibilityPoint", 3);
                    command.Parameters.AddWithValue("@regenerationPoint", 3);
                    command.Parameters.AddWithValue("@explosivenessPoint", 3);
                    command.Parameters.AddWithValue("@strengthPoint", 3);
                    command.Parameters.AddWithValue("@memoryPoint", 3);
                    command.Parameters.AddWithValue("@coordinationPoint", 3);
                    command.Parameters.AddWithValue("@computationPoint", 3);
                    command.Parameters.AddWithValue("@conductivityPoint", 3);
                    if (command.ExecuteNonQuery() <= 0)
                    {
                        DataBase.Instance.Log.ErrorFormat("MySQLContainerRepository Create ContainerAttributes Error ContainerID: {0}", containerID);
                    }
                }
                Inventory inventory = DataBase.Instance.RepositoryManager.InventoryRepository.Create(containerID, 40);
                return Find(containerID);
            }
            else
            {
                return null;
            }
        }

        public override void Delete(int containerID)
        {
            string sqlString = @"DELETE FROM Containers 
                WHERE ContainerID = @containerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@containerID", containerID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLContainerRepository Delete Container Error ContainerID: {0}", containerID);
                }
            }
        }

        public override Container Find(int containerID)
        {
            string sqlString = @"SELECT  
                EntityID, ContainerName,
                Level, MaxLevel, Experience, LifePoint, MaxLifePoint, EnergyPoint, MaxEnergyPoint,
                Sensibility, Regeneration, Explosiveness, Strength, Memory, Coordination, Computation, Conductivity,
                SensibilityPoint, RegenerationPoint, ExplosivenessPoint, StrengthPoint, MemoryPoint, CoordinationPoint, ComputationPoint, ConductivityPoint
                from Containers,ContainerAttributes,ContainerAttributes_KernelAbilityPotential,ContainerAttributes_KernelAbility WHERE Containers.ContainerID = @containerID AND ContainerAttributes.ContainerID = @containerID AND ContainerAttributes_KernelAbilityPotential.ContainerID = @containerID AND ContainerAttributes_KernelAbility.ContainerID = @containerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@containerID", containerID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int entityID = reader.GetInt32(0);
                        string containerName = reader.IsDBNull(1) ? null : reader.GetString(1);

                        byte level = reader.GetByte(2);
                        byte maxLevel = reader.GetByte(3);
                        int experience = reader.GetInt32(4);
                        decimal lifePoint = reader.GetDecimal(5);
                        decimal maxLifePoint = reader.GetDecimal(6);
                        decimal energyPoint = reader.GetDecimal(7);
                        decimal maxEnergyPoint = reader.GetDecimal(8);

                        int sensibility = reader.GetInt32(9);
                        int regeneration = reader.GetInt32(10);
                        int explosiveness = reader.GetInt32(11);
                        int strength = reader.GetInt32(12);
                        int memory = reader.GetInt32(13);
                        int coordination = reader.GetInt32(14);
                        int computation = reader.GetInt32(15);
                        int conductivity = reader.GetInt32(16);

                        int sensibilityPoint = reader.GetInt32(9);
                        int regenerationPoint = reader.GetInt32(10);
                        int explosivenessPoint = reader.GetInt32(11);
                        int strengthPoint = reader.GetInt32(12);
                        int memoryPoint = reader.GetInt32(13);
                        int coordinationPoint = reader.GetInt32(14);
                        int computationPoint = reader.GetInt32(15);
                        int conductivityPoint = reader.GetInt32(16);

                        ContainerAttributes attributes = new ContainerAttributes(
                            level: level,
                            maxLevel: maxLevel,
                            experience: experience,
                            lifePoint: lifePoint,
                            maxLifePoint: maxLifePoint,
                            energyPoint: energyPoint,
                            maxEnergyPoint: maxEnergyPoint,
                            kernelAbilityPotential: new ContainerKernelAbilityPotential(
                                sensibility: sensibility,
                                regeneration: regeneration,
                                explosiveness: explosiveness,
                                strength: strength,
                                memory: memory,
                                coordination: coordination,
                                computation: computation,
                                conductivity: conductivity
                            ),
                            kernelAbility: new ContainerKernelAbility(
                                sensibilityPoint: sensibilityPoint,
                                regenerationPoint: regenerationPoint,
                                explosivenessPoint: explosivenessPoint,
                                strengthPoint: strengthPoint,
                                memoryPoint: memoryPoint,
                                coordinationPoint: coordinationPoint,
                                computationPoint: computationPoint,
                                conductivityPoint: conductivityPoint
                            )
                        );
                        Entity entity = DataBase.Instance.RepositoryManager.EntityRepository.Find(entityID);
                        Inventory inventory = DataBase.Instance.RepositoryManager.InventoryRepository.FindByContainerID(containerID);
                        Container container = new Container(containerID, entityID, containerName, attributes);
                        container.BindEntity(entity);
                        container.BindInventory(inventory);
                        return container;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override List<Container> List()
        {
            string sqlString = @"SELECT  
                ContainerID
                from Containers;";
            List<int> containerIDs = new List<int>();
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int containerID = reader.GetInt32(0);
                        containerIDs.Add(containerID);
                    }
                }
            }

            List<Container> containers = new List<Container>();
            foreach (int containerID in containerIDs)
            {
                Container container = Find(containerID);
                if(container != null)
                {
                    containers.Add(container);
                }
            }
            return containers;
        }

        public override void Save(Container container)
        {
            string sqlString = @"UPDATE Containers SET 
                EntityID = @entityID
                WHERE ContainerID = @containerID;

                UPDATE ContainerAttributes SET 
                Level = @level, MaxLevel = @maxLevel, Experience = @experience, LifePoint = @lifePoint, MaxLifePoint = @maxLifePoint, EnergyPoint = @energyPoint, MaxEnergyPoint = @maxEnergyPoint
                WHERE ContainerID = @containerID;

                UPDATE ContainerAttributes_KernelAbilityPotential SET 
                Sensibility = @sensibility, Regeneration = @regeneration, Explosiveness = @explosiveness, Strength = @strength, Memory = @memory, Coordination = @coordination, Computation = @computation, Conductivity = @conductivity
                WHERE ContainerID = @containerID;

                UPDATE ContainerAttributes_KernelAbility SET 
                SensibilityPoint = @sensibilityPoint, RegenerationPoint = @regenerationPoint, ExplosivenessPoint = @explosivenessPoint, StrengthPoint = @strengthPoint, MemoryPoint = @memoryPoint, CoordinationPoint = @coordinationPoint, ComputationPoint = @computationPoint, ConductivityPoint = @conductivityPoint
                WHERE ContainerID = @containerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@entityID", container.EntityID);
                command.Parameters.AddWithValue("@containerID", container.ContainerID);

                command.Parameters.AddWithValue("@level", container.Attributes.Level);
                command.Parameters.AddWithValue("@maxLevel", container.Attributes.MaxLevel);
                command.Parameters.AddWithValue("@experience", container.Attributes.Experience);
                command.Parameters.AddWithValue("@lifePoint", container.Attributes.LifePoint);
                command.Parameters.AddWithValue("@maxLifePoint", container.Attributes.MaxLifePoint);
                command.Parameters.AddWithValue("@energyPoint", container.Attributes.EnergyPoint);
                command.Parameters.AddWithValue("@maxEnergyPoint", container.Attributes.MaxEnergyPoint);

                command.Parameters.AddWithValue("@sensibility", container.Attributes.KernelAbilityPotential.Sensibility);
                command.Parameters.AddWithValue("@regeneration", container.Attributes.KernelAbilityPotential.Regeneration);
                command.Parameters.AddWithValue("@explosiveness", container.Attributes.KernelAbilityPotential.Explosiveness);
                command.Parameters.AddWithValue("@strength", container.Attributes.KernelAbilityPotential.Strength);
                command.Parameters.AddWithValue("@memory", container.Attributes.KernelAbilityPotential.Memory);
                command.Parameters.AddWithValue("@coordination", container.Attributes.KernelAbilityPotential.Coordination);
                command.Parameters.AddWithValue("@computation", container.Attributes.KernelAbilityPotential.Computation);
                command.Parameters.AddWithValue("@conductivity", container.Attributes.KernelAbilityPotential.Conductivity);

                command.Parameters.AddWithValue("@sensibilityPoint", container.Attributes.KernelAbility.SensibilityPoint);
                command.Parameters.AddWithValue("@regenerationPoint", container.Attributes.KernelAbility.RegenerationPoint);
                command.Parameters.AddWithValue("@explosivenessPoint", container.Attributes.KernelAbility.ExplosivenessPoint);
                command.Parameters.AddWithValue("@strengthPoint", container.Attributes.KernelAbility.StrengthPoint);
                command.Parameters.AddWithValue("@memoryPoint", container.Attributes.KernelAbility.MemoryPoint);
                command.Parameters.AddWithValue("@coordinationPoint", container.Attributes.KernelAbility.CoordinationPoint);
                command.Parameters.AddWithValue("@computationPoint", container.Attributes.KernelAbility.ComputationPoint);
                command.Parameters.AddWithValue("@conductivityPoint", container.Attributes.KernelAbility.ConductivityPoint);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLContainerRepository Save Container Error ContainerID: {0}", container.ContainerID);
                    DataBase.Instance.RepositoryManager.EntityRepository.Save(container.Entity);
                }
            }
        }
    }
}
