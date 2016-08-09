using DoorofSoul.Protocol;
using MsgPack.Serialization;
using System.IO;

namespace DoorofSoul.Library.General.SoulElements
{
    public class SoulAttributes
    {
        public static object Deserialize(byte[] data)
        {
            var serializer = MessagePackSerializer.Get<SoulAttributes>();
            using (MemoryStream ms = new MemoryStream(data))
            {
                return serializer.Unpack(ms);
            }
        }

        public static byte[] Serialize(object data)
        {
            SoulAttributes value = data as SoulAttributes;
            var serializer = MessagePackSerializer.Get<SoulAttributes>();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.Pack(memoryStream, value);
                return memoryStream.ToArray();
            }
        }

        public SoulKernelType MainSoulType { get; protected set; }
        public SoulPhase[] Phases { get; protected set; }
        public byte MaxReachedPhaseLevel { get; protected set; }
        public decimal CorePoint { get; protected set; }
        public decimal MaxCorePoint { get; protected set; }
        public decimal SpiritPoint { get; protected set; }
        public decimal MaxSpiritPoint { get; protected set; }
        public SoulKernelAbility KernelAbility { get; protected set; }

        public SoulAttributes() { }
        public SoulAttributes(SoulKernelType mainSoulType, byte maxReachedPhaseLevel, decimal corePoint, decimal maxCorePoint, decimal spiritPoint, decimal maxSpiritPoint, SoulKernelAbility kernelAbility)
        {
            MainSoulType = mainSoulType;
            MaxReachedPhaseLevel = maxReachedPhaseLevel;
            Phases = new SoulPhase[MaxReachedPhaseLevel + 1];
            CorePoint = corePoint;
            MaxCorePoint = maxCorePoint;
            SpiritPoint = spiritPoint;
            MaxSpiritPoint = maxSpiritPoint;
            KernelAbility = kernelAbility;
        }
        public void LoadPhase(byte phaseLevel, byte understandingLevel, int understandingPoint)
        {
            if(phaseLevel >= 0 && phaseLevel <= MaxReachedPhaseLevel)
            {
                Phases[phaseLevel] = new SoulPhase(phaseLevel, understandingLevel, understandingPoint);
            }
        }
    }
}
