namespace DoorofSoul.Library.General.MindComponents.SoulElements
{
    public class SoulKernelAbilities
    {
        public int Creation { get; protected set; }
        public int Destruction { get; protected set; }
        public int Conservation { get; protected set; }
        public int Revolution { get; protected set; }
        public int Balance { get; protected set; }
        public int Guidance { get; protected set; }
        public int Mind { get; protected set; }

        public SoulKernelAbilities() { }
        public SoulKernelAbilities(int creation, int destruction, int conservation, int revolution, int balance, int guidance, int mind)
        {
            Creation = creation;
            Destruction = destruction;
            Conservation = conservation;
            Revolution = revolution;
            Balance = balance;
            Guidance = guidance;
            Mind = mind;
        }
    }
}
