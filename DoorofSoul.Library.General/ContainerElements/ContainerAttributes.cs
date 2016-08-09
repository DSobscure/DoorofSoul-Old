using System.IO;
using MsgPack.Serialization;

namespace DoorofSoul.Library.General.ContainerElements
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

        public byte Level { get; protected set; }
        public byte MaxLevel { get; protected set; }
        public int Experience { get; protected set; }
        public int MaxExperience { get; protected set; }
        public decimal LifePoint { get; protected set; }
        public decimal MaxLifePoint { get; protected set; }
        public decimal EnergyPoint { get; protected set; }
        public decimal MaxEnergyPoint { get; protected set; }
        public ContainerKernelAbilityPotential KernelAbilityPotential { get; protected set; }
        public ContainerKernelAbility KernelAbility { get; protected set; }

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
