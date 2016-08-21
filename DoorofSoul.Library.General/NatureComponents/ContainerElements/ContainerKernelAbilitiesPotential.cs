namespace DoorofSoul.Library.General.NatureComponents.ContainerElements
{
    public class ContainerKernelAbilitiesPotential
    {
        public int Sensibility { get; protected set; }
        public int Regeneration { get; protected set; }
        public int Explosiveness { get; protected set; }
        public int Strength { get; protected set; }
        public int Memory { get; protected set; }
        public int Coordination { get; protected set; }
        public int Computation { get; protected set; }
        public int Conductivity { get; protected set; }

        public ContainerKernelAbilitiesPotential() { }
        public ContainerKernelAbilitiesPotential(int sensibility, int regeneration, int explosiveness, int strength, int memory, int coordination, int computation, int conductivity)
        {
            Sensibility = sensibility;
            Regeneration = regeneration;
            Explosiveness = explosiveness;
            Strength = strength;
            Memory = memory;
            Coordination = coordination;
            Computation = computation;
            Conductivity = conductivity;
        }
    }
}
