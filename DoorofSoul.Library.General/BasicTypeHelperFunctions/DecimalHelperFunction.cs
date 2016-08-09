using System.IO;
using MsgPack.Serialization;


namespace DoorofSoul.Library.General.BasicTypeHelperFunctions
{
    public static class DecimalHelperFunction
    {
        public static object Deserialize(byte[] data)
        {
            var serializer = MessagePackSerializer.Get<decimal>();
            using (MemoryStream ms = new MemoryStream(data))
            {
                return serializer.Unpack(ms);
            }
        }

        public static byte[] Serialize(object data)
        {
            decimal value = (decimal)data;
            var serializer = MessagePackSerializer.Get<decimal>();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.Pack(memoryStream, value);
                return memoryStream.ToArray();
            }
        }
    }
}
