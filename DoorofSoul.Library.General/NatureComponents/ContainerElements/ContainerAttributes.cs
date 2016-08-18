using System;
using System.IO;
using MsgPack.Serialization;

namespace DoorofSoul.Library.General.NatureComponents.ContainerElements
{
    public class ContainerAttributes
    {
        public static object Deserialize(byte[] data)
        {
            var serializer = MessagePackSerializer.Get<ContainerAttributes>();
            using (MemoryStream ms = new MemoryStream(data))
            {
                return serializer.Unpack(ms);
            }
        }

        public static byte[] Serialize(object data)
        {
            ContainerAttributes value = data as ContainerAttributes;
            var serializer = MessagePackSerializer.Get<ContainerAttributes>();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.Pack(memoryStream, value);
                return memoryStream.ToArray();
            }
        }

        private byte level;
        public byte Level { get { return level; } protected set { level = Math.Max(Math.Min(value, MaxLevel), (byte)0); onLevelChange?.Invoke(level); } }

        public byte MaxLevel { get; protected set; } = byte.MaxValue;

        private int experience;
        public int Experience { get { return experience; } set { experience = Math.Max(Math.Min(value, MaxExperience), 0); onExperienceChange?.Invoke(experience); } }

        public int MaxExperience { get; protected set; } = int.MaxValue;

        private decimal lifePoint;
        public decimal LifePoint { get { return lifePoint; } set { lifePoint = Math.Max(Math.Min(value, MaxLifePoint), 0); onLifePointChange?.Invoke(lifePoint); } }

        public decimal MaxLifePoint { get; protected set; } = decimal.MaxValue;

        private decimal energyPoint;
        public decimal EnergyPoint { get { return energyPoint; } set { energyPoint = Math.Max(Math.Min(value, MaxEnergyPoint), 0); onEnergyPointChange?.Invoke(energyPoint); } }

        public decimal MaxEnergyPoint { get; protected set; } = decimal.MaxValue;

        public ContainerKernelAbilityPotential KernelAbilityPotential { get; protected set; }

        public ContainerKernelAbility KernelAbility { get; protected set; }

        private event Action<byte> onLevelChange;
        public event Action<byte> OnLevelChange { add { onLevelChange += value; } remove { onLevelChange -= value; } }

        private event Action<int> onExperienceChange;
        public event Action<int> OnExperienceChange { add { onExperienceChange += value; } remove { onExperienceChange -= value; } }

        private event Action<decimal> onLifePointChange;
        public event Action<decimal> OnLifePointChange { add { onLifePointChange += value; } remove { onLifePointChange -= value; } }

        private event Action<decimal> onEnergyPointChange;
        public event Action<decimal> OnEnergyPointChange { add { onEnergyPointChange += value; } remove { onEnergyPointChange -= value; } }

        public ContainerAttributes() { }
        public ContainerAttributes(byte level, byte maxLevel, int experience, decimal lifePoint, decimal maxLifePoint, decimal energyPoint, decimal maxEnergyPoint, ContainerKernelAbilityPotential kernelAbilityPotential, ContainerKernelAbility kernelAbility)
        {
            Level = level;
            MaxLevel = maxLevel;
            Experience = experience;
            LifePoint = lifePoint;
            MaxLifePoint = maxLifePoint;
            EnergyPoint = energyPoint;
            MaxEnergyPoint = maxEnergyPoint;
            KernelAbilityPotential = kernelAbilityPotential;
            KernelAbility = kernelAbility;
        }
    }
}
