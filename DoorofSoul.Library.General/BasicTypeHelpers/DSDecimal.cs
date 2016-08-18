using System.IO;
using MsgPack.Serialization;


namespace DoorofSoul.Library.General.BasicTypeHelpers
{
    public class DSDecimal
    {
        public static object Deserialize(byte[] data)
        {
            var serializer = MessagePackSerializer.Get<DSDecimal>();
            using (MemoryStream ms = new MemoryStream(data))
            {
                DSDecimal result = serializer.Unpack(ms);
                return result;
            }
        }

        public static byte[] Serialize(object data)
        {
            DSDecimal value = (DSDecimal)data;
            var serializer = MessagePackSerializer.Get<DSDecimal>();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.Pack(memoryStream, value);
                return memoryStream.ToArray();
            }
        }

        public decimal value { get; set; }
        public static explicit operator decimal(DSDecimal value)
        {
            return value.value;
        }
    }
}
