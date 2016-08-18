using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Library.General.NatureComponents.EntityElements;
using DoorofSoul.Library.General.NatureComponents;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Repositories.MySQL
{
    public class MySQLEntityRepository : EntityRepository
    {
        public override Entity Create(string entityName, int locatedSceneID, EntitySpaceProperties spaceProperties)
        {
            string sqlString = @"INSERT INTO Entities 
                (EntityName, LocatedSceneID,
                PositionX, PositionY, PositionZ,
                RotationX, RotationY, RotationZ,
                ScaleX, ScaleY, ScaleZ,
                VelocityX, VelocityY, VelocityZ,
                MaxVelocityX, MaxVelocityY, MaxVelocityZ,
                AngularVelocityX, AngularVelocityY, AngularVelocityZ,
                MaxAngularVelocityX, MaxAngularVelocityY, MaxAngularVelocityZ,
                Mass) 
                VALUES (@entityName, @locatedSceneID,
                @positionX, @positionY, @positionZ,
                @rotationX, @rotationY, @rotationZ,
                @scaleX, @scaleY, @scaleZ,
                @velocityX, @velocityY, @velocityZ,
                @maxVelocityX, @maxVelocityY, @maxVelocityZ,
                @angularVelocityX, @angularVelocityY, @angularVelocityZ,
                @maxAngularVelocityX, @maxAngularVelocityY, @maxAngularVelocityZ,
                @mass) ;
                SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@entityName", entityName);
                command.Parameters.AddWithValue("@locatedSceneID", locatedSceneID);
                command.Parameters.AddWithValue("@positionX", spaceProperties.Position.x);
                command.Parameters.AddWithValue("@positionY", spaceProperties.Position.y);
                command.Parameters.AddWithValue("@positionZ", spaceProperties.Position.z);
                command.Parameters.AddWithValue("@rotationX", spaceProperties.Rotation.x);
                command.Parameters.AddWithValue("@rotationY", spaceProperties.Rotation.y);
                command.Parameters.AddWithValue("@rotationZ", spaceProperties.Rotation.z);
                command.Parameters.AddWithValue("@scaleX", spaceProperties.Scale.x);
                command.Parameters.AddWithValue("@scaleY", spaceProperties.Scale.y);
                command.Parameters.AddWithValue("@scaleZ", spaceProperties.Scale.z);
                command.Parameters.AddWithValue("@velocityX", spaceProperties.Velocity.x);
                command.Parameters.AddWithValue("@velocityY", spaceProperties.Velocity.y);
                command.Parameters.AddWithValue("@velocityZ", spaceProperties.Velocity.z);
                command.Parameters.AddWithValue("@maxVelocityX", spaceProperties.MaxVelocity.x);
                command.Parameters.AddWithValue("@maxVelocityY", spaceProperties.MaxVelocity.y);
                command.Parameters.AddWithValue("@maxVelocityZ", spaceProperties.MaxVelocity.z);
                command.Parameters.AddWithValue("@angularVelocityX", spaceProperties.AngularVelocity.x);
                command.Parameters.AddWithValue("@angularVelocityY", spaceProperties.AngularVelocity.y);
                command.Parameters.AddWithValue("@angularVelocityZ", spaceProperties.AngularVelocity.z);
                command.Parameters.AddWithValue("@maxAngularVelocityX", spaceProperties.MaxAngularVelocity.x);
                command.Parameters.AddWithValue("@maxAngularVelocityY", spaceProperties.MaxAngularVelocity.y);
                command.Parameters.AddWithValue("@maxAngularVelocityZ", spaceProperties.MaxAngularVelocity.z);
                command.Parameters.AddWithValue("@mass", spaceProperties.Mass);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int entityID = reader.GetInt32(0);
                        return Find(entityID);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override void Delete(int entityID)
        {
            string sqlString = @"DELETE FROM Entities 
                WHERE EntityID = @entityID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@entityID", entityID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLEntityRepository Delete Entity Error EntityID: {0}", entityID);
                }
            }
        }

        public override Entity Find(int entityID)
        {
            string sqlString = @"SELECT  
                EntityName, LocatedSceneID,
                PositionX, PositionY, PositionZ,
                RotationX, RotationY, RotationZ,
                ScaleX, ScaleY, ScaleZ,
                VelocityX, VelocityY, VelocityZ,
                MaxVelocityX, MaxVelocityY, MaxVelocityZ,
                AngularVelocityX, AngularVelocityY, AngularVelocityZ,
                MaxAngularVelocityX, MaxAngularVelocityY, MaxAngularVelocityZ,
                Mass
                from Entities WHERE EntityID = @entityID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@entityID", entityID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string entityName = reader.IsDBNull(0) ? "" : reader.GetString(0);
                        int locatedSceneID = reader.IsDBNull(1) ? -1 : reader.GetInt32(1);
                        float positionX = reader.GetFloat(2);
                        float positionY = reader.GetFloat(3);
                        float positionZ = reader.GetFloat(4);
                        float rotationX = reader.GetFloat(5);
                        float rotationY = reader.GetFloat(6);
                        float rotationZ = reader.GetFloat(7);
                        float scaleX = reader.GetFloat(8);
                        float scaleY = reader.GetFloat(9);
                        float scaleZ = reader.GetFloat(10);
                        float velocityX = reader.GetFloat(11);
                        float velocityY = reader.GetFloat(12);
                        float velocityZ = reader.GetFloat(13);
                        float maxVelocityX = reader.GetFloat(14);
                        float maxVelocityY = reader.GetFloat(15);
                        float maxVelocityZ = reader.GetFloat(16);
                        float angularVelocityX = reader.GetFloat(17);
                        float angularVelocityY = reader.GetFloat(18);
                        float angularVelocityZ = reader.GetFloat(19);
                        float maxAngularVelocityX = reader.GetFloat(20);
                        float maxAngularVelocityY = reader.GetFloat(21);
                        float maxAngularVelocityZ = reader.GetFloat(22);
                        float mass = reader.GetFloat(23);

                        return new Entity(entityID, entityName, locatedSceneID, new EntitySpaceProperties
                        {
                            Position = new DSVector3 { x = positionX, y = positionY, z = positionZ },
                            Rotation = new DSVector3 { x = rotationX, y = rotationY, z = rotationZ },
                            Scale = new DSVector3 { x = scaleX, y = scaleY, z = scaleZ },
                            Velocity = new DSVector3 { x = velocityX, y = velocityY, z = velocityZ },
                            MaxVelocity = new DSVector3 { x = maxVelocityX, y = maxVelocityY, z = maxVelocityZ },
                            AngularVelocity = new DSVector3 { x = angularVelocityX, y = angularVelocityY, z = angularVelocityZ },
                            MaxAngularVelocity = new DSVector3 { x = maxAngularVelocityX, y = maxAngularVelocityY, z = maxAngularVelocityZ },
                            Mass = mass
                        });
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override List<Entity> List()
        {
            string sqlString = @"SELECT  
                EntityID, EntityName, LocatedSceneID,
                PositionX, PositionY, PositionZ,
                RotationX, RotationY, RotationZ,
                ScaleX, ScaleY, ScaleZ,
                VelocityX, VelocityY, VelocityZ,
                MaxVelocityX, MaxVelocityY, MaxVelocityZ,
                AngularVelocityX, AngularVelocityY, AngularVelocityZ,
                MaxAngularVelocityX, MaxAngularVelocityY, MaxAngularVelocityZ,
                Mass
                from Entities;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<Entity> entities = new List<Entity>();
                    while (reader.Read())
                    {
                        int entityID = reader.GetInt32(0);
                        string entityName = reader.IsDBNull(1) ? "" : reader.GetString(1);
                        int locatedSceneID = reader.IsDBNull(2) ? -1 : reader.GetInt32(2);
                        float positionX = reader.GetFloat(3);
                        float positionY = reader.GetFloat(4);
                        float positionZ = reader.GetFloat(5);
                        float rotationX = reader.GetFloat(6);
                        float rotationY = reader.GetFloat(7);
                        float rotationZ = reader.GetFloat(8);
                        float scaleX = reader.GetFloat(9);
                        float scaleY = reader.GetFloat(10);
                        float scaleZ = reader.GetFloat(11);
                        float velocityX = reader.GetFloat(12);
                        float velocityY = reader.GetFloat(13);
                        float velocityZ = reader.GetFloat(14);
                        float maxVelocityX = reader.GetFloat(15);
                        float maxVelocityY = reader.GetFloat(16);
                        float maxVelocityZ = reader.GetFloat(17);
                        float angularVelocityX = reader.GetFloat(18);
                        float angularVelocityY = reader.GetFloat(19);
                        float angularVelocityZ = reader.GetFloat(20);
                        float maxAngularVelocityX = reader.GetFloat(21);
                        float maxAngularVelocityY = reader.GetFloat(22);
                        float maxAngularVelocityZ = reader.GetFloat(23);
                        float mass = reader.GetFloat(24);
                        entities.Add(new Entity(entityID, entityName, locatedSceneID, new EntitySpaceProperties
                        {
                            Position = new DSVector3 { x = positionX, y = positionY, z = positionZ },
                            Rotation = new DSVector3 { x = rotationX, y = rotationY, z = rotationZ },
                            Scale = new DSVector3 { x = scaleX, y = scaleY, z = scaleZ },
                            Velocity = new DSVector3 { x = velocityX, y = velocityY, z = velocityZ },
                            MaxVelocity = new DSVector3 { x = maxVelocityX, y = maxVelocityY, z = maxVelocityZ },
                            AngularVelocity = new DSVector3 { x = angularVelocityX, y = angularVelocityY, z = angularVelocityZ },
                            MaxAngularVelocity = new DSVector3 { x = maxAngularVelocityX, y = maxAngularVelocityY, z = maxAngularVelocityZ },
                            Mass = mass
                        }));
                    }
                    return entities;
                }
            }
        }

        public override List<Entity> ListInScene(int sceneID)
        {
            string sqlString = @"SELECT  
                EntityID, EntityName, LocatedSceneID,
                PositionX, PositionY, PositionZ,
                RotationX, RotationY, RotationZ,
                ScaleX, ScaleY, ScaleZ,
                VelocityX, VelocityY, VelocityZ,
                MaxVelocityX, MaxVelocityY, MaxVelocityZ,
                AngularVelocityX, AngularVelocityY, AngularVelocityZ,
                MaxAngularVelocityX, MaxAngularVelocityY, MaxAngularVelocityZ,
                Mass
                from Entities WHERE LocatedSceneID = @sceneID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("sceneID", sceneID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<Entity> entities = new List<Entity>();
                    while (reader.Read())
                    {
                        int entityID = reader.GetInt32(0);
                        string entityName = reader.IsDBNull(1) ? "" : reader.GetString(1);
                        int locatedSceneID = reader.IsDBNull(2) ? -1 : reader.GetInt32(2);
                        float positionX = reader.GetFloat(3);
                        float positionY = reader.GetFloat(4);
                        float positionZ = reader.GetFloat(5);
                        float rotationX = reader.GetFloat(6);
                        float rotationY = reader.GetFloat(7);
                        float rotationZ = reader.GetFloat(8);
                        float scaleX = reader.GetFloat(9);
                        float scaleY = reader.GetFloat(10);
                        float scaleZ = reader.GetFloat(11);
                        float velocityX = reader.GetFloat(12);
                        float velocityY = reader.GetFloat(13);
                        float velocityZ = reader.GetFloat(14);
                        float maxVelocityX = reader.GetFloat(15);
                        float maxVelocityY = reader.GetFloat(16);
                        float maxVelocityZ = reader.GetFloat(17);
                        float angularVelocityX = reader.GetFloat(18);
                        float angularVelocityY = reader.GetFloat(19);
                        float angularVelocityZ = reader.GetFloat(20);
                        float maxAngularVelocityX = reader.GetFloat(21);
                        float maxAngularVelocityY = reader.GetFloat(22);
                        float maxAngularVelocityZ = reader.GetFloat(23);
                        float mass = reader.GetFloat(24);
                        entities.Add(new Entity(entityID, entityName, locatedSceneID, new EntitySpaceProperties
                        {
                            Position = new DSVector3 { x = positionX, y = positionY, z = positionZ },
                            Rotation = new DSVector3 { x = rotationX, y = rotationY, z = rotationZ },
                            Scale = new DSVector3 { x = scaleX, y = scaleY, z = scaleZ },
                            Velocity = new DSVector3 { x = velocityX, y = velocityY, z = velocityZ },
                            MaxVelocity = new DSVector3 { x = maxVelocityX, y = maxVelocityY, z = maxVelocityZ },
                            AngularVelocity = new DSVector3 { x = angularVelocityX, y = angularVelocityY, z = angularVelocityZ },
                            MaxAngularVelocity = new DSVector3 { x = maxAngularVelocityX, y = maxAngularVelocityY, z = maxAngularVelocityZ },
                            Mass = mass
                        }));
                    }
                    return entities;
                }
            }
        }

        public override void Save(Entity entity)
        {
            string sqlString = @"UPDATE Entities SET 
                EntityName = @entityName, LocatedSceneID = @locatedSceneID,
                PositionX = @positionX, PositionY = @positionY, PositionZ = @positionZ,
                RotationX = @rotationX, RotationY = @rotationY, RotationZ = @rotationZ,
                ScaleX = @scaleX, ScaleY = @scaleY, ScaleZ = @scaleZ,
                VelocityX = @velocityX, VelocityY = @velocityY, VelocityZ = @velocityZ,
                MaxVelocityX = @maxVelocityX, MaxVelocityY = @maxVelocityY, MaxVelocityZ = @maxVelocityZ,
                AngularVelocityX = @angularVelocityX, AngularVelocityY = @angularVelocityY, AngularVelocityZ = @angularVelocityZ,
                MaxAngularVelocityX = @maxAngularVelocityX, MaxAngularVelocityY = @maxAngularVelocityY, MaxAngularVelocityZ = @maxAngularVelocityZ,
                Mass = @mass
                WHERE EntityID = @entityID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@entityName", entity.EntityName);
                command.Parameters.AddWithValue("@locatedSceneID", entity.LocatedSceneID);
                command.Parameters.AddWithValue("@positionX", entity.Position.x);
                command.Parameters.AddWithValue("@positionY", entity.Position.y);
                command.Parameters.AddWithValue("@positionZ", entity.Position.z);
                command.Parameters.AddWithValue("@rotationX", entity.Rotation.x);
                command.Parameters.AddWithValue("@rotationY", entity.Rotation.y);
                command.Parameters.AddWithValue("@rotationZ", entity.Rotation.z);
                command.Parameters.AddWithValue("@scaleX", entity.Scale.x);
                command.Parameters.AddWithValue("@scaleY", entity.Scale.y);
                command.Parameters.AddWithValue("@scaleZ", entity.Scale.z);
                command.Parameters.AddWithValue("@velocityX", entity.Velocity.x);
                command.Parameters.AddWithValue("@velocityY", entity.Velocity.y);
                command.Parameters.AddWithValue("@velocityZ", entity.Velocity.z);
                command.Parameters.AddWithValue("@maxVelocityX", entity.MaxVelocity.x);
                command.Parameters.AddWithValue("@maxVelocityY", entity.MaxVelocity.y);
                command.Parameters.AddWithValue("@maxVelocityZ", entity.MaxVelocity.z);
                command.Parameters.AddWithValue("@angularVelocityX", entity.AngularVelocity.x);
                command.Parameters.AddWithValue("@angularVelocityY", entity.AngularVelocity.y);
                command.Parameters.AddWithValue("@angularVelocityZ", entity.AngularVelocity.z);
                command.Parameters.AddWithValue("@maxAngularVelocityX", entity.MaxAngularVelocity.x);
                command.Parameters.AddWithValue("@maxAngularVelocityY", entity.MaxAngularVelocity.y);
                command.Parameters.AddWithValue("@maxAngularVelocityZ", entity.MaxAngularVelocity.z);
                command.Parameters.AddWithValue("@mass", entity.Mass);
                command.Parameters.AddWithValue("@entityID", entity.EntityID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLEntityRepository Save Entity Error EntityID: {0}", entity.EntityID);
                }
            }
        }
    }
}
