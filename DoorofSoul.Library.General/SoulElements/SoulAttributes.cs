using DoorofSoul.Protocol;
using MsgPack.Serialization;
using System;
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

        public SoulKernelTypeCode MainSoulType { get; protected set; }

        public SoulPhase[] Phases { get; protected set; }

        public byte CurrentPhaseLevel { get; protected set; }

        public byte MaxReachedPhaseLevel { get; protected set; }

        private decimal corePoint;
        public decimal CorePoint { get { return corePoint; } set { corePoint = Math.Max(Math.Min(value, MaxCorePoint), 0); onCorePointChange?.Invoke(corePoint); } }

        public decimal MaxCorePoint { get; protected set; } = decimal.MaxValue;

        private decimal spiritPoint;
        public decimal SpiritPoint { get { return spiritPoint; } set { spiritPoint = Math.Max(Math.Min(value, MaxSpiritPoint), 0); onSpiritPointChange?.Invoke(spiritPoint); } }

        public decimal MaxSpiritPoint { get; protected set; } = decimal.MaxValue;

        public SoulKernelAbility KernelAbility { get; protected set; }

        private event Action<decimal> onCorePointChange;
        public event Action<decimal> OnCorePointChange { add { onCorePointChange += value; } remove { onCorePointChange -= value; } }

        private event Action<decimal> onSpiritPointChange;
        public event Action<decimal> OnSpiritPointChange { add { onSpiritPointChange += value; } remove { onSpiritPointChange -= value; } }

        public SoulAttributes() { }
        public SoulAttributes(SoulKernelTypeCode mainSoulType, byte currentPhaseLevel, byte maxReachedPhaseLevel, decimal corePoint, decimal maxCorePoint, decimal spiritPoint, decimal maxSpiritPoint, SoulKernelAbility kernelAbility)
        {
            MainSoulType = mainSoulType;
            CurrentPhaseLevel = currentPhaseLevel;
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
