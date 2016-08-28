using DoorofSoul.Protocol;
using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.IO;

namespace DoorofSoul.Library.General.MindComponents.SoulElements
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
        public static SoulAttributes GetDefaultAttribute(SoulKernelTypeCode mainSoulType)
        {
            SoulAttributes attributes = new SoulAttributes(
                mainSoulType: mainSoulType,
                currentPhaseLevel: 0,
                maxReachedPhaseLevel: 0,
                corePoint: 90,
                maxCorePoint: 90,
                spiritPoint: 50,
                maxSpiritPoint: 50,
                kernelAbilities: new SoulKernelAbilities(
                    creation: mainSoulType == SoulKernelTypeCode.Creation ? 5 : 1,
                    destruction: mainSoulType == SoulKernelTypeCode.Destruction ? 5 : 1,
                    conservation: mainSoulType == SoulKernelTypeCode.Conservation ? 5 : 1,
                    revolution: mainSoulType == SoulKernelTypeCode.Revolution ? 5 : 1,
                    balance: mainSoulType == SoulKernelTypeCode.Balance ? 5 : 1,
                    guidance: mainSoulType == SoulKernelTypeCode.Guidance ? 5 : 1,
                    mind: mainSoulType == SoulKernelTypeCode.Mind ? 5 : 1
                )
            );
            attributes.LoadPhase(new SoulPhase(0, 1, 0));
            return attributes;
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

        public SoulKernelAbilities KernelAbilities { get; protected set; }

        private event Action<decimal> onCorePointChange;
        public event Action<decimal> OnCorePointChange { add { onCorePointChange += value; } remove { onCorePointChange -= value; } }

        private event Action<decimal> onSpiritPointChange;
        public event Action<decimal> OnSpiritPointChange { add { onSpiritPointChange += value; } remove { onSpiritPointChange -= value; } }

        public SoulAttributes() { }
        public SoulAttributes(SoulKernelTypeCode mainSoulType, byte currentPhaseLevel, byte maxReachedPhaseLevel, decimal corePoint, decimal maxCorePoint, decimal spiritPoint, decimal maxSpiritPoint, SoulKernelAbilities kernelAbilities)
        {
            MainSoulType = mainSoulType;
            CurrentPhaseLevel = currentPhaseLevel;
            MaxReachedPhaseLevel = maxReachedPhaseLevel;
            Phases = new SoulPhase[MaxReachedPhaseLevel + 1];
            CorePoint = corePoint;
            MaxCorePoint = maxCorePoint;
            SpiritPoint = spiritPoint;
            MaxSpiritPoint = maxSpiritPoint;
            KernelAbilities = kernelAbilities;
        }
        public void LoadPhase(SoulPhase phase)
        {
            if(phase.PhaseLevel >= 0 && phase.PhaseLevel <= MaxReachedPhaseLevel)
            {
                Phases[phase.PhaseLevel] = phase;
            }
        }
        public void LoadPhases(List<SoulPhase> phases)
        {
            foreach(SoulPhase phase in phases)
            {
                LoadPhase(phase);
            }
        }
    }
}
