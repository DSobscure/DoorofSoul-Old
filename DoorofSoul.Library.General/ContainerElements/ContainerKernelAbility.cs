using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Library.General.ContainerElements
{
    public class ContainerKernelAbility
    {
        public int SensibilityPoint { get; protected set; }
        public int RegenerationPoint { get; protected set; }
        public int ExplosivenessPoint { get; protected set; }
        public int StrengthPoint { get; protected set; }
        public int MemoryPoint { get; protected set; }
        public int CoordinationPoint { get; protected set; }
        public int ComputationPoint { get; protected set; }
        public int ConductivityPoint { get; protected set; }

        public ContainerKernelAbility() { }
        public ContainerKernelAbility(int sensibilityPoint, int regenerationPoint, int explosivenessPoint, int strengthPoint, int memoryPoint, int coordinationPoint, int computationPoint, int conductivityPoint)
        {
            SensibilityPoint = sensibilityPoint;
            RegenerationPoint = regenerationPoint;
            ExplosivenessPoint = explosivenessPoint;
            StrengthPoint = strengthPoint;
            MemoryPoint = memoryPoint;
            CoordinationPoint = coordinationPoint;
            ComputationPoint = computationPoint;
            ConductivityPoint = conductivityPoint;
        }
    }
}
