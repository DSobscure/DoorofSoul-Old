using System.IO;
using MsgPack.Serialization;

namespace DoorofSoul.Library.General
{
    public class EntitySpaceProperties
    {
        public static object Deserialize(byte[] data)
        {
            var serializer = MessagePackSerializer.Get<EntitySpaceProperties>();
            using (MemoryStream ms = new MemoryStream(data))
            {
                return serializer.Unpack(ms);
            }
        }

        public static byte[] Serialize(object customType)
        {
            EntitySpaceProperties properties = customType as EntitySpaceProperties;
            var serializer = MessagePackSerializer.Get<EntitySpaceProperties>();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.Pack(memoryStream, properties);
                return memoryStream.ToArray();
            }
        }

        public DSVector3 Position { get; set; }
        public DSVector3 Rotation { get; set; }
        public DSVector3 Scale { get; set; }

        public DSVector3 Velocity { get; set; }
        public DSVector3 MaxVelocity { get; set; }
        public DSVector3 AngularVelocity { get; set; }
        public DSVector3 MaxAngularVelocity { get; set; }
        public float Mass { get; set; }
    }
}
