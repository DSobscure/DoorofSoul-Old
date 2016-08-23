using MsgPack.Serialization;
using System;
using System.IO;

namespace DoorofSoul.Library.General.KnowledgeComponents.StatusEffects
{
    public class ContainerStatusEffectInfo
    {
        public static object Deserialize(byte[] data)
        {
            var serializer = MessagePackSerializer.Get<ContainerStatusEffectInfo>();
            using (MemoryStream ms = new MemoryStream(data))
            {
                return serializer.Unpack(ms);
            }
        }

        public static byte[] Serialize(object data)
        {
            ContainerStatusEffectInfo value = data as ContainerStatusEffectInfo;
            var serializer = MessagePackSerializer.Get<ContainerStatusEffectInfo>();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.Pack(memoryStream, value);
                return memoryStream.ToArray();
            }
        }

        public int ContainerStatusEffectInfoID { get; protected set; }
        public int AffectedContainerID { get; protected set; }
        public StatusEffect StatusEffect { get; protected set; }
        public float EffectDuration { get; protected set; }
        public DateTime ExpirationTime { get; protected set; }

        public ContainerStatusEffectInfo() { }
        public ContainerStatusEffectInfo(int containerStatusEffectInfoID, int affectedContainerID, StatusEffect statusEffect, float effectDuration, DateTime expirationTime)
        {
            ContainerStatusEffectInfoID = containerStatusEffectInfoID;
            AffectedContainerID = affectedContainerID;
            StatusEffect = statusEffect;
            EffectDuration = effectDuration;
            ExpirationTime = expirationTime;
        }
    }
}
