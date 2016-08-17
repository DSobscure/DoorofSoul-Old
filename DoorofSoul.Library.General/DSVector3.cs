using MsgPack.Serialization;
using System;
using System.IO;
using UnityEngine;

namespace DoorofSoul.Library.General
{
    public struct DSVector3
    {
        public static object Deserialize(byte[] data)
        {
            var serializer = MessagePackSerializer.Get<DSVector3>();
            using (MemoryStream ms = new MemoryStream(data))
            {
                return serializer.Unpack(ms);
            }
        }

        public static byte[] Serialize(object data)
        {
            DSVector3 value = (DSVector3)data;
            var serializer = MessagePackSerializer.Get<DSVector3>();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.Pack(memoryStream, value);
                return memoryStream.ToArray();
            }
        }

        public float x;
        public float y;
        public float z;

        public static explicit operator Vector3(DSVector3 vector)
        {
            return new Vector3(vector.x, vector.y, vector.z);
        }
        public static DSVector3 Cast(Vector3 vector)
        {
            return new DSVector3 { x = vector.x, y = vector.y, z = vector.z };
        }
    }
}
