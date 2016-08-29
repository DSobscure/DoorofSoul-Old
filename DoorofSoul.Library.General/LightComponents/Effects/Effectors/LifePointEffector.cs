using DoorofSoul.Library.General.NatureComponents;
using System.Collections.Generic;
using System.Linq;

namespace DoorofSoul.Library.General.LightComponents.Effects.Effectors
{
    public class LifePointEffector : Effector
    {
        public int LifePointEffectorID { get; private set; }
        public decimal EffectValue { get; private set; }

        public LifePointEffector(int lifePointEffectorID, decimal effectValue)
        {
            LifePointEffectorID = lifePointEffectorID;
            EffectValue = effectValue;
        }
        public override bool Affect(List<IEffectorTarget> targets)
        {
            if(targets.Any(x => x is Container))
            {
                targets.OfType<Container>().ToList().ForEach(x => x.Attributes.LifePoint += EffectValue);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
