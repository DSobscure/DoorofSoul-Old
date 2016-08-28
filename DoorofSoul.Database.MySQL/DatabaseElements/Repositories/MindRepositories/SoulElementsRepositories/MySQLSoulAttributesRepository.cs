using DoorofSoul.Database.DatabaseElements.Repositories.MindRepositories.SoulElementsRepositories;
using DoorofSoul.Library.General.MindComponents.SoulElements;
using DoorofSoul.Protocol;
using MySql.Data.MySqlClient;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.MindRepositories.SoulElementsRepositories
{
    class MySQLSoulAttributesRepository : SoulAttributesRepository
    {
        public override SoulAttributes Create(int soulID, SoulAttributes soulAttributes)
        {
            string sqlString = @"INSERT INTO SoulAttributesCollection 
            (SoulID, MainSoulType, MaxReachedPhaseLevel, CorePoint, MaxCorePoint, SpiritPoint, MaxSpiritPoint) VALUES 
            (@soulID, @mainSoulType, @maxReachedPhaseLevel, @corePoint, @maxCorePoint, @spiritPoint, @maxSpiritPoint) ;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.MindConnection.SoulElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@soulID", soulID);
                command.Parameters.AddWithValue("@mainSoulType", (byte)soulAttributes.MainSoulType);
                command.Parameters.AddWithValue("@maxReachedPhaseLevel", soulAttributes.MaxReachedPhaseLevel);
                command.Parameters.AddWithValue("@corePoint", soulAttributes.CorePoint);
                command.Parameters.AddWithValue("@maxCorePoint", soulAttributes.MaxCorePoint);
                command.Parameters.AddWithValue("@spiritPoint", soulAttributes.SpiritPoint);
                command.Parameters.AddWithValue("@maxSpiritPoint", soulAttributes.MaxSpiritPoint);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLSoulAttributesRepository Create SoulAttributes Error SoulID: {0}", soulID);
                    return null;
                }
            }
            Database.RepositoryList.MindRepositoryList.SoulElementsRepositoryList.SoulKernelAbilitiesRepository.Create(soulID, soulAttributes.KernelAbilities);
            foreach(SoulPhase phase in soulAttributes.Phases)
            {
                Database.RepositoryList.MindRepositoryList.SoulElementsRepositoryList.SoulPhaseRepository.Create(soulID, phase);
            }
            return Find(soulID);
        }

        public override void Delete(int soulID)
        {
            string sqlString = @"DELETE FROM SoulAttributesCollection 
                WHERE SoulID = @soulID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.MindConnection.SoulElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@soulID", soulID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLSoulAttributesRepository Delete SoulAttributes Error SoulID: {0}", soulID);
                }
            }
        }

        public override SoulAttributes Find(int soulID)
        {
            string sqlString = @"SELECT  
            MainSoulType, CurrentPhaseLevel, MaxReachedPhaseLevel, CorePoint, MaxCorePoint, SpiritPoint, MaxSpiritPoint
            from SoulAttributesCollection WHERE SoulID = @soulID;";
            SoulKernelAbilities kernelAbilities = Database.RepositoryList.MindRepositoryList.SoulElementsRepositoryList.SoulKernelAbilitiesRepository.Find(soulID);
            SoulAttributes attributes = null;
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.MindConnection.SoulElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@soulID", soulID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        SoulKernelTypeCode mainSoulType = (SoulKernelTypeCode)reader.GetByte(0);
                        byte currentPhaseLevel = reader.GetByte(1);
                        byte maxReachedPhaseLevel = reader.GetByte(2);
                        decimal corePoint = reader.GetDecimal(3);
                        decimal maxCorePoint = reader.GetDecimal(4);
                        decimal spiritPoint = reader.GetDecimal(5);
                        decimal maxSpiritPoint = reader.GetDecimal(6);
                        attributes = new SoulAttributes(
                            mainSoulType: mainSoulType,
                            currentPhaseLevel: currentPhaseLevel,
                            maxReachedPhaseLevel: maxReachedPhaseLevel,
                            corePoint: corePoint,
                            maxCorePoint: maxCorePoint,
                            spiritPoint: spiritPoint,
                            maxSpiritPoint: maxSpiritPoint,
                            kernelAbilities: kernelAbilities
                        );
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            attributes.LoadPhases(Database.RepositoryList.MindRepositoryList.SoulElementsRepositoryList.SoulPhaseRepository.ListOfSoul(soulID));
            return attributes;
        }

        public override void Save(int soulID, SoulAttributes soulAttributes)
        {
            string sqlString = @"UPDATE SoulAttributesCollection SET 
            MainSoulType = @mainSoulType, CurrentPhaseLevel = @currentPhaseLevel, MaxReachedPhaseLevel = @maxReachedPhaseLevel, CorePoint = @corePoint, MaxCorePoint = @maxCorePoint, SpiritPoint = @spiritPoint, MaxSpiritPoint = @maxSpiritPoint
            WHERE SoulID = @soulID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.MindConnection.SoulElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@mainSoulType", (byte)soulAttributes.MainSoulType);
                command.Parameters.AddWithValue("@currentPhaseLevel", (byte)soulAttributes.CurrentPhaseLevel);
                command.Parameters.AddWithValue("@maxReachedPhaseLevel", soulAttributes.MaxReachedPhaseLevel);
                command.Parameters.AddWithValue("@corePoint", soulAttributes.CorePoint);
                command.Parameters.AddWithValue("@maxCorePoint", soulAttributes.MaxCorePoint);
                command.Parameters.AddWithValue("@spiritPoint", soulAttributes.SpiritPoint);
                command.Parameters.AddWithValue("@maxSpiritPoint", soulAttributes.MaxSpiritPoint);
                command.Parameters.AddWithValue("@soulID", soulID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLSoulRepository Save Soul Error SoulID: {0}", soulID);
                }
            }
            Database.RepositoryList.MindRepositoryList.SoulElementsRepositoryList.SoulKernelAbilitiesRepository.Save(soulID, soulAttributes.KernelAbilities);
            Database.RepositoryList.MindRepositoryList.SoulElementsRepositoryList.SoulPhaseRepository.SavePhases(soulID, soulAttributes.Phases);
        }
    }
}
