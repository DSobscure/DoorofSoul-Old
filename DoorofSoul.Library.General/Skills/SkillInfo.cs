using MsgPack.Serialization;
using System.IO;
using DoorofSoul.Protocol;

namespace DoorofSoul.Library.General.Skills
{
    public class SkillInfo
    {
        public static object Deserialize(byte[] data)
        {
            var serializer = MessagePackSerializer.Get<SkillInfo>();
            using (MemoryStream ms = new MemoryStream(data))
            {
                return serializer.Unpack(ms);
            }
        }

        public static byte[] Serialize(object data)
        {
            SkillInfo value = data as SkillInfo;
            var serializer = MessagePackSerializer.Get<SkillInfo>();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.Pack(memoryStream, value);
                return memoryStream.ToArray();
            }
        }

        public int SkillInfoID { get; protected set; }
        public int UnderstanderSoulID { get; protected set; }
        public Skill Skill { get; protected set; }
        public byte SkillLevel { get; protected set; }
        public SkillPitch SkillPitch { get; protected set; }

        public SkillInfo() { }
        public SkillInfo(int skillInfoID, int understanderSoulID, Skill skill, byte skillLevel, SkillPitch skillPitch)
        {
            SkillInfoID = skillInfoID;
            UnderstanderSoulID = understanderSoulID;
            Skill = skill;
            SkillLevel = skillLevel;
            SkillPitch = skillPitch;
        }
    }
}
