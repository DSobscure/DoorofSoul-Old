using DoorofSoul.Database.DatabaseElements.Repositories.MindRepositories.SoulElementsRepositories;
using DoorofSoul.Library.General.MindComponents.SoulElements;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.MindRepositories.SoulElementsRepositories
{
    class MySQLSoulPhaseRepository : SoulPhaseRepository
    {
        public override SoulPhase Create(int soulID, SoulPhase phase)
        {
            string sqlString = @"INSERT INTO SoulPhaseCollection 
            (SoulID, PhaseLevel, UnderstandingLevel, UnderstandingPoint) VALUES 
            (@soulID, @phaseLevel, @understandingLevel, @understandingPoint) ;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.MindConnection.SoulElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@soulID", soulID);
                command.Parameters.AddWithValue("@phaseLevel", phase.PhaseLevel);
                command.Parameters.AddWithValue("@understandingLevel", phase.UnderstandingLevel);
                command.Parameters.AddWithValue("@understandingPoint", phase.UnderstandingPoint);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLSoulKernelAbilitiesRepository Create SoulAttributes Error SoulID: {0}", soulID);
                    return null;
                }
                else
                {
                    return Find(soulID, phase.PhaseLevel);
                }
            }
        }

        public override void Delete(int soulID)
        {
            string sqlString = @"DELETE FROM SoulPhaseCollection 
            WHERE SoulID = @soulID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.MindConnection.SoulElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@soulID", soulID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLSoulPhaseRepository Delete SoulPhase Error SoulID: {0}", soulID);
                }
            }
        }

        public override SoulPhase Find(int soulID, byte phaseLevel)
        {
            string sqlString = @"SELECT  
            UnderstandingLevel, UnderstandingPoint
            from SoulPhaseCollection WHERE SoulID = @soulID AND PhaseLevel = @phaseLevel;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.MindConnection.SoulElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("soulID", soulID);
                command.Parameters.AddWithValue("phaseLevel", phaseLevel);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        byte understandingLevel = reader.GetByte(0);
                        int understandingPoint = reader.GetInt32(1);
                        return new SoulPhase(phaseLevel, understandingLevel, understandingPoint);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override List<SoulPhase> ListOfSoul(int soulID)
        {
            string sqlString = @"SELECT  
            PhaseLevel, UnderstandingLevel, UnderstandingPoint
            from SoulPhaseCollection WHERE SoulID = @soulID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.MindConnection.SoulElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("soulID", soulID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<SoulPhase> phases = new List<SoulPhase>();
                    while (reader.Read())
                    {
                        byte phaseLevel = reader.GetByte(0);
                        byte understandingLevel = reader.GetByte(1);
                        int understandingPoint = reader.GetInt32(2);
                        phases.Add(new SoulPhase(phaseLevel, understandingLevel, understandingPoint));
                    }
                    return phases;
                }
            }
        }

        public override void Save(int soulID, SoulPhase phase)
        {
            string sqlString = @"UPDATE SoulPhaseCollection SET
            UnderstandingLevel = @understandingLevel, UnderstandingPoint = @understandingPoint
            WHERE SoulID = @soulID AND PhaseLevel = @phaseLevel;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.MindConnection.SoulElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@understandingLevel", phase.UnderstandingLevel);
                command.Parameters.AddWithValue("@understandingPoint", phase.UnderstandingPoint);
                command.Parameters.AddWithValue("@soulID", soulID);
                command.Parameters.AddWithValue("@phaseLevel", phase.PhaseLevel);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLSoulPhaseRepository Save Phase Error SoulID: {0}, PhaseLevel: {1}", soulID, phase.PhaseLevel);
                }
            }
        }

        public override void SavePhases(int soulID, SoulPhase[] phases)
        {
            foreach(SoulPhase phase in phases)
            {
                Save(soulID, phase);
            }
        }
    }
}
