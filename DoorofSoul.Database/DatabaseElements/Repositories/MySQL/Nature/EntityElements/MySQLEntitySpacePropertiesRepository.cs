using DoorofSoul.Database.DatabaseElements.Repositories.Nature.EntityElements;
using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Library.General.NatureComponents.EntityElements;
using MySql.Data.MySqlClient;

namespace DoorofSoul.Database.DatabaseElements.Repositories.MySQL.Nature.EntityElements
{
    public class MySQLEntitySpacePropertiesRepository : EntitySpacePropertiesRepository
    {
        public override EntitySpaceProperties Create(int entityID, EntitySpaceProperties spaceProperties)
        {
            string sqlString = @"INSERT INTO Nature_EntityElements_EntitySpaceProperties
                (EntityID,
                PositionX, PositionY, PositionZ,
                RotationX, RotationY, RotationZ,
                ScaleX, ScaleY, ScaleZ,
                VelocityX, VelocityY, VelocityZ,
                MaxVelocityX, MaxVelocityY, MaxVelocityZ,
                AngularVelocityX, AngularVelocityY, AngularVelocityZ,
                MaxAngularVelocityX, MaxAngularVelocityY, MaxAngularVelocityZ,
                Mass) 
                VALUES (@entityID,
                @positionX, @positionY, @positionZ,
                @rotationX, @rotationY, @rotationZ,
                @scaleX, @scaleY, @scaleZ,
                @velocityX, @velocityY, @velocityZ,
                @maxVelocityX, @maxVelocityY, @maxVelocityZ,
                @angularVelocityX, @angularVelocityY, @angularVelocityZ,
                @maxAngularVelocityX, @maxAngularVelocityY, @maxAngularVelocityZ,
                @mass) ;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@entityID", entityID);
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
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLEntitySpacePropertiesRepository Create EntitySpaceProperties Error EntityID: {0}", entityID);
                    return null;
                }
                else
                {
                    return Find(entityID);
                }
            }
        }

        public override void Delete(int entityID)
        {
            string sqlString = @"DELETE FROM Nature_EntityElements_EntitySpaceProperties 
                WHERE EntityID = @entityID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@entityID", entityID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLEntitySpacePropertiesRepository Delete EntitySpaceProperties Error EntityID: {0}", entityID);
                }
            }
        }

        public override EntitySpaceProperties Find(int entityID)
        {
            string sqlString = @"SELECT  
                PositionX, PositionY, PositionZ,
                RotationX, RotationY, RotationZ,
                ScaleX, ScaleY, ScaleZ,
                VelocityX, VelocityY, VelocityZ,
                MaxVelocityX, MaxVelocityY, MaxVelocityZ,
                AngularVelocityX, AngularVelocityY, AngularVelocityZ,
                MaxAngularVelocityX, MaxAngularVelocityY, MaxAngularVelocityZ,
                Mass
                from Nature_EntityElements_EntitySpaceProperties WHERE EntityID = @entityID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@entityID", entityID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        float positionX = reader.GetFloat(0);
                        float positionY = reader.GetFloat(1);
                        float positionZ = reader.GetFloat(2);
                        float rotationX = reader.GetFloat(3);
                        float rotationY = reader.GetFloat(4);
                        float rotationZ = reader.GetFloat(5);
                        float scaleX = reader.GetFloat(6);
                        float scaleY = reader.GetFloat(7);
                        float scaleZ = reader.GetFloat(8);
                        float velocityX = reader.GetFloat(9);
                        float velocityY = reader.GetFloat(10);
                        float velocityZ = reader.GetFloat(11);
                        float maxVelocityX = reader.GetFloat(12);
                        float maxVelocityY = reader.GetFloat(13);
                        float maxVelocityZ = reader.GetFloat(14);
                        float angularVelocityX = reader.GetFloat(15);
                        float angularVelocityY = reader.GetFloat(16);
                        float angularVelocityZ = reader.GetFloat(17);
                        float maxAngularVelocityX = reader.GetFloat(18);
                        float maxAngularVelocityY = reader.GetFloat(19);
                        float maxAngularVelocityZ = reader.GetFloat(20);
                        float mass = reader.GetFloat(21);

                        return new EntitySpaceProperties
                        {
                            Position = new DSVector3 { x = positionX, y = positionY, z = positionZ },
                            Rotation = new DSVector3 { x = rotationX, y = rotationY, z = rotationZ },
                            Scale = new DSVector3 { x = scaleX, y = scaleY, z = scaleZ },
                            Velocity = new DSVector3 { x = velocityX, y = velocityY, z = velocityZ },
                            MaxVelocity = new DSVector3 { x = maxVelocityX, y = maxVelocityY, z = maxVelocityZ },
                            AngularVelocity = new DSVector3 { x = angularVelocityX, y = angularVelocityY, z = angularVelocityZ },
                            MaxAngularVelocity = new DSVector3 { x = maxAngularVelocityX, y = maxAngularVelocityY, z = maxAngularVelocityZ },
                            Mass = mass
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override void Save(int entityID, EntitySpaceProperties entitySpaceProperties)
        {
            string sqlString = @"UPDATE Nature_EntityElements_EntitySpaceProperties SET 
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
                command.Parameters.AddWithValue("@positionX", entitySpaceProperties.Position.x);
                command.Parameters.AddWithValue("@positionY", entitySpaceProperties.Position.y);
                command.Parameters.AddWithValue("@positionZ", entitySpaceProperties.Position.z);
                command.Parameters.AddWithValue("@rotationX", entitySpaceProperties.Rotation.x);
                command.Parameters.AddWithValue("@rotationY", entitySpaceProperties.Rotation.y);
                command.Parameters.AddWithValue("@rotationZ", entitySpaceProperties.Rotation.z);
                command.Parameters.AddWithValue("@scaleX", entitySpaceProperties.Scale.x);
                command.Parameters.AddWithValue("@scaleY", entitySpaceProperties.Scale.y);
                command.Parameters.AddWithValue("@scaleZ", entitySpaceProperties.Scale.z);
                command.Parameters.AddWithValue("@velocityX", entitySpaceProperties.Velocity.x);
                command.Parameters.AddWithValue("@velocityY", entitySpaceProperties.Velocity.y);
                command.Parameters.AddWithValue("@velocityZ", entitySpaceProperties.Velocity.z);
                command.Parameters.AddWithValue("@maxVelocityX", entitySpaceProperties.MaxVelocity.x);
                command.Parameters.AddWithValue("@maxVelocityY", entitySpaceProperties.MaxVelocity.y);
                command.Parameters.AddWithValue("@maxVelocityZ", entitySpaceProperties.MaxVelocity.z);
                command.Parameters.AddWithValue("@angularVelocityX", entitySpaceProperties.AngularVelocity.x);
                command.Parameters.AddWithValue("@angularVelocityY", entitySpaceProperties.AngularVelocity.y);
                command.Parameters.AddWithValue("@angularVelocityZ", entitySpaceProperties.AngularVelocity.z);
                command.Parameters.AddWithValue("@maxAngularVelocityX", entitySpaceProperties.MaxAngularVelocity.x);
                command.Parameters.AddWithValue("@maxAngularVelocityY", entitySpaceProperties.MaxAngularVelocity.y);
                command.Parameters.AddWithValue("@maxAngularVelocityZ", entitySpaceProperties.MaxAngularVelocity.z);
                command.Parameters.AddWithValue("@mass", entitySpaceProperties.Mass);
                command.Parameters.AddWithValue("@entityID", entityID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLEntitySpacePropertiesRepository Save EntitySpaceProperties Error EntityID: {0}", entityID);
                }
            }
        }
    }
}
