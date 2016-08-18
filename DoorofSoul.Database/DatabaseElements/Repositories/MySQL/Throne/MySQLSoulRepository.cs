using DoorofSoul.Database.DatabaseElements.Repositories.Throne;
using DoorofSoul.Library.General.ThroneComponents;
using DoorofSoul.Library.General.ThroneComponents.SoulElements;
using DoorofSoul.Protocol;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Repositories.MySQL.Throne
{
    public class MySQLSoulRepository : SoulRepository
    {
        public override Soul Create(Answer answer, string soulName, SoulKernelTypeCode mainSoulType)
        {
            string sqlString = @"INSERT INTO Souls 
                (AnswerID, SoulName) VALUES (@answerID, @soulName) ;
                SELECT LAST_INSERT_ID();";
            int soulID;
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@answerID", answer.AnswerID);
                command.Parameters.AddWithValue("@soulName", soulName);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        soulID = reader.GetInt32(0);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            sqlString = @"INSERT INTO SoulAttributes 
            (SoulID, MainSoulType, MaxReachedPhaseLevel, CorePoint, MaxCorePoint, SpiritPoint, MaxSpiritPoint) VALUES 
            (@soulID, @mainSoulType, @maxReachedPhaseLevel, @corePoint, @maxCorePoint, @spiritPoint, @maxSpiritPoint) ;
            INSERT INTO SoulAttributes_Phases
            (SoulID, PhaseLevel, UnderstandingLevel, UnderstandingPoint) VALUES 
            (@soulID, @phaseLevel, @understandingLevel, @understandingPoint) ;
            INSERT INTO SoulAttributes_KernelAbility
            (SoulID, Creation, Destruction, Conservation, Revolution, Balance, Guidance, Mind) VALUES 
            (@soulID, @creation, @destruction, @conservation, @revolution, @balance, @guidance, @mind) ;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@soulID", soulID);
                command.Parameters.AddWithValue("@mainSoulType", (byte)mainSoulType);
                command.Parameters.AddWithValue("@maxReachedPhaseLevel", 0);
                command.Parameters.AddWithValue("@corePoint", 90);
                command.Parameters.AddWithValue("@maxCorePoint", 90);
                command.Parameters.AddWithValue("@spiritPoint", 50);
                command.Parameters.AddWithValue("@maxSpiritPoint", 50);
                command.Parameters.AddWithValue("@phaseLevel", 0);
                command.Parameters.AddWithValue("@understandingLevel", 1);
                command.Parameters.AddWithValue("@understandingPoint", 0);
                command.Parameters.AddWithValue("@creation", mainSoulType == SoulKernelTypeCode.Creation ? 5 : 1);
                command.Parameters.AddWithValue("@destruction", mainSoulType == SoulKernelTypeCode.Destruction ? 5 : 1);
                command.Parameters.AddWithValue("@conservation", mainSoulType == SoulKernelTypeCode.Conservation ? 5 : 1);
                command.Parameters.AddWithValue("@revolution", mainSoulType == SoulKernelTypeCode.Revolution ? 5 : 1);
                command.Parameters.AddWithValue("@balance", mainSoulType == SoulKernelTypeCode.Balance ? 5 : 1);
                command.Parameters.AddWithValue("@guidance", mainSoulType == SoulKernelTypeCode.Guidance ? 5 : 1);
                command.Parameters.AddWithValue("@mind", mainSoulType == SoulKernelTypeCode.Mind ? 5 : 1);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLSoulRepository Create SoulAttributes Error SoulID: {0}", soulID);
                }
            }
            return Find(soulID, answer);
        }

        public override void Delete(int soulID)
        {
            string sqlString = @"DELETE FROM Souls 
                WHERE SoulID = @soulID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@soulID", soulID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLSoulRepository Delete Soul Error SoulID: {0}", soulID);
                }
            }
        }

        public override Soul Find(int soulID, Answer answer)
        {
            string sqlString = @"SELECT  
                AnswerID, SoulName, 
                MainSoulType, CurrentPhaseLevel, MaxReachedPhaseLevel, CorePoint, MaxCorePoint, SpiritPoint, MaxSpiritPoint,
                Creation, Destruction, Conservation, Revolution, Balance, Guidance, Mind
                from Souls,SoulAttributes,SoulAttributes_KernelAbility WHERE Souls.SoulID = @soulID AND SoulAttributes.SoulID = @soulID AND SoulAttributes_KernelAbility.SoulID = @soulID;";
            Soul soul = null;
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@soulID", soulID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int answerID = reader.GetInt32(0);
                        string soulName = reader.IsDBNull(1) ? "" : reader.GetString(1);

                        SoulKernelTypeCode mainSoulType = (SoulKernelTypeCode)reader.GetByte(2);
                        byte currentPhaseLevel = reader.GetByte(3);
                        byte maxReachedPhaseLevel = reader.GetByte(4);
                        decimal corePoint = reader.GetDecimal(5);
                        decimal maxCorePoint = reader.GetDecimal(6);
                        decimal spiritPoint = reader.GetDecimal(7);
                        decimal maxSpiritPoint = reader.GetDecimal(8);

                        int creation = reader.GetInt32(9);
                        int destruction = reader.GetInt32(10);
                        int conservation = reader.GetInt32(11);
                        int revolution = reader.GetInt32(12);
                        int balance = reader.GetInt32(13);
                        int guidance = reader.GetInt32(14);
                        int mind = reader.GetInt32(15);
                        SoulAttributes attributes = new SoulAttributes(
                            mainSoulType: mainSoulType,
                            currentPhaseLevel: currentPhaseLevel,
                            maxReachedPhaseLevel: maxReachedPhaseLevel,
                            corePoint: corePoint,
                            maxCorePoint: maxCorePoint,
                            spiritPoint: spiritPoint,
                            maxSpiritPoint: maxSpiritPoint,
                            kernelAbility: new SoulKernelAbility(
                                creation: creation,
                                destruction: destruction,
                                conservation: conservation,
                                revolution: revolution,
                                balance: balance,
                                guidance: guidance,
                                mind: mind
                                )
                        );
                        soul = new Soul(soulID, answer, soulName, attributes);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            sqlString = @"SELECT  
            PhaseLevel, UnderstandingLevel, UnderstandingPoint
            from SoulAttributes_Phases WHERE SoulID = @soulID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("soulID", soul.SoulID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        byte phaseLevel = reader.GetByte(0);
                        byte understandingLevel = reader.GetByte(1);
                        int understandingPoint = reader.GetInt32(2);
                        soul.Attributes.LoadPhase(phaseLevel, understandingLevel, understandingPoint);
                    }
                }
            }
            return soul;
        }

        public override List<Soul> ListOfAnswer(Answer answer)
        {
            string sqlString = @"SELECT  
                SoulID
                from Souls Where AnswerID = @answerID;";
            List<int> soulIDs = new List<int>();
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("answerID", answer.AnswerID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int soulID = reader.GetInt32(0);
                        soulIDs.Add(soulID);
                    }
                }
            }
            List<Soul> souls = new List<Soul>();
            foreach(int soulID in soulIDs)
            {
                Soul soul = Find(soulID, answer);
                if(soul != null)
                {
                    souls.Add(soul);
                }
            }
            return souls;
        }

        public override void Save(Soul soul)
        {
            string sqlString = @"UPDATE Souls SET 
                AnswerID = @answerID, SoulName = @soulName
                WHERE SoulID = @soulID;

                UPDATE SoulAttributes SET 
                MainSoulType = @mainSoulType, CurrentPhaseLevel = @currentPhaseLevel, MaxReachedPhaseLevel = @maxReachedPhaseLevel, CorePoint = @corePoint, MaxCorePoint = @maxCorePoint, SpiritPoint = @spiritPoint, MaxSpiritPoint = @maxSpiritPoint
                WHERE SoulID = @soulID;

                UPDATE SoulAttributes_KernelAbility SET 
                Creation = @creation, Destruction = @destruction, Conservation = @conservation, Revolution = @revolution, Balance = @balance, Guidance = @guidance, Mind = @mind
                WHERE SoulID = @soulID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@answerID", soul.AnswerID);
                command.Parameters.AddWithValue("@soulName", soul.SoulName);
                command.Parameters.AddWithValue("@soulID", soul.SoulID);

                command.Parameters.AddWithValue("@mainSoulType", (byte)soul.Attributes.MainSoulType);
                command.Parameters.AddWithValue("@currentPhaseLevel", (byte)soul.Attributes.CurrentPhaseLevel);
                command.Parameters.AddWithValue("@maxReachedPhaseLevel", soul.Attributes.MaxReachedPhaseLevel);
                command.Parameters.AddWithValue("@corePoint", soul.Attributes.CorePoint);
                command.Parameters.AddWithValue("@maxCorePoint", soul.Attributes.MaxCorePoint);
                command.Parameters.AddWithValue("@spiritPoint", soul.Attributes.SpiritPoint);
                command.Parameters.AddWithValue("@maxSpiritPoint", soul.Attributes.MaxSpiritPoint);

                command.Parameters.AddWithValue("@creation", soul.Attributes.KernelAbility.Creation);
                command.Parameters.AddWithValue("@destruction", soul.Attributes.KernelAbility.Destruction);
                command.Parameters.AddWithValue("@conservation", soul.Attributes.KernelAbility.Conservation);
                command.Parameters.AddWithValue("@revolution", soul.Attributes.KernelAbility.Revolution);
                command.Parameters.AddWithValue("@balance", soul.Attributes.KernelAbility.Balance);
                command.Parameters.AddWithValue("@guidance", soul.Attributes.KernelAbility.Guidance);
                command.Parameters.AddWithValue("@mind", soul.Attributes.KernelAbility.Mind);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLSoulRepository Save Soul Error SoulID: {0}", soul.SoulID);
                }
            }
            sqlString = @"UPDATE SoulAttributes_Phases SET
            UnderstandingLevel = @understandingLevel, UnderstandingPoint = @understandingPoint
            WHERE SoulID = @soulID AND PhaseLevel = @phaseLevel;";
            foreach (SoulPhase phase in soul.Attributes.Phases)
            {
                using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
                {
                    command.Parameters.AddWithValue("@understandingLevel", phase.UnderstandingLevel);
                    command.Parameters.AddWithValue("@understandingPoint", phase.UnderstandingPoint);
                    command.Parameters.AddWithValue("@soulID", soul.SoulID);
                    command.Parameters.AddWithValue("@phaseLevel", phase.PhaseLevel);
                    if (command.ExecuteNonQuery() <= 0)
                    {
                        DataBase.Instance.Log.ErrorFormat("MySQLSoulRepository SavePhases Soul Error SoulID: {0}, PhaseLevel: {1}", soul.SoulID, phase.PhaseLevel);
                    }
                }
            }
        }
    }
}
