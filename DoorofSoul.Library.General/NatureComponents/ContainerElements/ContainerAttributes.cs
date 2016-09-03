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
        public static ContainerAttributes GetDefaultAttribute()
        {
            ContainerAttributes attributes = new ContainerAttributes(
                level: 1,
                maxLevel: 30,
                experience: 0,
                lifePoint: 100,
                maxEnergyPoint: 100,
                energyPoint: 50,
                maxLifePoint: 50,
                kernelAbilitiesPotential: new ContainerKernelAbilitiesPotential(
                    sensibility: 1,
                    regeneration: 1,
                    explosiveness: 1,
                    strength: 1,
                    memory: 1,
                    coordination: 1,
                    computation: 1,
                    conductivity: 1
                ),
                kernelAbilities: new ContainerKernelAbilities(
                    sensibilityPoint: 3,
                    regenerationPoint: 3,
                    explosivenessPoint: 3,
                    strengthPoint: 3,
                    memoryPoint: 3,
                    coordinationPoint: 3,
                    computationPoint: 3,
                    conductivityPoint: 3
                )
            );
            return attributes;
        }

        private byte level;
        public byte Level { get { return level; } protected set { level = Math.Max(Math.Min(value, MaxLevel), (byte)0); onLevelChange?.Invoke(level); } }

        public byte MaxLevel { get; protected set; } = byte.MaxValue;

        private int experience;
        public int Experience { get { return experience; } set { experience = Math.Max(Math.Min(value, MaxExperience), 0); onExperienceChange?.Invoke(experience); } }

        public int MaxExperience { get; protected set; } = int.MaxValue;

        private decimal lifePoint;
        public decimal LifePoint
        {
            get { return lifePoint; }
            set
            {
                lifePoint = Math.Max(Math.Min(value, MaxLifePoint), 0);
                onLifePointChange?.Invoke(lifePoint);
            }
        }

        public decimal MaxLifePoint { get; protected set; } = decimal.MaxValue;

        private decimal energyPoint;
        public decimal EnergyPoint { get { return energyPoint; } set { energyPoint = Math.Max(Math.Min(value, MaxEnergyPoint), 0); onEnergyPointChange?.Invoke(energyPoint); } }

        public decimal MaxEnergyPoint { get; protected set; } = decimal.MaxValue;

        public ContainerKernelAbilitiesPotential KernelAbilitiesPotential { get; protected set; }

        public ContainerKernelAbilities KernelAbilities { get; protected set; }

        private event Action<byte> onLevelChange;
        public event Action<byte> OnLevelChange { add { onLevelChange += value; } remove { onLevelChange -= value; } }

        private event Action<int> onExperienceChange;
        public event Action<int> OnExperienceChange { add { onExperienceChange += value; } remove { onExperienceChange -= value; } }

        private event Action<decimal> onLifePointChange;
        public event Action<decimal> OnLifePointChange { add { onLifePointChange += value; } remove { onLifePointChange -= value; } }

        private event Action<decimal> onEnergyPointChange;
        public event Action<decimal> OnEnergyPointChange { add { onEnergyPointChange += value; } remove { onEnergyPointChange -= value; } }

        public ContainerAttributes() { }
        public ContainerAttributes(byte level, byte maxLevel, int experience, decimal lifePoint, decimal maxLifePoint, decimal energyPoint, decimal maxEnergyPoint, ContainerKernelAbilitiesPotential kernelAbilitiesPotential, ContainerKernelAbilities kernelAbilities)
        {
            Level = level;
            MaxLevel = maxLevel;
            Experience = experience;
            LifePoint = lifePoint;
            MaxLifePoint = maxLifePoint;
            EnergyPoint = energyPoint;
            MaxEnergyPoint = maxEnergyPoint;
            KernelAbilitiesPotential = kernelAbilitiesPotential;
            KernelAbilities = kernelAbilities;
        }
    }
}
