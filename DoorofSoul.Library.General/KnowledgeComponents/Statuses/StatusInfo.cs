using MsgPack.Serialization;
using System.IO;

namespace DoorofSoul.Library.General.KnowledgeComponents.Statuses

{
    public class StatusInfo
    {
        public static object Deserialize(byte[] data)
        {
            var serializer = MessagePackSerializer.Get<StatusInfo>();
            using (MemoryStream ms = new MemoryStream(data))
            {
                return serializer.Unpack(ms);
            }
        }

        public static byte[] Serialize(object data)
        {
            StatusInfo value = data as StatusInfo;
            var serializer = MessagePackSerializer.Get<StatusInfo>();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.Pack(memoryStream, value);
                return memoryStream.ToArray();
            }
        }

        public int StatusInfoID { get; protected set; }
        public Status Status { get; protected set; }

        public StatusInfo() { }
        public StatusInfo(int statusInfoID, Status status)
        {
            StatusInfoID = statusInfoID;
            Status = status;
        }
    }
}
