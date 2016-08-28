using DoorofSoul.Library.General.NatureComponents;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Effects.Effectors
{
    public class ContainerLifePointEffector : Effector
    {
        public int ContainerLifePointEffectorID { get; private set; }
        public decimal EffectValue { get; private set; }

        public ContainerLifePointEffector(int containerLifePointEffectorID, decimal effectValue)
        {
            ContainerLifePointEffectorID = containerLifePointEffectorID;
            EffectValue = effectValue;
        }
        public override bool Affect(List<IEffectorTarget> targets)
        {
            return targets.TrueForAll(x => x is Container && x.IncreaseLifePoint(EffectValue));
        }
    }
}
